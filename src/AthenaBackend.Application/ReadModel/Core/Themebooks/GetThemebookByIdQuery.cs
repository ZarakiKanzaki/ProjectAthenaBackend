using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AthenaBackend.Application.ReadModel.Core.Themebooks
{

    public class GetThemebookByIdQuery : IRequest<ThemebookUI>
    {

        public Guid ThemebookId { get; set; }
        public GetThemebookByIdQuery(Guid themebookId) => ThemebookId = themebookId;
    }

    public class GetThemebookByIdQueryHandler : IRequestHandler<GetThemebookByIdQuery, ThemebookUI>
    {
        private readonly IReadRepository<ThemebookUI, Guid> readRepository;

        public GetThemebookByIdQueryHandler(IReadRepository<ThemebookUI, Guid> readRepository) => this.readRepository = readRepository;

        public async Task<ThemebookUI> Handle(GetThemebookByIdQuery request, CancellationToken cancellationToken)
            => await readRepository.GetByKey(request.ThemebookId);
    }


}
