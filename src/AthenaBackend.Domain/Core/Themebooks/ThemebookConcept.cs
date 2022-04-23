using AthenaBackend.Common.Converters;
using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Themebooks.Dtos;
using System;
using System.Collections.Generic;

namespace AthenaBackend.Domain.Core.Themebooks
{
    public class ThemebookConcept : Entity
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Question { get; protected set; }
        public virtual Guid ThemebookId { get; protected set; }

        private List<string> answers = new List<string>();
        public virtual IEnumerable<string> Answers => answers ??= new List<string>();

        protected internal static ThemebookConcept Create(IConverter<ThemebookConceptDto, ThemebookConcept> converter, ThemebookConceptDto dto)
        {
            Validate(dto);

            return converter.Convert(dto);
        }

        private static void Validate(ThemebookConceptDto dto)
        {
            throw new NotImplementedException();
        }

    }
}