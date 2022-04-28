using System;

namespace AthenaBackend.InfrastructureTest.ReadModel.Core.Themebooks.Defaults
{
    public static class TagQuestionUIDefaultValues
    {
        public static Guid? id_empty = null;
        public static Guid? id = Guid.NewGuid();
        public static string question = "TEST";
        public static short type = 0;
    }
}
