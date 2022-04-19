using MediatR;
using System;

namespace AthenaBackend.Common.DomainDrivenDesign
{
    public interface IDomainEventSync : IRequest<Unit>
    {
        Guid Id { get; }

        object AggregateId { get; set; }
    }
}