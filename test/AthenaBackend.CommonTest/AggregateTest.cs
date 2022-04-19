using AthenaBackend.Common.DomainDrivenDesign;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;

namespace AthenaBackend.CommonTest
{
    public class AggregateTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AggregateRaiseEvent_EventSync_ShouldBeProcessed()
        {
            var animal = new Animal();
            animal.IsThereAnySyncEventToDispatch.ShouldBeTrue();
            animal.SyncEventsToDispatch.Any(a => a.DomainEvent is AnimalCreatedEvent).ShouldBeTrue();
        }

        [Test]
        public void AggregateRaiseEvent_EventAsync_ShouldBeProcessed()
        {
            var animal = Animal.Create(new AnimalCreatedEventAsync(Guid.NewGuid()));
            animal.IsThereAnyAsyncEventToDispatch.ShouldBeTrue();
            animal.AsyncEventsToDispatch.Any(a => a.DomainEvent is AnimalCreatedEventAsync).ShouldBeTrue();
        }

        [Test]
        public void AggregateRaiseEvent_NullEventAsync_ShouldThrowException()
        {
            var exception = Should.Throw<NullReferenceException>(() => new Animal().RaiseEvent(null));

        }
    }

    internal class Animal : Aggregate<Guid>
    {
        public Guid Code { get; set; }
        public Animal()
        {
            Code = Guid.NewGuid();
            RaiseEvent(new AnimalCreatedEvent(this.Id));
        }

        public static Animal Create(AnimalCreatedEventAsync _event)
        {
            var animalCreated = new Animal();
            animalCreated.RaiseEvent(_event);

            return animalCreated;
        }
    }

    internal class AnimalCreatedEvent : DomainEventBase, IDomainEventSync
    {
        public AnimalCreatedEvent(object aggregateId) : base(aggregateId)
        {
        }
    }
    internal class AnimalCreatedEventAsync : DomainEventBase, IDomainEventAsync
    {
        public AnimalCreatedEventAsync(object aggregateId) : base(aggregateId)
        {
        }
    }
}
