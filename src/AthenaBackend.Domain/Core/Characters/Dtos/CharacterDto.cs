using AthenaBackend.Domain.Enums;
using AthenaBackend.Domain.WellKnownInstances;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AthenaBackend.Domain.Core.Characters.Dtos
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

        public IEnumerable<CharacterThemebookDto> MythosThemebooks => Themebooks.Where(x => x.TypeId == ThemebookTypes.Mythos.Id);
        public IEnumerable<CharacterThemebookDto> LogosThemebooks => Themebooks.Where(x => x.TypeId == ThemebookTypes.Logos.Id);
        public IEnumerable<TagDto> PowerTags => Tags.Where(x => x.Type == TagType.Power);
        public IEnumerable<TagDto> WeaknessTags => Tags.Where(x => x.Type == TagType.Weakness);
    }

}
