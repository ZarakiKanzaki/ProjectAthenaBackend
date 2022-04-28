using AthenaBackend.Domain.Core.ManualReferences;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AthenaBackend.Application.ReadModel.Core.ManualReferences
{
    public class GetManualReferencesQuery : IRequest<IEnumerable<ManualReference>>
    {

    }

    public class GetManualReferencesQueryHandler : IRequestHandler<GetManualReferencesQuery, IEnumerable<ManualReference>>
    {

        public GetManualReferencesQueryHandler()
        {

        }
        public async Task<IEnumerable<ManualReference>> Handle(GetManualReferencesQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<ManualReference> { new() });
        }
    }

}
