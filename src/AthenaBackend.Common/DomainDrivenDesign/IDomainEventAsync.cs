using System;

namespace AthenaBackend.Common.DomainDrivenDesign
{
    public interface IDomainEventAsync
    {

        Guid Id { get; }

        object AggregateId { get; set; }
    }
}