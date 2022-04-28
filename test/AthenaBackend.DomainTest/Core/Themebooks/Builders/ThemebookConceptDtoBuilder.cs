using AthenaBackend.Domain.Core.Themebooks.Dtos;
using System;

namespace AthenaBackend.DomainTest.Core.Themebooks.Builders
{
    public class ThemebookConceptDtoBuilder
    {
        private Guid? userId = null;
        private Guid? id = null;
        private string question = "";

        public ThemebookConceptDto Build() =>
          new ThemebookConceptDto
          {
              UserId = userId,
              Id = id,
              Question = question,
          };

        public ThemebookConceptDtoBuilder WithUserId(Guid? value)
        {
            userId = value;
            return this;
        }

        public ThemebookConceptDtoBuilder WithId(Guid? value)
        {
            id = value;
            return this;
        }

        public ThemebookConceptDtoBuilder WithQuestion(string value)
        {
            question = value;
            return this;
        }

    }
}
