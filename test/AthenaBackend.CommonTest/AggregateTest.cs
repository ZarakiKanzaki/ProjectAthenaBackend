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
            animal.IsThereAnyAsyncDispatchedEvents.ShouldBeFalse();
        }

        [Test]
        public void AggregateRaiseEvent_EventAsync_ShouldBeProcessed()
        {
            var animal = Animal.Create(new AnimalCreatedEventAsync(Guid.NewGuid()));
            animal.IsThereAnyAsyncEventToDispatch.ShouldBeTrue();
            animal.AsyncEventsToDispatch.Any(a => a.DomainEvent is AnimalCreatedEventAsync).ShouldBeTrue();
        }

        [Test]
        public void AggregateRaiseEvent_EventAsyncAndEventNull_ShouldBeProcessed()
        {
            var animal = Animal.Create(new AnimalCreatedEventAsync(Guid.NewGuid()), null);
            animal.IsThereAnyAsyncEventToDispatch.ShouldBeTrue();
            animal.AsyncEventsToDispatch.Any(a => a.DomainEvent is AnimalCreatedEventAsync).ShouldBeTrue();
        }

        [Test]
        public void AggregateRaiseEvent_NullEventAsync_ShouldThrowException()
        {
            var exception = Should.Throw<NullReferenceException>(() => new Animal().RaiseEvent(null));
        }

        [Test]
        public void InitializeNewAggregate_NoUserId_CrudOperationShoudBeNull()
        {
            var animal = new Animal();
            animal.CrudOperationLog.ShouldBeNull();
        }

        [Test]
        public void InitializeNewAggregate_wUserId_CrudOperationShoudNotBeNull()
        {
            var animal = new Animal(Guid.NewGuid());
            animal.CrudOperationLog.ShouldNotBeNull();
            animal.CrudOperationLog.Creation.ShouldNotBeNull();
            animal.CrudOperationLog.Update.ShouldBeNull();
            animal.CrudOperationLog.Deletion.ShouldBeNull();
        }

        [Test]
        public void UpdateNewlyAggregate_wUserId_CrudOperationShouldUpdate()
        {
            var animal = new Animal(Guid.NewGuid());

            animal.Update(Guid.NewGuid());
            animal.CrudOperationLog.ShouldNotBeNull();
            animal.CrudOperationLog.Update.ShouldNotBeNull();
            animal.CrudOperationLog.Creation.ShouldNotBeNull();
            animal.CrudOperationLog.Deletion.ShouldBeNull();
        }

        [Test]
        public void DeleteAggregate_WithUpdate_CrudOperationShouldUpdate()
        {
            var animal = new Animal(Guid.NewGuid());
            animal.Update(Guid.NewGuid());
            animal.Delete(Guid.NewGuid());

            animal.CrudOperationLog.ShouldNotBeNull();
            animal.CrudOperationLog.Update.ShouldNotBeNull();
            animal.CrudOperationLog.Creation.ShouldNotBeNull();
            animal.CrudOperationLog.Deletion.ShouldNotBeNull();
            animal.CrudOperationLog.Update.ShouldNotBeSameAs(animal.CrudOperationLog.Deletion);
        }

        [Test]
        public void DeleteAggregate_WithoutUpdate_CrudOperationShouldUpdate()
        {
            var animal = new Animal(Guid.NewGuid());
            animal.Delete(Guid.NewGuid());

            animal.CrudOperationLog.ShouldNotBeNull();
            animal.CrudOperationLog.Update.ShouldNotBeNull();
            animal.CrudOperationLog.Creation.ShouldNotBeNull();
            animal.CrudOperationLog.Deletion.ShouldNotBeNull();
            animal.CrudOperationLog.Update.ShouldBeSameAs(animal.CrudOperationLog.Deletion);
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

        public Animal(Guid guid) : base(guid)
        {
        }

        public static Animal Create(AnimalCreatedEventAsync _event)
        {
            var animalCreated = new Animal();
            animalCreated.CreateOperationLog(Guid.NewGuid());
            animalCreated.RaiseEvent(_event);

            return animalCreated;
        }

        protected internal static Animal Create(AnimalCreatedEventAsync _event, DomainEventsCollection events)
        {
            var animalCreated = new Animal
            {
                Events = events
            };
            animalCreated.CreateOperationLog(Guid.NewGuid());
            animalCreated.RaiseEvent(_event);

            return animalCreated;
        }

        protected internal void Update(Guid guid)
        {
            UpdateOperationLog(guid);
        }

        protected internal void Delete(Guid guid)
        {
            IsDeleted = true;
            DeleteOperationLog(guid);
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
