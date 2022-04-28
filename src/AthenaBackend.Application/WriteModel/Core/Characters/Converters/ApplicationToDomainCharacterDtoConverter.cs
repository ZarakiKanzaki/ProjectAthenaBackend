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
    public class ApplicationToDomainCharacterDtoConverter : BaseConverterWithValidation<ApplicationCharacterDto, DomainCharacterDto, ApplicationToDomainCharacterDtoConverter>
    {
        private readonly IConverter<ApplicationTagDto, DomainTagDto> applicationToDomainTagDtoConverter;
        private readonly IConverter<ApplicationCharacterThemebookDto, DomainCharacterThemebookDto> applicationToDomainThemebookDtoConverter;

        public ApplicationToDomainCharacterDtoConverter(IConverter<ApplicationTagDto, DomainTagDto> applicationToDomainTagDtoConverter,
                                                        IConverter<ApplicationCharacterThemebookDto, DomainCharacterThemebookDto> applicationToDomainThemebookDtoConverter)
        {
            this.applicationToDomainTagDtoConverter = applicationToDomainTagDtoConverter;
            this.applicationToDomainThemebookDtoConverter = applicationToDomainThemebookDtoConverter;
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
                UserId = objectToConvert.UserId.GetValueOrDefault(),
                Tags = ConvertTags(objectToConvert),
                Themebooks = ConvertThemebooks(objectToConvert),
            };

        }



        private List<DomainCharacterThemebookDto> ConvertThemebooks(ApplicationCharacterDto objectToConvert)
            => objectToConvert.Themebooks.Select(x => applicationToDomainThemebookDtoConverter.Convert(x)).ToList();

        private List<DomainTagDto> ConvertTags(ApplicationCharacterDto objectToConvert)
            => objectToConvert.Tags.Select(x => applicationToDomainTagDtoConverter.Convert(x)).ToList();
    }
}
