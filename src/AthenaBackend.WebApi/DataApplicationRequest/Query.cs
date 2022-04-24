using AthenaBackend.Application.ReadModel.Core.ManualReferences;
using AthenaBackend.Domain.Core.ManualReferences;
using HotChocolate;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenaBackend.WebApi.DataApplicationRequest
{
    public class Query
    {
        public string Version => $"Version - 1.0.0";

        public async Task<IEnumerable<ManualReference>> ManualReferences([Service] ISender Mediator)
            => await Mediator.Send(new GetManualReferencesQuery());

    }
}
