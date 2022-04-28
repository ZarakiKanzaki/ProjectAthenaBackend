using AthenaBackend.Common.Converters;
using System.Collections.Generic;
using System.Linq;
using ApplicationCharacterThemebookDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.CharacterThemebookDto;
using ApplicationCharacterThemebookTagDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.CharacterThemebookTagDto;
using DomainCharacterThemebookDto = AthenaBackend.Domain.Core.Characters.Dtos.CharacterThemebookDto;
using DomainCharacterThemebookTagDto = AthenaBackend.Domain.Core.Characters.Dtos.CharacterThemebookTagDto;

namespace AthenaBackend.Application.WriteModel.Core.Characters.Converters
{
    public class ApplicationToDomainCharacterThemebookDtoConverter
        : BaseConverterWithValidation<ApplicationCharacterThemebookDto, DomainCharacterThemebookDto, ApplicationToDomainCharacterThemebookDtoConverter>
    {

        private readonly IConverter<ApplicationCharacterThemebookTagDto, DomainCharacterThemebookTagDto> applicationToDomainThemebookTagDtoConverter;

        public ApplicationToDomainCharacterThemebookDtoConverter(
            IConverter<ApplicationCharacterThemebookTagDto, DomainCharacterThemebookTagDto> applicationToDomainThemebookTagDtoConverter)
            => this.applicationToDomainThemebookTagDtoConverter = applicationToDomainThemebookTagDtoConverter;

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
            => objectToConvert.Tags.Select(x => applicationToDomainThemebookTagDtoConverter.Convert(x)).ToList();
    }
}
