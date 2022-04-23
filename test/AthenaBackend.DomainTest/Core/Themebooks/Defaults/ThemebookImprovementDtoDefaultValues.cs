using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenaBackend.DomainTest.Core.Themebooks.Defaults
{
    public static class ThemebookImprovementDtoDefaultValues
    {
        public static Guid? id = Guid.NewGuid();
        public static Guid? id_empty = null;
        public static string title = "TEST";
        public static string decription = "TEST Description";
    }
}
