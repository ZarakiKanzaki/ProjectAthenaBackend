using AthenaBackend.Common.DomainDrivenDesign;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;

namespace AthenaBackend.CommonTest
{
    public class DomainEventsCollectionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RaiseEvent_ValidAsyncEvent_ShouldBeExecutedAsToDispatch()
        {
            var events = new DomainEventsCollection();
            events.Raise(new InternalDomainEvent(1));

            events.IsThereAnyAsyncEventToDispatch.ShouldBeTrue();

            events.AsyncEventsToDispatch.Any(e => e.DomainEvent is InternalDomainEvent).ShouldBeTrue();

            events.IsThereAnyAsyncDispatchedEvents.ShouldBeFalse();
        }

        [Test]
        public void RaiseEvent_NullEvent_ShouldThrowException()
        {
            var events = new DomainEventsCollection();
            Should.Throw<NullReferenceException>(() => events.Raise(null));
        }

        [Test]
        public void RaiseEvent_UnrecognizedEvent_ShouldThrowException()
        {
            var events = new DomainEventsCollection();
            Should.Throw<InvalidOperationException>(() => events.Raise(new InternalUnrecognizedDomainEvent(1)));
        }


        [Test]
        public void MarkEventAsDispatchedSync_SyncEvent_EventProcessedCorrectly()
        {
            var events = new DomainEventsCollection();
            var theEvent = new InternalDomainEventSync(1);

            events.Raise(theEvent);
            events.MarkEventAsDispatchedSync(theEvent.Id);

            events.IsThereAnySyncDispatchedEvents.ShouldBeTrue();
            events.SyncDispatchedEvents.Any(e => e.Id == theEvent.Id).ShouldBeTrue();

            events.IsThereAnySyncEventToDispatch.ShouldBeFalse();
            events.SyncEventsToDispatch.Any().ShouldBeFalse();
        }

        [Test]
        public void MarkEventAsDispatchedSync_AsyncEvent_ShouldThrowExcepion()
        {
            var events = new DomainEventsCollection();
            var theEvent = new InternalDomainEvent(1);

            events.Raise(theEvent);
            Should.Throw<InvalidOperationException>(()=> events.MarkEventAsDispatchedSync(theEvent.Id));
        }


        [Test]
        public void MarkEventAsDispatchedAsync_AsyncEvent_EventProcessedCorrectly()
        {
            var events = new DomainEventsCollection();
            var theEvent = new InternalDomainEvent(1);

            events.Raise(theEvent);
            events.MarkEventAsDispatchedAsync(theEvent.Id);

            events.IsThereAnyAsyncDispatchedEvents.ShouldBeTrue();
            events.AsyncDispatchedEvents.Any(e => e.Id == theEvent.Id).ShouldBeTrue();

            events.IsThereAnyAsyncEventToDispatch.ShouldBeFalse();
            events.AsyncEventsToDispatch.Any().ShouldBeFalse();
        }

        [Test]
        public void MarkEventAsDispatchedAsync_SyncEvent_ShouldThrowExcepion()
        {
            var events = new DomainEventsCollection();
            var theEvent = new InternalDomainEventSync(1);

            events.Raise(theEvent);
            Should.Throw<InvalidOperationException>(() => events.MarkEventAsDispatchedAsync(theEvent.Id));
        }

        [Test]
        public void MarkEventAsDispatchedAsync_SyncEventNotRaised_ShouldThrowExcepion()
        {
            var events = new DomainEventsCollection();
            var theEvent = new InternalDomainEventSync(1);
            var theSecondEvent = new InternalDomainEventSync(2);

            events.Raise(theEvent);
            Should.Throw<InvalidOperationException>(() => events.MarkEventAsDispatchedAsync(theSecondEvent.Id));
        }


        [Test]
        public void MarkEventAsDispatchedSync_AsyncEventNotRaised_ShouldThrowExcepion()
        {
            var events = new DomainEventsCollection();
            var theEvent = new InternalDomainEvent(1);
            var theSecondEvent = new InternalDomainEvent(1);

            events.Raise(theEvent);
            Should.Throw<InvalidOperationException>(() => events.MarkEventAsDispatchedSync(theSecondEvent.Id));
        }
    }

    internal class InternalUnrecognizedDomainEvent : DomainEventBase
    {
        public InternalUnrecognizedDomainEvent(object aggregateId) : base(aggregateId)
        {
        }
    }

    internal class InternalDomainEventSync : DomainEventBase, IDomainEventSync
    {
        public InternalDomainEventSync(object aggregateId) : base(aggregateId)
        {
        }
    }

    internal class InternalDomainEvent : DomainEventBase, IDomainEventAsync
    {
        public InternalDomainEvent(object aggregateId) : base(aggregateId)
        {
        }
    }
}
