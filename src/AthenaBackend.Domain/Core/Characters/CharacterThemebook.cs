using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Characters.Dtos;
using AthenaBackend.Domain.Exceptions;
using AthenaBackend.Domain.WellKnownInstances;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AthenaBackend.Domain.Core.Characters
{

    public class CharacterThemebook : Entity
    {
        #region Properties
        public virtual Guid Id { get; protected set; }
        public virtual Guid ThemebookId { get; protected set; }
        public virtual Guid CharacterId { get; protected set; }
        public virtual Character Character { get; protected set; }
        public virtual ThemebookType Type { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual string Concept { get; protected set; }
        public virtual string IdentityMistery { get; protected set; }
        public virtual string Flipside { get; protected set; }
        public virtual short AttentionLevel { get; protected set; } = 0;
        public virtual short FadeCrackLevel { get; protected set; } = 0;


        private ICollection<CharacterThemebookTag> themebookTags = new List<CharacterThemebookTag>();
        public virtual IEnumerable<CharacterThemebookTag> ThemebookTags => themebookTags ??= new List<CharacterThemebookTag>();
        #endregion

        #region Constructors
        protected CharacterThemebook() { }
        #endregion

        #region Public Functions
        protected internal static CharacterThemebook Create(Character character, CharacterThemebookDto dto)
        {
            Validate(dto);

            var themebook = new CharacterThemebook
            {
                Character = character,
                AttentionLevel = dto.AttentionLevel,
                Concept = dto.Concept,
                FadeCrackLevel = dto.FadeCrackLevel,
                Flipside = dto.Flipside,
                IdentityMistery = dto.IdentityMistery,
                ThemebookId = dto.ThemebookId,
                Title = dto.Title,
                Type = ThemebookTypes.GetThemebookByKey(dto.TypeId),
            };

            themebook.AddTags(dto.Tags);

            return themebook;
        }

        protected internal void Update(CharacterThemebookDto dto)
        {
            Validate(dto);

            AttentionLevel = dto.AttentionLevel;
            Concept = dto.Concept;
            FadeCrackLevel = dto.FadeCrackLevel;
            Flipside = dto.Flipside;
            IdentityMistery = dto.IdentityMistery;
            ThemebookId = dto.ThemebookId;
            Title = dto.Title;
            Type = ThemebookTypes.GetThemebookByKey(dto.TypeId);

            HandleTags(dto.Tags);
        }

        private void HandleTags(List<CharacterThemebookTagDto> tags)
        {
            var toAdd = tags.Where(tag => tag.Id is null);
            var toUpdate = tags.Where(tag => ThemebookTags.Any(themebookTag => themebookTag.IsDeleted == false
                                                                            && themebookTag.Id == tag.Id));
            var toRemove = ThemebookTags.Where(t => t.IsDeleted == false)
                                        .Select(t => t.Id)
                                        .Except(toUpdate.Select(pt => (Guid)pt.Id));
            AddTags(toAdd);
            UpdateTags(toUpdate);
            DeleteTags(toRemove);
        }

        private void DeleteTags(IEnumerable<Guid> toRemove)
        {
            foreach (var tagId in toRemove)
            {
                var tag = GetTagByKey(tagId);
                tag.Delete();
            }
        }

        private void UpdateTags(IEnumerable<CharacterThemebookTagDto> toUpdate)
        {
            foreach (var tag in toUpdate)
            {
                var tagToUpdate = GetTagByKey((Guid)tag.Id);
                tagToUpdate.Update(tag);
            }
        }

        private CharacterThemebookTag GetTagByKey(Guid id) => ThemebookTags.FirstOrDefault(x => x.Id == id);

        private void AddTags(IEnumerable<CharacterThemebookTagDto> toAdd)
        {
            foreach (var tag in toAdd)
            {
                themebookTags.Add(CharacterThemebookTag.Create(this, tag));
            }
        }

        #endregion

        #region Custom Functions

        private static void Validate(CharacterThemebookDto dto)
        {
            var exceptions = new List<DomainException>();
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                exceptions.Add(new NullOrWhiteSpaceDomainException(nameof(Title)));
            }
            if (string.IsNullOrWhiteSpace(dto.Concept))
            {
                exceptions.Add(new NullOrWhiteSpaceDomainException(nameof(Concept)));
            }
            if (string.IsNullOrWhiteSpace(dto.IdentityMistery))
            {
                exceptions.Add(new NullOrWhiteSpaceDomainException(nameof(IdentityMistery)));
            }
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
        #endregion
    }

}