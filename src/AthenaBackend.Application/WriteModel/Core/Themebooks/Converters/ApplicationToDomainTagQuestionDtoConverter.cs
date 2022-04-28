using AthenaBackend.Common.Converters;
using ApplicationTagQuestionDto = AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos.TagQuestionDto;
using DomainTagQuestionDto = AthenaBackend.Domain.Core.Themebooks.Dtos.TagQuestionDto;

namespace AthenaBackend.Application.WriteModel.Core.Themebooks.Converters
{
    public class ApplicationToDomainTagQuestionDtoConverter : BaseConverterWithValidation<ApplicationTagQuestionDto, DomainTagQuestionDto, ApplicationToDomainTagQuestionDtoConverter>
    {
        protected override DomainTagQuestionDto GetConvertedObject(ApplicationTagQuestionDto objectToConvert)
            => new()
            {
                Id = objectToConvert.Id,
                Question = objectToConvert.Question,
                Answers = objectToConvert.Answers,
            };
    }
}
