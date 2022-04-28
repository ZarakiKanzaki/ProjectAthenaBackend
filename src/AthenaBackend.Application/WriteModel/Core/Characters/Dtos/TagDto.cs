using System;

namespace AthenaBackend.Application.WriteModel.Core.Characters.Dtos
{
    public class TagDto
    {
        public Guid? Id { get; set; }
        public short Type { get; set; }
        public string Name { get; set; }
        public short Level { get; set; } = 1;
        public bool IsSubtractive { get; set; } = false;
    }

}