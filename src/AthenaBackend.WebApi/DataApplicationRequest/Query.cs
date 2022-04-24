using AthenaBackend.Application.ReadModel.Core.ManualReferences;
using AthenaBackend.Domain.Core.ManualReferences;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenaBackend.WebApi.DataApplicationRequest
{
    public class Query
    {

        private IMediator mediator;
        protected IMediator Mediator => mediator;

        public Query(IMediator mediator) => this.mediator = mediator;


        public string Version => $"Version - 1.0.0";

        public async Task<IEnumerable<ManualReference>> GetManualReferences()
            => await Mediator.Send(new GetManualReferencesQuery());

    }
}
