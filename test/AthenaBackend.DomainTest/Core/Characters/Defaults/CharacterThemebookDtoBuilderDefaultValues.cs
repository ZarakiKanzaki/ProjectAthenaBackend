using System;

namespace AthenaBackend.DomainTest.Core.Characters.Defaults
{
    public static class CharacterThemebookDtoBuilderDefaultValues
    {
        public static Guid? id = null;
        public static Guid themebookId = Guid.NewGuid();
        public static Guid characterId = Guid.NewGuid();
        public static short typeId = 0;
        public static string title = "Title";
        public static string concept = "My concept";
        public static string identityMistery = "My identity or mistery";
        public static string flipside = "";
        public static short attentionLevel = 0;
        public static short fadeCrackLevel = 0;
    }
}
