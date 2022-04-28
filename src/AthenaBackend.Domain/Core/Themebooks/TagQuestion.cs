using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Themebooks.Dtos;
using AthenaBackend.Domain.Enums;
using AthenaBackend.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace AthenaBackend.Domain.Core.Themebooks
{
    public class TagQuestion : Entity
    {
        public virtual Guid Id { get; protected set; }
        public virtual TagType Type { get; protected set; }
        public virtual string Question { get; protected set; }
        public virtual Themebook Themebook { get; protected set; }

        private List<string> answers = new List<string>();
        public virtual IEnumerable<string> Answers => answers ??= new List<string>();

        protected internal static TagQuestion Create(Themebook themebook, TagQuestionDto dto)
        {
            Validate(dto);

            var tagQuestion = new TagQuestion
            {
                Question = dto.Question,
                Themebook = themebook,
                Type = (TagType)dto.Type
            };

            tagQuestion.AddAnswers(dto.Answers);

            return tagQuestion;
        }

        protected internal void Update(TagQuestionDto dto)
        {
            Question = dto.Question;

            HandleAnswers(dto.Answers);
        }


        #region private fuctions
        private void HandleAnswers(List<string> answers)
        {
            this.answers.Clear();
            AddAnswers(answers);
        }

        private void AddAnswers(List<string> answers)
            => this.answers.AddRange(answers);

        private static void Validate(TagQuestionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Question))
            {
                throw new NullOrWhiteSpaceDomainException(nameof(dto.Question));
            }
        }
        #endregion


    }
}