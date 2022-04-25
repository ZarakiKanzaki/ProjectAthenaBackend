using AthenaBackend.Application.ReadModel.Core.ManualReferences;
using AthenaBackend.Application.ReadModel.Core.Themebooks;
using AthenaBackend.Domain.Core.ManualReferences;
using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using HotChocolate;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenaBackend.WebApi.DataApplicationRequest
{
    public class Query
    {
        public string Version => $"Version - 0.0.1";

        public async Task<IEnumerable<ManualReference>> ManualReferences([Service] ISender Mediator)
            => await Mediator.Send(new GetManualReferencesQuery());

        public async Task<IEnumerable<ThemebookUI>> Themebooks([Service] ISender Mediator)
            => await Mediator.Send(new GetAllThemebooksQuery());

    }
}
