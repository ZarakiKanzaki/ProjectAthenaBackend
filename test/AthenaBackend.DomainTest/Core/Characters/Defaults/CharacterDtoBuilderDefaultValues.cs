using System;

namespace AthenaBackend.DomainTest.Core.Characters.Defaults
{
    public static class CharacterDtoBuilderDefaultValues
    {
        public static Guid? id_create = null;
        public static Guid id_update = Guid.NewGuid();
        public static Guid userId = Guid.NewGuid();
        public static string name = "Character name";
        public static string mythos = "My Mythos";
        public static string logos = "My Logos";
        public static string note = "notes";
    }
}
