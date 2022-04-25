using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Characters.Dtos;
using AthenaBackend.Domain.Enums;
using AthenaBackend.Domain.Exceptions;
using System;

namespace AthenaBackend.Domain.Core.Characters
{

    public class Tag : Entity
    {
        #region Properties
        public virtual Guid Id { get; protected set; }
        public virtual TagType Type { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual short Level { get; protected set; } = 1;
        public virtual bool IsSubtractive { get; protected set; } = false;
        public virtual Character Character { get; protected set; }
        public virtual Guid CharacterId { get; protected set; }

        #endregion

        #region Constructors
        protected Tag() { }
        #endregion

        #region Public Functions
        protected internal static Tag Create(Character character, TagDto dto)
        {
            Validate(dto);

            var tag = new Tag
            {
                Character = character,
                Type = dto.Type,
                Name = dto.Name,
                Level = dto.Level,
                IsSubtractive = dto.IsSubtractive,
            };

            return tag;
        }

        protected internal void Update(TagDto dto)
        {
            Validate(dto);

            Type = dto.Type;
            Name = dto.Name;
            Level = dto.Level;
            IsSubtractive = dto.IsSubtractive;
        }

        #endregion

        #region Custom Functions

        private static void Validate(TagDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new NullOrWhiteSpaceDomainException(nameof(dto.Name));
            }
        }

        #endregion
    }

}