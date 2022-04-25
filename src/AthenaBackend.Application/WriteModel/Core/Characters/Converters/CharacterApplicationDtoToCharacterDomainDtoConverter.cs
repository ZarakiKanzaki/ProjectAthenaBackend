using AthenaBackend.Common.Converters;
using System.Collections.Generic;
using System.Linq;
using ApplicationCharacterDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.CharacterDto;
using ApplicationCharacterThemebookDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.CharacterThemebookDto;
using ApplicationTagDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.TagDto;
using DomainCharacterDto = AthenaBackend.Domain.Core.Characters.Dtos.CharacterDto;
using DomainCharacterThemebookDto = AthenaBackend.Domain.Core.Characters.Dtos.CharacterThemebookDto;
using DomainTagDto = AthenaBackend.Domain.Core.Characters.Dtos.TagDto;

namespace AthenaBackend.Application.WriteModel.Core.Characters.Converters
{
    public class CharacterApplicationDtoToCharacterDomainDtoConverter : BaseConverterWithValidation<ApplicationCharacterDto, DomainCharacterDto, CharacterApplicationDtoToCharacterDomainDtoConverter>
    {
        private readonly IConverter<ApplicationTagDto, DomainTagDto> applicationTagToDomainTagConverter;
        private readonly IConverter<ApplicationCharacterThemebookDto, DomainCharacterThemebookDto> applicationThemebookToDomainThemebookConverter;

        public CharacterApplicationDtoToCharacterDomainDtoConverter(IConverter<ApplicationTagDto, DomainTagDto> applicationTagToDomainTagConverter,
                                                                    IConverter<ApplicationCharacterThemebookDto, DomainCharacterThemebookDto> applicationThemebookToDomainThemebookConverter)
        {
            this.applicationTagToDomainTagConverter = applicationTagToDomainTagConverter;
            this.applicationThemebookToDomainThemebookConverter = applicationThemebookToDomainThemebookConverter;
        }

        protected override DomainCharacterDto GetConvertedObject(ApplicationCharacterDto objectToConvert)
        {
            return new DomainCharacterDto
            {
                Id = objectToConvert.Id,
                Logos = objectToConvert.Logos,
                Mythos = objectToConvert.Mythos,
                Name = objectToConvert.Name,
                Note = objectToConvert.Note,
                UserId = objectToConvert.UserId,
                Tags = ConvertTags(objectToConvert),
                Themebooks = ConvertThemebooks(objectToConvert),
            };

        }



        private List<DomainCharacterThemebookDto> ConvertThemebooks(ApplicationCharacterDto objectToConvert)
            => objectToConvert.Themebooks.Select(x => applicationThemebookToDomainThemebookConverter.Convert(x)).ToList();

        private List<DomainTagDto> ConvertTags(ApplicationCharacterDto objectToConvert)
            => objectToConvert.Tags.Select(x => applicationTagToDomainTagConverter.Convert(x)).ToList();
    }
}
