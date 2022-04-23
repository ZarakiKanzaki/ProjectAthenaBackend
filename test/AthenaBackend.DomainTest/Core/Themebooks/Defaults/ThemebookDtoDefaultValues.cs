using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenaBackend.DomainTest.Core.Themebooks.Defaults
{
    public static class ThemebookDtoDefaultValues
    {
        public static Guid userId = Guid.NewGuid();
        public static Guid? id_create = null;
        public static Guid? id_updated = Guid.NewGuid();
        public static string name = "TEST";
        public static string description = "TEST DESCRIPTION";
        public static short typeId = 1;
        public static List<string> examplesOfApplication_empty = new List<string>();
        public static List<string> misteryOptions_empty = new List<string>();
        public static List<string> titleExamples_empty = new List<string>();
        public static List<string> crewRelationships_empty = new List<string>();

        public static List<string> examplesOfApplication = new List<string>() { "Test1", "Test2", "Test3" };
        public static List<string> misteryOptions = new List<string>() { "Test1",  "Test3" };
        public static List<string> titleExamples = new List<string>() { "Test1", "Test2" };
        public static List<string> crewRelationships = new List<string>() { "Test1", "Test2", "Test3" };
    }
}
