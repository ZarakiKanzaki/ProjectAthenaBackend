using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Themebooks.Dtos;
using AthenaBackend.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AthenaBackend.Domain.Core.Themebooks
{
    public class ThemebookImprovement : Entity
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual string Decription { get; protected set; }
        public virtual Themebook Themebook { get; protected set; }

        protected ThemebookImprovement()
        {

        }

        protected internal static ThemebookImprovement Create(Themebook themebook, ThemebookImprovementDto dto)
        {
            Validate(dto);

            var improvement = new ThemebookImprovement
            {
                Title = dto.Title,
                Decription = dto.Decription,
                Themebook = themebook,
            };

            return improvement;
        }


        protected internal void Update(ThemebookImprovementDto improvement)
        {
            throw new NotImplementedException();
        }

        private static void Validate(ThemebookImprovementDto dto)
        {
            var domainExceptions = new List<DomainException>();

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                domainExceptions.Add(new NullOrWhiteSpaceDomainException(nameof(dto.Title)));
            }

            if (string.IsNullOrWhiteSpace(dto.Decription))
            {
                domainExceptions.Add(new NullOrWhiteSpaceDomainException(nameof(dto.Decription)));
            }

            if (domainExceptions.Any())
            {
                throw new AggregateException(domainExceptions);
            }
        }

    }
}