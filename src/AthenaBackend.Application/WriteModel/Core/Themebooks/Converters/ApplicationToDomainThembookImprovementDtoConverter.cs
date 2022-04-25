using ApplicationThemebookImprovementDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.ThemebookImprovementDto;
using DomainThemebookImprovementDto = AthenaBackend.Domain.Core.Themebooks.Dtos.ThemebookImprovementDto;
using AthenaBackend.Common.Converters;

namespace AthenaBackend.Application.WriteModel.Core.Themebooks.Converters
{
    public class ApplicationToDomainThembookImprovementDtoConverter
        : BaseConverterWithValidation<ApplicationThemebookImprovementDto, DomainThemebookImprovementDto, ApplicationToDomainThembookImprovementDtoConverter>
    {
        protected override DomainThemebookImprovementDto GetConvertedObject(ApplicationThemebookImprovementDto objectToConvert)
            => new()
            {
                Id = objectToConvert.Id,
                Decription = objectToConvert.Decription,
                Title = objectToConvert.Title,
            };
    }
}
