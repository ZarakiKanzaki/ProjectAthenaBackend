using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using System;

namespace AthenaBackend.InfrastructureTest.ReadModel.Core.Themebooks.Builders
{
    public class ThemebookImprovementUIBuilder
    {
        private Guid themebookId = Guid.NewGuid();
        private string title = "";
        private string decription = "";

        public ThemebookImprovementUI Build() =>
          new ThemebookImprovementUI
          {
              ThemebookId = themebookId,
              Title = title,
              Decription = decription
          };

        public ThemebookImprovementUIBuilder WithThemebookId(Guid value)
        {
            themebookId = value;
            return this;
        }

        public ThemebookImprovementUIBuilder WithTitle(string value)
        {
            title = value;
            return this;
        }

        public ThemebookImprovementUIBuilder WithDecription(string value)
        {
            decription = value;
            return this;
        }
    }
}
