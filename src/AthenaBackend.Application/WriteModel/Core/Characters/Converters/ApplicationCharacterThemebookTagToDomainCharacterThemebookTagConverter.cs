using AthenaBackend.Common.Converters;
using ApplicationCharacterThemebookTagDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.CharacterThemebookTagDto;
using DomainCharacterThemebookTagDto = AthenaBackend.Domain.Core.Characters.Dtos.CharacterThemebookTagDto;

namespace AthenaBackend.Application.WriteModel.Core.Characters.Converters
{
    public class ApplicationCharacterThemebookTagToDomainCharacterThemebookTagConverter
        : BaseConverterWithValidation<ApplicationCharacterThemebookTagDto, DomainCharacterThemebookTagDto, ApplicationCharacterThemebookTagToDomainCharacterThemebookTagConverter>
    {
        protected override DomainCharacterThemebookTagDto GetConvertedObject(ApplicationCharacterThemebookTagDto objectToConvert)
            => new DomainCharacterThemebookTagDto
            {
                CharacterThemebookId = objectToConvert.CharacterThemebookId,
                Id = objectToConvert.Id,
                TagId = objectToConvert.TagId,
                TagName = objectToConvert.TagName,
            };
    }
}
