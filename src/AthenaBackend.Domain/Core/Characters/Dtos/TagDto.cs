using AthenaBackend.Domain.Enums;
using System;

namespace AthenaBackend.Domain.Core.Characters.Dtos
{
    public class TagDto
    {
        public Guid? Id { get; set; }
        public TagType Type { get; set; }
        public string Name { get; set; }
        public short Level { get; set; } = 1;
        public bool IsSubtractive { get; set; } = false;
    }

}