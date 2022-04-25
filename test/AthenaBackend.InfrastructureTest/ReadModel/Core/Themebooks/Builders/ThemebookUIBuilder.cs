using AthenaBackend.Domain.WellKnownInstances;
using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using System;
using System.Collections.Generic;

namespace AthenaBackend.InfrastructureTest.ReadModel.Core.Themebooks.Builders
{
    public class ThemebookUIBuilder
    {
        private Guid id = Guid.NewGuid();
        private string name = "";
        private string description = "";
        private ThemebookType type = null;
        private ThemebookConceptUI themebookConcept = null;
        private List<string> examplesOfApplication = new List<string>();
        private List<string> misteryOptions = new List<string>();
        private List<string> titleExamples = new List<string>();
        private List<string> crewRelationships = new List<string>();
        private List<TagQuestionUI> tagQuestions = new List<TagQuestionUI>();
        private List<ThemebookImprovementUI> improvements = new List<ThemebookImprovementUI>();

        public ThemebookUI Build() =>
          new ThemebookUI
          {
              Id = id,
              Name = name,
              Description = description,
              Type = type,
              ThemebookConcept = themebookConcept,
              ExamplesOfApplication = examplesOfApplication,
              MisteryOptions = misteryOptions,
              TitleExamples = titleExamples,
              CrewRelationships = crewRelationships,
              TagQuestions = tagQuestions,
              Improvements = improvements
          };

        public ThemebookUIBuilder WithId(Guid value)
        {
            id = value;
            return this;
        }

        public ThemebookUIBuilder WithName(string value)
        {
            name = value;
            return this;
        }

        public ThemebookUIBuilder WithDescription(string value)
        {
            description = value;
            return this;
        }

        public ThemebookUIBuilder WithType(ThemebookType value)
        {
            type = value;
            return this;
        }

        public ThemebookUIBuilder WithThemebookConcept(ThemebookConceptUI value)
        {
            themebookConcept = value;
            return this;
        }

        public ThemebookUIBuilder WithExamplesOfApplication(List<string> value)
        {
            examplesOfApplication = value;
            return this;
        }

        public ThemebookUIBuilder WithMisteryOptions(List<string> value)
        {
            misteryOptions = value;
            return this;
        }

        public ThemebookUIBuilder WithTitleExamples(List<string> value)
        {
            titleExamples = value;
            return this;
        }

        public ThemebookUIBuilder WithCrewRelationships(List<string> value)
        {
            crewRelationships = value;
            return this;
        }

        public ThemebookUIBuilder WithTagQuestions(List<TagQuestionUI> value)
        {
            tagQuestions = value;
            return this;
        }

        public ThemebookUIBuilder WithImprovements(List<ThemebookImprovementUI> value)
        {
            improvements = value;
            return this;
        }
    }
}
