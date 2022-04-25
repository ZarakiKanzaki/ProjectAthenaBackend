using AthenaBackend.Common.Converters;
using ApplicationThemebookConceptDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.ThemebookConceptDto;
using DomainThemebookConceptDto = AthenaBackend.Domain.Core.Themebooks.Dtos.ThemebookConceptDto;

namespace AthenaBackend.Application.WriteModel.Core.Themebooks.Converters
{
    public class ApplicationToDomainThembookConceptDtoConverter : BaseConverterWithValidation<ApplicationThemebookConceptDto, DomainThemebookConceptDto, ApplicationToDomainThembookConceptDtoConverter>
    {
        protected override DomainThemebookConceptDto GetConvertedObject(ApplicationThemebookConceptDto objectToConvert) 
            => new()
            {
                Id = objectToConvert.Id,
                Question = objectToConvert.Question,
                UserId = objectToConvert.UserId,
            };
    }
}
