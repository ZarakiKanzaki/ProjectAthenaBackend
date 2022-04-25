using AthenaBackend.Domain.Core.Characters.Dtos;
using System;
using System.Collections.Generic;

namespace AthenaBackend.DomainTest.Core.Characters.Builders
{
    public class CharacterDtoBuilder
    {
        private Guid? id = null;
        private Guid userId = Guid.NewGuid();
        private string name = "";
        private string mythos = "";
        private string logos = "";
        private string note = "";
        private List<CharacterThemebookDto> themebooks = new List<CharacterThemebookDto>();
        private List<TagDto> tags = new List<TagDto>();

        public CharacterDto Build() =>
          new CharacterDto
          {
              Id = id,
              UserId = userId,
              Name = name,
              Mythos = mythos,
              Logos = logos,
              Note = note,
              Themebooks = themebooks,
              Tags = tags
          };

        public CharacterDtoBuilder WithId(Guid? value)
        {
            id = value;
            return this;
        }

        public CharacterDtoBuilder WithUserId(Guid value)
        {
            userId = value;
            return this;
        }

        public CharacterDtoBuilder WithName(string value)
        {
            name = value;
            return this;
        }

        public CharacterDtoBuilder WithMythos(string value)
        {
            mythos = value;
            return this;
        }

        public CharacterDtoBuilder WithLogos(string value)
        {
            logos = value;
            return this;
        }

        public CharacterDtoBuilder WithNote(string value)
        {
            note = value;
            return this;
        }

        public CharacterDtoBuilder WithThemebooks(List<CharacterThemebookDto> value)
        {
            themebooks = value;
            return this;
        }

        public CharacterDtoBuilder WithTags(List<TagDto> value)
        {
            tags = value;
            return this;
        }
    }
}
