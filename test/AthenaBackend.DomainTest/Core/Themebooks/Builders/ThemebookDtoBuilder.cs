using AthenaBackend.Domain.Core.Themebooks.Dtos;
using System;
using System.Collections.Generic;

namespace AthenaBackend.DomainTest.Core.Themebooks.Builders
{
    public class ThemebookDtoBuilder
    {
        private Guid userId = Guid.NewGuid();
        private Guid? id = null;
        private string name = "";
        private string description = "";
        private short typeId = 0;
        private ThemebookConceptDto themebookConcept = null;
        private List<string> examplesOfApplication = new List<string>();
        private List<string> misteryOptions = new List<string>();
        private List<string> titleExamples = new List<string>();
        private List<string> crewRelationships = new List<string>();
        private List<TagQuestionDto> tagQuestions = new List<TagQuestionDto>();
        private List<ThemebookImprovementDto> improvements = new List<ThemebookImprovementDto>();

        public ThemebookDto Build()
            => new ThemebookDto
            {
                UserId = userId,
                Id = id,
                Name = name,
                Description = description,
                TypeId = typeId,
                ThemebookConcept = themebookConcept,
                ExamplesOfApplication = examplesOfApplication,
                MisteryOptions = misteryOptions,
                TitleExamples = titleExamples,
                CrewRelationships = crewRelationships,
                TagQuestions = tagQuestions,
                Improvements = improvements
            };

        public ThemebookDtoBuilder WithUserId(Guid value)
        {
            userId = value;
            return this;
        }

        public ThemebookDtoBuilder WithId(Guid? value)
        {
            id = value;
            return this;
        }

        public ThemebookDtoBuilder WithName(string value)
        {
            name = value;
            return this;
        }

        public ThemebookDtoBuilder WithDescription(string value)
        {
            description = value;
            return this;
        }

        public ThemebookDtoBuilder WithTypeId(short value)
        {
            typeId = value;
            return this;
        }

        public ThemebookDtoBuilder WithThemebookConcept(ThemebookConceptDto value)
        {
            themebookConcept = value;
            return this;
        }

        public ThemebookDtoBuilder WithExamplesOfApplication(List<string> value)
        {
            examplesOfApplication = value;
            return this;
        }

        public ThemebookDtoBuilder WithMisteryOptions(List<string> value)
        {
            misteryOptions = value;
            return this;
        }

        public ThemebookDtoBuilder WithTitleExamples(List<string> value)
        {
            titleExamples = value;
            return this;
        }

        public ThemebookDtoBuilder WithCrewRelationships(List<string> value)
        {
            crewRelationships = value;
            return this;
        }

        public ThemebookDtoBuilder WithTagQuestions(List<TagQuestionDto> value)
        {
            tagQuestions = value;
            return this;
        }

        public ThemebookDtoBuilder WithImprovements(List<ThemebookImprovementDto> value)
        {
            improvements = value;
            return this;
        }
    }
}
