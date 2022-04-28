using AthenaBackend.Common.Converters;
using AthenaBackend.Domain.Enums;
using ApplicationTagDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.TagDto;
using DomainTagDto = AthenaBackend.Domain.Core.Characters.Dtos.TagDto;

namespace AthenaBackend.Application.WriteModel.Core.Characters.Converters
{
    public class ApplicationToDomainTagDtoConverter : BaseConverterWithValidation<ApplicationTagDto, DomainTagDto, ApplicationToDomainTagDtoConverter>
    {
        protected override DomainTagDto GetConvertedObject(ApplicationTagDto objectToConvert)
            => new DomainTagDto
            {
                Id = objectToConvert.Id,
                Name = objectToConvert.Name,
                IsSubtractive = objectToConvert.IsSubtractive,
                Level = objectToConvert.Level,
                Type = (TagType)objectToConvert.Type,
            };
    }
}
