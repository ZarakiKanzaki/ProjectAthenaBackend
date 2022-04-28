using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Themebooks.Dtos;
using AthenaBackend.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace AthenaBackend.Domain.Core.Themebooks
{
    public class ThemebookConcept : Entity
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Question { get; protected set; }
        public virtual Themebook Themebook { get; protected set; }
        public virtual Guid ThemebookId { get; protected set; }

        private List<string> answers = new List<string>();
        public virtual IEnumerable<string> Answers => answers ??= new List<string>();

        protected ThemebookConcept()
        {

        }

        protected internal static ThemebookConcept Create(Themebook themebook, ThemebookConceptDto dto)
        {
            Validate(dto);

            var concept = new ThemebookConcept
            {
                Themebook = themebook,
                Question = dto.Question,
            };

            concept.AddAnswers(dto.Answers);

            return concept;
        }

        protected internal void Update(ThemebookConceptDto dto)
        {
            Validate(dto);

            Question = dto.Question;
            answers = dto.Answers;

            HandleAnswers(dto.Answers);
        }

        private void HandleAnswers(List<string> answers)
        {
            this.answers.Clear();
            AddAnswers(answers);
        }

        private void AddAnswers(List<string> answers) => this.answers.AddRange(answers);

        private static void Validate(ThemebookConceptDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Question))
            {
                throw new NullOrWhiteSpaceDomainException(nameof(dto.Question));
            }
        }

    }
}