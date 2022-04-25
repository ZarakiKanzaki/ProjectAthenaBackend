using AthenaBackend.Domain.Core.Characters.Dtos;
using System;
using System.Collections.Generic;

namespace AthenaBackend.DomainTest.Core.Characters.Builders
{
    public class CharacterThemebookDtoBuilder
    {
        private Guid? id = null;
        private Guid themebookId = Guid.NewGuid();
        private Guid characterId = Guid.NewGuid();
        private short typeId = 0;
        private string title = "";
        private string concept = "";
        private string identityMistery = "";
        private string flipside = "";
        private short attentionLevel = 0;
        private short fadeCrackLevel = 0;
        private List<CharacterThemebookTagDto> tags = new List<CharacterThemebookTagDto>();

        public CharacterThemebookDto Build() =>
          new CharacterThemebookDto
          {
              Id = id,
              ThemebookId = themebookId,
              CharacterId = characterId,
              TypeId = typeId,
              Title = title,
              Concept = concept,
              IdentityMistery = identityMistery,
              Flipside = flipside,
              AttentionLevel = attentionLevel,
              FadeCrackLevel = fadeCrackLevel,
              Tags = tags
          };

        public CharacterThemebookDtoBuilder WithId(Guid? value)
        {
            id = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithThemebookId(Guid value)
        {
            themebookId = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithCharacterId(Guid value)
        {
            characterId = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithTypeId(short value)
        {
            typeId = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithTitle(string value)
        {
            title = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithConcept(string value)
        {
            concept = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithIdentityMistery(string value)
        {
            identityMistery = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithFlipside(string value)
        {
            flipside = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithAttentionLevel(short value)
        {
            attentionLevel = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithFadeCrackLevel(short value)
        {
            fadeCrackLevel = value;
            return this;
        }

        public CharacterThemebookDtoBuilder WithTags(List<CharacterThemebookTagDto> value)
        {
            tags = value;
            return this;
        }
    }
}
