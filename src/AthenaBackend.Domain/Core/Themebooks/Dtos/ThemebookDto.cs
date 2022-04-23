using System;
using System.Collections.Generic;

namespace AthenaBackend.Domain.Core.Themebooks.Dtos
{
    public class ThemebookDto
    {
        public Guid UserId { get; set; }
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short TypeId { get; set; }
        public ThemebookConceptDto ThemebookConcept { get; set; }
        public List<string> ExamplesOfApplication { get; set; } = new List<string>();
        public List<string> MisteryOptions { get; set; } = new List<string>();
        public List<string> TitleExamples { get; set; } = new List<string>();
        public List<string> CrewRelationships { get; set; } = new List<string>();
        public List<TagQuestionDto> TagQuestions { get; set; } = new List<TagQuestionDto>();
        public List<ThemebookImprovementDto> Improvements { get; set; } = new List<ThemebookImprovementDto>();
    }
}