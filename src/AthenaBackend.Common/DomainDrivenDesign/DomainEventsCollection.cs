using System;
using System.Collections.Generic;
using System.Linq;

namespace AthenaBackend.Common.DomainDrivenDesign
{
    public class DomainEventsCollection
    {
        public DomainEventsCollection()
        {
            AsyncEventsToDispatch = new List<EventToDispatch>();
            AsyncDispatchedEvents = new List<IDomainEventAsync>();

            SyncEventsToDispatch = new List<EventToDispatch>();
            SyncDispatchedEvents = new List<IDomainEventSync>();
        }

        public List<EventToDispatch> AsyncEventsToDispatch { get; private set; }
        public List<EventToDispatch> SyncEventsToDispatch { get; private set; }

        public List<IDomainEventAsync> AsyncDispatchedEvents { get; private set; }

        public List<IDomainEventSync> SyncDispatchedEvents { get; private set; }

        public void Raise(DomainEventBase domainEvent)
        {
            if (domainEvent == null)
            {
                throw new NullReferenceException($"Cannot raise a null event.");
            }

            if (domainEvent.IsEventSync() == false && domainEvent.IsEventAsync() == false)
            {
                throw new InvalidOperationException($@"Cannot raise an event that isn't Sync and isn't Async (your event should 
implement interface {nameof(IDomainEventAsync)} or interface {nameof(IDomainEventSync)})");
            }

            if (AsyncEventsToDispatch == null)
            {
                AsyncEventsToDispatch = new List<EventToDispatch>();
            }

            if (SyncEventsToDispatch == null)
            {
                SyncEventsToDispatch = new List<EventToDispatch>();
            }

            var toDispatchEvent = new EventToDispatch(domainEvent, domainEvent.GetType());

            if (domainEvent.IsEventAsync())
            {
                AsyncEventsToDispatch.Add(toDispatchEvent);
            }

            if (domainEvent.IsEventSync())
            {
                SyncEventsToDispatch.Add(toDispatchEvent);
            }
        }

        public void MarkEventAsDispatchedSync(Guid idDomainEvent)
        {
            var toDispatchEvent = GetEventToDispatchSync(idDomainEvent);

            if (toDispatchEvent.DomainEvent?.IsEventSync() == false)
            {
                throw new InvalidOperationException($"You can mark as dispatched sync only Sync events. The event should implement interface {nameof(IDomainEventSync)}");
            }

            if (SyncDispatchedEvents == null)
            {
                SyncDispatchedEvents = new List<IDomainEventSync>();
            }

            if (SyncEventsToDispatch == null)
            {
                SyncEventsToDispatch = new List<EventToDispatch>();
            }

            SyncDispatchedEvents.Add((IDomainEventSync)toDispatchEvent.DomainEvent);
            SyncEventsToDispatch.Remove(toDispatchEvent);
        }

        public void MarkEventAsDispatchedAsync(Guid idDomainEvent)
        {
            var toDispatchEvent = GetEventToDispatchAsync(idDomainEvent);

            if (toDispatchEvent.DomainEvent?.IsEventAsync() == false)
            {
                throw new InvalidOperationException($"You can mark as dispatched async only Async events. The event should implement interface {nameof(IDomainEventAsync)}");
            }

            if (AsyncDispatchedEvents == null)
            {
                AsyncDispatchedEvents = new List<IDomainEventAsync>();
            }

            if (AsyncEventsToDispatch == null)
            {
                AsyncEventsToDispatch = new List<EventToDispatch>();
            }

            AsyncDispatchedEvents.Add((IDomainEventAsync)toDispatchEvent.DomainEvent);
            AsyncEventsToDispatch.Remove(toDispatchEvent);
        }

        public bool IsThereAnyAsyncEventToDispatch
            => AsyncEventsToDispatch != null && AsyncEventsToDispatch.Any();

        public bool IsThereAnyAsyncDispatchedEvents
            => AsyncDispatchedEvents != null && AsyncDispatchedEvents.Any();


        public bool IsThereAnySyncEventToDispatch
            => SyncEventsToDispatch != null && SyncEventsToDispatch.Any();

        public bool IsThereAnySyncDispatchedEvents
            => SyncDispatchedEvents != null && SyncDispatchedEvents.Any();

        private EventToDispatch GetEventToDispatchSync(Guid idDomainEvent)
        {
            var eventToDispatch = GetSyncEventToDispatchById(idDomainEvent);

            return eventToDispatch == null
                ? throw new InvalidOperationException($"ToDispatchEvent with id {idDomainEvent} not found")
                : eventToDispatch;
        }

        private EventToDispatch GetEventToDispatchAsync(Guid idDomainEvent)
        {
            var eventToDispatch = GetAsyncEventToDispatchById(idDomainEvent);

            return eventToDispatch == null
                ? throw new InvalidOperationException($"ToDispatchEvent with id {idDomainEvent} not found")
                : eventToDispatch;
        }
        private EventToDispatch GetAsyncEventToDispatchById(Guid idDomainEvent)
            => AsyncEventsToDispatch.FirstOrDefault(de => de.DomainEvent.Id == idDomainEvent);

        private EventToDispatch GetSyncEventToDispatchById(Guid idDomainEvent)
            => SyncEventsToDispatch.FirstOrDefault(de => de.DomainEvent.Id == idDomainEvent);
    }

    public class EventToDispatch
    {
        public EventToDispatch(DomainEventBase domainEvent, Type eventType)
        {
            DomainEvent = domainEvent;
            EventType = eventType;
        }

        public DomainEventBase DomainEvent { get; set; }
        public Type EventType { get; set; }
    }
}