using System;

namespace AthenaBackend.Common.DomainDrivenDesign
{
    public abstract class DomainEventBase
    {
        public DomainEventBase() { }

        public DomainEventBase(object aggregateId)
        {
            if (aggregateId == null)
                throw new InvalidOperationException("aggregateId cannot be null");

            Id = Guid.NewGuid();
            AggregateId = aggregateId;
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public object AggregateId { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsEventSync() => this is IDomainEventSync;
        public bool IsEventAsync() => this is IDomainEventAsync;
    }
}