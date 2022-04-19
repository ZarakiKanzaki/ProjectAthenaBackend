using System.Collections.Generic;
using System.Linq;

namespace AthenaBackend.Common.DomainDrivenDesign
{
    public abstract class Aggregate<TKey> : Aggregate where TKey : struct
    {
        public virtual TKey Id { get; protected set; }

        public virtual byte[] Version { get; protected set; }
    }

    public abstract class Aggregate : Entity
    {
        public Aggregate() => Events = new DomainEventsCollection();

        public virtual DomainEventsCollection Events { get; protected set; }

        public virtual void RaiseEvent(DomainEventBase theEvent)
        {
            if (Events == null)
            {
                Events = new DomainEventsCollection();
            }

            Events.Raise(theEvent);
        }

        public bool IsThereAnySyncEventToDispatch
            => SyncEventsToDispatch != null
            && SyncEventsToDispatch.Any();

        public List<EventToDispatch> SyncEventsToDispatch
            => Events?.SyncEventsToDispatch?.ToList()
            ?? new List<EventToDispatch>();

        public bool IsThereAnyAsyncEventToDispatch
            => Events?.IsThereAnyAsyncEventToDispatch
            ?? false;

        public bool IsThereDispatchedEventsAsync
            => Events?.IsThereAnyAsyncDispatchedEvents
            ?? false;

        public List<EventToDispatch> AsyncEventsToDispatch
            => Events?.AsyncEventsToDispatch?.ToList()
            ?? new List<EventToDispatch>();

        public List<IDomainEventAsync> DispatchedEventsAsync
             => Events?.AsyncDispatchedEvents;

        public bool IsEventAlreadyDispatchedSync(EventToDispatch domainEvent)
             => SyncEventsToDispatch
                    .Any(e => e.DomainEvent.Id == domainEvent.DomainEvent.Id) == false;
    }
}
