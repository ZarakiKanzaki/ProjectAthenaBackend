using System;
using System.Collections.Generic;

namespace AthenaBackend.Domain.Core.Characters.Dtos
{
    public class CharacterThemebookDto
    {
        public Guid? Id { get; set; }
        public Guid ThemebookId { get; set; }
        public Guid CharacterId { get; set; }
        public short TypeId { get; set; }
        public string Title { get; set; }
        public string Concept { get; set; }
        public string IdentityMistery { get; set; }
        public string Flipside { get; set; }
        public short AttentionLevel { get; set; } = 0;
        public short FadeCrackLevel { get; set; } = 0;
        public List<CharacterThemebookTagDto> Tags { get; set; }
    }

}