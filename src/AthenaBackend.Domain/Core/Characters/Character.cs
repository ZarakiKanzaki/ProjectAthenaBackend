using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Characters.Dtos;
using AthenaBackend.Domain.Exceptions;
using AthenaBackend.Domain.WellKnownInstances;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AthenaBackend.Domain.Core.Characters
{

    public class Character : Aggregate<Guid>
    {
        private static readonly short LEGAL_NUMBER_OF_THEMEBOOKS = 4;
        private static readonly short MINIMUM_POWERTAG_ALLOWED = 3;
        private static readonly short MINIMUM_WEAKNESSTAG_ALLOWED = 1;

        #region Properties
        public virtual Guid PlayerId { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Mythos { get; protected set; }
        public virtual string Logos { get; protected set; }
        public virtual string Note { get; protected set; }


        private ICollection<CharacterThemebook> themebooks = new List<CharacterThemebook>();
        public virtual IEnumerable<CharacterThemebook> Themebooks => themebooks ??= new List<CharacterThemebook>();


        private ICollection<Tag> tags = new List<Tag>();
        public virtual IEnumerable<Tag> Tags => tags ??= new List<Tag>();


        public virtual IEnumerable<CharacterThemebook> MythosThemebooks => Themebooks.Where(themebook => themebook.Type.Id == ThemebookTypes.Mythos.Id);
        public virtual IEnumerable<CharacterThemebook> LogosThemebooks => Themebooks.Where(themebook => themebook.Type.Id == ThemebookTypes.Logos.Id);
        public virtual IEnumerable<Tag> PowerTags => Tags.Where(tag => tag.Type == Enums.TagType.Power);
        public virtual IEnumerable<Tag> WeaknessTags => Tags.Where(tag => tag.Type == Enums.TagType.Weakness);


        #endregion

        #region Constructors
        protected Character() { }

        protected Character(Guid userCreatedId) : base(userCreatedId)
        {
        }
        #endregion

        #region Public Functions
        protected internal static Character Create(CharacterDto dto)
        {
            Validate(dto);

            var character = new Character(dto.UserId)
            {
                Name = dto.Name,
                Mythos = dto.Mythos,
                Logos = dto.Logos,
                Note = dto.Note,
            };

            character.AddTags(dto.Tags);
            character.AddThemebooks(dto.Themebooks);

            return character;
        }

        protected internal void Update(CharacterDto dto)
        {
            Validate(dto);

            Name = dto.Name;
            Mythos = dto.Mythos;
            Logos = dto.Logos;
            Note = dto.Note;

            HandleTags(dto.Tags);
            HandleThemebooks(dto.Themebooks);

            UpdateOperationLog(dto.UserId);
        }

        protected internal void Delete(Guid userId)
        {
            Delete();
            DeleteOperationLog(userId);
        }

        #endregion


        #region Private Functions
        #region Themebooks
        private void HandleThemebooks(List<CharacterThemebookDto> themebooks)
        {
            var toAdd = themebooks.Where(t => t.Id is null || Themebooks.Any(themebook => themebook.IsDeleted == false
                                                                                       && themebook.Id != t.Id));
            var toUpdate = themebooks.Where(t => Themebooks.Any(themebook => themebook.IsDeleted == false
                                                                          && themebook.Id == t.Id));
            var toRemove = Themebooks.Where(t => t.IsDeleted == false)
                                     .Select(t => t.Id)
                                     .Except(toUpdate.Select(pt => (Guid)pt.Id));

            AddThemebooks(toAdd);
            UpdateThemebooks(toUpdate);
            DeleteThemebooks(toRemove);
        }

        private void DeleteThemebooks(IEnumerable<Guid> toRemove)
        {
            foreach (var themebookId in toRemove)
            {
                var themebook = GetThemebookByKey(themebookId);
                themebook.Delete();
            }
        }

        private void UpdateThemebooks(IEnumerable<CharacterThemebookDto> toUpdate)
        {
            foreach (var themebook in toUpdate)
            {
                var themebookToUpdate = GetThemebookByKey((Guid)themebook.Id);
                themebookToUpdate.Update(themebook);
            }
        }
        private CharacterThemebook GetThemebookByKey(Guid id) => Themebooks.FirstOrDefault(x => x.Id == id);

        private void AddThemebooks(IEnumerable<CharacterThemebookDto> toAdd)
        {
            foreach (var themebook in toAdd)
            {
                themebooks.Add(CharacterThemebook.Create(this, themebook));
            }
        }

        #endregion

        #region Tags
        private void HandleTags(List<TagDto> tags)
        {
            var toAdd = tags.Where(tag => tag.Id is null);
            var toUpdate = tags.Where(tag => Tags.Any(themebookTag => themebookTag.IsDeleted == false
                                                                   && themebookTag.Id == tag.Id));
            var toRemove = Tags.Where(t => t.IsDeleted == false)
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

        private void UpdateTags(IEnumerable<TagDto> toUpdate)
        {
            foreach (var tag in toUpdate)
            {
                var tagToUpdate = GetTagByKey((Guid)tag.Id);
                tagToUpdate.Update(tag);
            }
        }

        private Tag GetTagByKey(Guid id) => Tags.FirstOrDefault(x => x.Id == id);

        private void AddTags(IEnumerable<TagDto> toAdd)
        {
            foreach (var tag in toAdd)
            {
                tags.Add(Tag.Create(this, tag));
            }
        }
        #endregion
        #endregion

        #region Custom Functions
        private static void Validate(CharacterDto dto)
        {
            var exceptions = new List<DomainException>();

            BasicValidation(dto, exceptions);

            CoreCharacterThemebookValidation(dto, exceptions);

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }

        }

        private static void CoreCharacterThemebookValidation(CharacterDto dto, List<DomainException> exceptions)
        {
            if (dto.Themebooks.Any() == false)
            {
                exceptions.Add(new DomainException($"A Character needs to have {LEGAL_NUMBER_OF_THEMEBOOKS} Themebooks to be created"));
            }

            if (dto.Tags.Any() == false)
            {
                exceptions.Add(new DomainException($"A Character Needs cannot have an empty list of tags"));
            }

            if (DoThemebooksExceedTheMaximumAllowed(dto))
            {
                exceptions.Add(new DomainException($"A Character can only have a maximum of {LEGAL_NUMBER_OF_THEMEBOOKS} Themebooks along Mythos and Logos"));
            }

            ValidateExperienceLevels(dto, exceptions);
            ValidateThemebookTags(dto, exceptions);
        }

        private static void ValidateThemebookTags(CharacterDto dto, List<DomainException> exceptions)
        {
            foreach (var themebook in dto.Themebooks)
            {
                ValidatePowerTags(dto, exceptions, themebook);

                ValidateWeaknessTags(dto, exceptions, themebook);
            }
        }

        private static void ValidateWeaknessTags(CharacterDto dto, List<DomainException> exceptions, CharacterThemebookDto themebook)
        {
            var weaknessTags = dto.WeaknessTags.Where(weaknessTag => themebook.Tags.Any(themebookTag => themebookTag.TagName == weaknessTag.Name));


            if (weaknessTags.Count() < MINIMUM_WEAKNESSTAG_ALLOWED)
            {
                exceptions.Add(new DomainException($"A themebook needs to have at least {MINIMUM_WEAKNESSTAG_ALLOWED} weakness tags"));
            }

            if (weaknessTags.Any(x => x.IsSubtractive == false))
            {
                exceptions.Add(new DomainException($"A themebook needs to have all weakness tags subtractive"));
            }
        }

        private static void ValidatePowerTags(CharacterDto dto, List<DomainException> exceptions, CharacterThemebookDto themebook)
        {
            var powerTags = dto.PowerTags.Where(powerTag => themebook.Tags.Any(themebookTag => themebookTag.TagName == powerTag.Name));

            if (powerTags.Count() < MINIMUM_POWERTAG_ALLOWED)
            {
                exceptions.Add(new DomainException($"A themebook needs to have at least {MINIMUM_POWERTAG_ALLOWED} power tags"));
            }

            if (powerTags.Any(x => x.IsSubtractive))
            {
                exceptions.Add(new DomainException($"A themebook needs to have all power tags non-subtractive"));
            }
        }

        private static void ValidateExperienceLevels(CharacterDto dto, List<DomainException> exceptions)
        {
            if (dto.Themebooks.Any(x => x.FadeCrackLevel < 0))
            {
                exceptions.Add(new DomainException($"A themebook has a Fade/Crack level with unallowed value"));
            }

            if (dto.Themebooks.Any(x => x.AttentionLevel < 0))
            {
                exceptions.Add(new DomainException($"A themebook has a Attention level with unallowed value"));
            }
        }

        private static bool DoThemebooksExceedTheMaximumAllowed(CharacterDto dto)
            => GetSumOfTheThemebooks(dto) > LEGAL_NUMBER_OF_THEMEBOOKS;

        private static int GetSumOfTheThemebooks(CharacterDto dto) => dto.MythosThemebooks.Count() + dto.LogosThemebooks.Count();

        private static void BasicValidation(CharacterDto dto, List<DomainException> exceptions)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                exceptions.Add(new NullOrWhiteSpaceDomainException(nameof(dto.Name)));
            }

            if (string.IsNullOrWhiteSpace(dto.Mythos))
            {
                exceptions.Add(new NullOrWhiteSpaceDomainException(nameof(dto.Mythos)));
            }

            if (string.IsNullOrWhiteSpace(dto.Logos))
            {
                exceptions.Add(new NullOrWhiteSpaceDomainException(nameof(dto.Logos)));
            }
        }

        #endregion
    }

}
