using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using System;

namespace AthenaBackend.InfrastructureTest.ReadModel.Core.Themebooks.Builders
{
    public class TagQuestionUIBuilder
    {
        private Guid themebookId = Guid.NewGuid();
        private string question = "";
        private short type = 0;

        public TagQuestionUI Build() =>
          new TagQuestionUI
          {
              ThemebookId = themebookId,
              Question = question,
              Type = type
          };

        public TagQuestionUIBuilder WithThemebookId(Guid value)
        {
            themebookId = value;
            return this;
        }

        public TagQuestionUIBuilder WithQuestion(string value)
        {
            question = value;
            return this;
        }

        public TagQuestionUIBuilder WithType(short value)
        {
            type = value;
            return this;
        }
    }
}
