using ApplicationThemebookDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.ThemebookDto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AthenaBackend.Common.Converters;
using DomainThemebookDto = AthenaBackend.Domain.Core.Themebooks.Dtos.ThemebookDto;
using AthenaBackend.Domain.Core.Themebooks;
using ApplicationThemebookConceptDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.ThemebookConceptDto;
using ApplicationThemebookImprovementDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.ThemebookImprovementDto;
using ApplicationTagQuestionDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.TagQuestionDto;
using DomainThemebookConceptDto = AthenaBackend.Domain.Core.Themebooks.Dtos.ThemebookConceptDto;
using DomainThemebookImprovementDto = AthenaBackend.Domain.Core.Themebooks.Dtos.ThemebookImprovementDto;
using DomainTagQuestionDto = AthenaBackend.Domain.Core.Themebooks.Dtos.TagQuestionDto;

namespace AthenaBackend.Application.WriteModel.Core.Themebooks
{

    public class CreateThemebookCommand : IRequest<bool>
    {
        public ApplicationThemebookDto Themebook { get; set; }
        public CreateThemebookCommand(ApplicationThemebookDto themebook) => Themebook = themebook;
    }

    public class CreateThemebookCommandHandler : IRequestHandler<CreateThemebookCommand, bool>
    {
        private readonly IConverter<ApplicationThemebookDto, DomainThemebookDto> converter;
        private readonly IConverter<ApplicationThemebookConceptDto, DomainThemebookConceptDto> applicationToDomainThembookConceptDtoConverter;
        private readonly IConverter<ApplicationThemebookImprovementDto, DomainThemebookImprovementDto> applicationToDomainThembookImprovementDtoConverter;
        private readonly IConverter<ApplicationTagQuestionDto, DomainTagQuestionDto> applicationToDomainTagQuestionDtoConverter;
        private readonly ThemebookService themebookService;

        public CreateThemebookCommandHandler(IConverter<ApplicationThemebookDto, DomainThemebookDto> converter,
                                             IConverter<ApplicationThemebookConceptDto, DomainThemebookConceptDto> applicationToDomainThembookConceptDtoConverter,
                                             IConverter<ApplicationThemebookImprovementDto, DomainThemebookImprovementDto> applicationToDomainThembookImprovementDtoConverter,
                                             IConverter<ApplicationTagQuestionDto, DomainTagQuestionDto> applicationToDomainTagQuestionDtoConverter,
                                             ThemebookService themebookService)
        {
            this.converter = converter;
            this.applicationToDomainThembookConceptDtoConverter = applicationToDomainThembookConceptDtoConverter;
            this.applicationToDomainThembookImprovementDtoConverter = applicationToDomainThembookImprovementDtoConverter;
            this.applicationToDomainTagQuestionDtoConverter = applicationToDomainTagQuestionDtoConverter;
            this.themebookService = themebookService;
        }

        public async Task<bool> Handle(CreateThemebookCommand request, CancellationToken cancellationToken)
        {
            var createdThemebook = await themebookService.Create(converter.Convert(request.Themebook));

            return createdThemebook != null;
        }
    }

}
