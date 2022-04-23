using AthenaBackend.Domain.Core.Themebooks.Dtos;
using System;

namespace AthenaBackend.DomainTest.Core.Themebooks.Builders
{
    public class ThemebookImprovementDtoBuilder
    {
        private Guid? id = null;
        private string title = "";
        private string decription = "";

        public ThemebookImprovementDto Build() =>
          new ThemebookImprovementDto
          {
              Id = id,
              Title = title,
              Decription = decription,
          };

        public ThemebookImprovementDtoBuilder WithId(Guid? value)
        {
            id = value;
            return this;
        }

        public ThemebookImprovementDtoBuilder WithTitle(string value)
        {
            title = value;
            return this;
        }

        public ThemebookImprovementDtoBuilder WithDecription(string value)
        {
            decription = value;
            return this;
        }

    }
}
