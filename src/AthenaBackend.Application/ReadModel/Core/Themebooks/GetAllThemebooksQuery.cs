using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AthenaBackend.Application.ReadModel.Core.Themebooks
{

    public class GetAllThemebooksQuery : IRequest<IEnumerable<ThemebookUI>>
    {

    }

    public class GetAllThemebooksQueryHandler : IRequestHandler<GetAllThemebooksQuery, IEnumerable<ThemebookUI>>
    {
        private readonly IReadRepository<ThemebookUI, Guid> readRepository;

        public GetAllThemebooksQueryHandler(IReadRepository<ThemebookUI, Guid> readRepository) => this.readRepository = readRepository;

        public async Task<IEnumerable<ThemebookUI>> Handle(GetAllThemebooksQuery request, CancellationToken cancellationToken)
            => await readRepository.GetList();
    }
}
