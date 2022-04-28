using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using System;
using System.Collections.Generic;

namespace AthenaBackend.InfrastructureTest.ReadModel.Core.Themebooks.Builders
{
    public class ThemebookConceptUIBuilder
    {
        private Guid themebookId = Guid.NewGuid();
        private string question = "";
        private List<string> answers = new List<string>();

        public ThemebookConceptUI Build() =>
          new ThemebookConceptUI
          {
              ThemebookId = themebookId,
              Question = question,
              Answers = answers,
          };

        public ThemebookConceptUIBuilder WithThemebookId(Guid value)
        {
            themebookId = value;
            return this;
        }

        public ThemebookConceptUIBuilder WithQuestion(string value)
        {
            question = value;
            return this;
        }

        public ThemebookConceptUIBuilder WithAnswers(List<string> value)
        {
            answers = value;
            return this;
        }
    }
}
