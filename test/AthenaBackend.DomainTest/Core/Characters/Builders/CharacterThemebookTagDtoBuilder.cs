using AthenaBackend.Domain.Core.Characters.Dtos;
using System;

namespace AthenaBackend.DomainTest.Core.Characters.Builders
{
    public class CharacterThemebookTagDtoBuilder
    {
        private Guid? id = null;
        private Guid characterThemebookId = Guid.NewGuid();
        private Guid tagId = Guid.NewGuid();
        private string tagName = "";

        public CharacterThemebookTagDto Build() =>
          new CharacterThemebookTagDto
          {
              Id = id,
              CharacterThemebookId = characterThemebookId,
              TagId = tagId,
              TagName = tagName
          };

        public CharacterThemebookTagDtoBuilder WithId(Guid? value)
        {
            id = value;
            return this;
        }

        public CharacterThemebookTagDtoBuilder WithCharacterThemebookId(Guid value)
        {
            characterThemebookId = value;
            return this;
        }

        public CharacterThemebookTagDtoBuilder WithTagId(Guid value)
        {
            tagId = value;
            return this;
        }

        public CharacterThemebookTagDtoBuilder WithTagName(string value)
        {
            tagName = value;
            return this;
        }
    }
}
