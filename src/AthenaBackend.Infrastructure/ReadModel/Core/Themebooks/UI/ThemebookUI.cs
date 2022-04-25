using AthenaBackend.Domain.WellKnownInstances;
using System;
using System.Collections.Generic;

namespace AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI
{
    public class ThemebookUI
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ThemebookType Type { get; set; }
        public ThemebookConceptUI ThemebookConcept { get; set; }
        public List<string> ExamplesOfApplication { get; set; } = new List<string>();
        public List<string> MisteryOptions { get; set; } = new List<string>();
        public List<string> TitleExamples { get; set; } = new List<string>();
        public List<string> CrewRelationships { get; set; } = new List<string>();
        public List<TagQuestionUI> TagQuestions { get; set; } = new List<TagQuestionUI>();
        public List<ThemebookImprovementUI> Improvements { get; set; } = new List<ThemebookImprovementUI>();
    }
}
