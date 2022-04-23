using AthenaBackend.Domain.Core.Themebooks.Dtos;
using System;

namespace AthenaBackend.DomainTest.Core.Themebooks.Builders
{
    public class TagQuestionDtoBuilder
    {
        private Guid? id = null;
        private string question = "";
        private short type = 0;

        public TagQuestionDto Build() =>
          new TagQuestionDto
          {
              Id = id,
              Question = question,
              Type = type
          };

        public TagQuestionDtoBuilder WithId(Guid? value)
        {
            id = value;
            return this;
        }

        public TagQuestionDtoBuilder WithQuestion(string value)
        {
            question = value;
            return this;
        }

        public TagQuestionDtoBuilder WithType(short value)
        {
            type = value;
            return this;
        }
    }
}
