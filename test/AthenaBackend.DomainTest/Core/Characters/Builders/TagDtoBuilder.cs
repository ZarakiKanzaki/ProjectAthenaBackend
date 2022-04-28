using AthenaBackend.Domain.Core.Characters.Dtos;
using AthenaBackend.Domain.Enums;
using System;

namespace AthenaBackend.DomainTest.Core.Characters.Builders
{
    public class TagDtoBuilder
    {
        private Guid? id = null;
        private TagType type = TagType.Power;
        private string name = "";
        private short level = 0;
        private bool isSubtractive = false;

        public TagDto Build() =>
          new TagDto
          {
              Id = id,
              Type = type,
              Name = name,
              Level = level,
              IsSubtractive = isSubtractive
          };

        public TagDtoBuilder WithId(Guid? value)
        {
            id = value;
            return this;
        }

        public TagDtoBuilder WithType(TagType value)
        {
            type = value;
            return this;
        }

        public TagDtoBuilder WithName(string value)
        {
            name = value;
            return this;
        }

        public TagDtoBuilder WithLevel(short value)
        {
            level = value;
            return this;
        }

        public TagDtoBuilder WithIsSubtractive(bool value)
        {
            isSubtractive = value;
            return this;
        }
    }
}
