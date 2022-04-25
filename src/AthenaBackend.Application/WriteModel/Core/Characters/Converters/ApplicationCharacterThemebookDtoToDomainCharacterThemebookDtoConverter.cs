using AthenaBackend.Common.Converters;
using System.Collections.Generic;
using System.Linq;
using ApplicationCharacterThemebookDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.CharacterThemebookDto;
using ApplicationCharacterThemebookTagDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.CharacterThemebookTagDto;
using DomainCharacterThemebookDto = AthenaBackend.Domain.Core.Characters.Dtos.CharacterThemebookDto;
using DomainCharacterThemebookTagDto = AthenaBackend.Domain.Core.Characters.Dtos.CharacterThemebookTagDto;

namespace AthenaBackend.Application.WriteModel.Core.Characters.Converters
{
    public class ApplicationCharacterThemebookDtoToDomainCharacterThemebookDtoConverter
        : BaseConverterWithValidation<ApplicationCharacterThemebookDto, DomainCharacterThemebookDto, ApplicationCharacterThemebookDtoToDomainCharacterThemebookDtoConverter>
    {

        private readonly IConverter<ApplicationCharacterThemebookTagDto, DomainCharacterThemebookTagDto> applicationThemebookTagToDomainThemebookTagConverter;

        public ApplicationCharacterThemebookDtoToDomainCharacterThemebookDtoConverter(
            IConverter<ApplicationCharacterThemebookTagDto, DomainCharacterThemebookTagDto> applicationThemebookTagToDomainThemebookTagConverter)
            => this.applicationThemebookTagToDomainThemebookTagConverter = applicationThemebookTagToDomainThemebookTagConverter;

        protected override DomainCharacterThemebookDto GetConvertedObject(ApplicationCharacterThemebookDto objectToConvert)
            => new DomainCharacterThemebookDto
            {
                AttentionLevel = objectToConvert.AttentionLevel,
                CharacterId = objectToConvert.CharacterId,
                Concept = objectToConvert.Concept,
                FadeCrackLevel = objectToConvert.FadeCrackLevel,
                Flipside = objectToConvert.Flipside,
                Id = objectToConvert.Id,
                IdentityMistery = objectToConvert.IdentityMistery,
                ThemebookId = objectToConvert.ThemebookId,
                Title = objectToConvert.Title,
                TypeId = objectToConvert.TypeId,
                Tags = ConvertTags(objectToConvert),
            };

        private List<DomainCharacterThemebookTagDto> ConvertTags(ApplicationCharacterThemebookDto objectToConvert)
            => objectToConvert.Tags.Select(x => applicationThemebookTagToDomainThemebookTagConverter.Convert(x)).ToList();
    }
}
