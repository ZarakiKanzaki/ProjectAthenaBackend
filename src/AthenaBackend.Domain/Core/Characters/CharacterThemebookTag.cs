using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Characters.Dtos;
using System;

namespace AthenaBackend.Domain.Core.Characters
{

    public class CharacterThemebookTag : Entity
    {
        #region Properties
        public virtual Guid Id { get; protected set; }
        public virtual CharacterThemebook CharacterThemebook { get; protected set; }
        public virtual Guid CharacterThemebookId { get; protected set; }
        public virtual Guid TagId { get; protected set; }
        public virtual string TagName { get; protected set; }

        #endregion

        #region Constructors

        protected CharacterThemebookTag() { }

        #endregion

        #region Public Functions
        protected internal static CharacterThemebookTag Create(CharacterThemebook characterThemebook, CharacterThemebookTagDto dto)
        {
            var aggregateName = new CharacterThemebookTag
            {
                CharacterThemebook = characterThemebook,
                TagId = dto.TagId,
                TagName = dto.TagName,
            };

            return aggregateName;
        }

        protected internal void Update(CharacterThemebookTagDto dto)
        {
            TagId = dto.TagId;
            TagName = dto.TagName;
        }

        #endregion

    }

}