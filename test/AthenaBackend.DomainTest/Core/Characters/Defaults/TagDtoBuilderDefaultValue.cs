using AthenaBackend.Domain.Enums;
using System;

namespace AthenaBackend.DomainTest.Core.Characters.Defaults
{
    public static class TagDtoBuilderDefaultValue
    {
        public static Guid? id = null;
        public static Guid id_update = Guid.NewGuid();
        public static TagType type = TagType.Power;
        public static string name = "TAG NAME";
        public static short level = 0;
        public static bool isSubtractive = false;

    }
}
