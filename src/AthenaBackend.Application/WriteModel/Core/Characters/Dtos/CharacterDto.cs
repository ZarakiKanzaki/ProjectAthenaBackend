using System;
using System.Collections.Generic;

namespace AthenaBackend.Application.WriteModel.Core.Characters.Dtos
{
    public class CharacterDto
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Mythos { get; set; }
        public string Logos { get; set; }
        public string Note { get; set; }
        public List<CharacterThemebookDto> Themebooks { get; set; }
        public List<TagDto> Tags { get; set; }
    }

}
