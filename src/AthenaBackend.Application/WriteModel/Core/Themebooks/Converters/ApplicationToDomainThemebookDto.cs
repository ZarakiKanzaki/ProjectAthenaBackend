using AthenaBackend.Common.Converters;
using ApplicationThemebookDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.ThemebookDto;
using ApplicationThemebookConceptDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.ThemebookConceptDto;
using ApplicationThemebookImprovementDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.ThemebookImprovementDto;
using ApplicationTagQuestionDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.TagQuestionDto;
using DomainThemebookDto = AthenaBackend.Domain.Core.Themebooks.Dtos.ThemebookDto;
using DomainThemebookConceptDto = AthenaBackend.Domain.Core.Themebooks.Dtos.ThemebookConceptDto;
using DomainThemebookImprovementDto = AthenaBackend.Domain.Core.Themebooks.Dtos.ThemebookImprovementDto;
using DomainTagQuestionDto = AthenaBackend.Domain.Core.Themebooks.Dtos.TagQuestionDto;
using System.Linq;
using System.Collections.Generic;

namespace AthenaBackend.Application.WriteModel.Core.Themebooks.Converters
{
    public class ApplicationToDomainThemebookDto : BaseConverterWithValidation<ApplicationThemebookDto, DomainThemebookDto, ApplicationToDomainThemebookDto>
    {
        private readonly IConverter<ApplicationThemebookConceptDto, DomainThemebookConceptDto> applicationToDomainThembookConceptDtoConverter;
        private readonly IConverter<ApplicationThemebookImprovementDto, DomainThemebookImprovementDto> applicationToDomainThembookImprovementDtoConverter;
        private readonly IConverter<ApplicationTagQuestionDto, DomainTagQuestionDto> applicationToDomainTagQuestionDtoConverter;

        public ApplicationToDomainThemebookDto(IConverter<ApplicationThemebookConceptDto, DomainThemebookConceptDto> applicationToDomainThembookConceptDtoConverter,
                                               IConverter<ApplicationThemebookImprovementDto, DomainThemebookImprovementDto> applicationToDomainThembookImprovementDtoConverter,
                                               IConverter<ApplicationTagQuestionDto, DomainTagQuestionDto> applicationToDomainTagQuestionDtoConverter)
        {
            this.applicationToDomainThembookConceptDtoConverter = applicationToDomainThembookConceptDtoConverter;
            this.applicationToDomainThembookImprovementDtoConverter = applicationToDomainThembookImprovementDtoConverter;
            this.applicationToDomainTagQuestionDtoConverter = applicationToDomainTagQuestionDtoConverter;
        }

        protected override DomainThemebookDto GetConvertedObject(ApplicationThemebookDto objectToConvert)
            => new DomainThemebookDto
            {
                Name = objectToConvert.Name,
                CrewRelationships = objectToConvert.CrewRelationships,
                Description = objectToConvert.Description,
                ExamplesOfApplication = objectToConvert.ExamplesOfApplication,
                Id = objectToConvert.Id,
                IdentityMisteryOptions = objectToConvert.IdentityMisteryOptions,
                TitleExamples = objectToConvert.TitleExamples,
                TypeId = objectToConvert.TypeId,
                UserId = objectToConvert.UserId,
                Improvements = ConvertImprovements(objectToConvert),
                ThemebookConcept = ConvertConcept(objectToConvert),
                TagQuestions = ConvertTagQuestions(objectToConvert),
            };

        private List<DomainTagQuestionDto> ConvertTagQuestions(ApplicationThemebookDto objectToConvert) 
            => objectToConvert.TagQuestions.Select(x => applicationToDomainTagQuestionDtoConverter.Convert(x)).ToList();

        private DomainThemebookConceptDto ConvertConcept(ApplicationThemebookDto objectToConvert)
            => applicationToDomainThembookConceptDtoConverter.Convert(objectToConvert.ThemebookConcept);

        private List<DomainThemebookImprovementDto> ConvertImprovements(ApplicationThemebookDto objectToConvert)
            => objectToConvert.Improvements.Select(x => applicationToDomainThembookImprovementDtoConverter.Convert(x)).ToList();
    }
}
