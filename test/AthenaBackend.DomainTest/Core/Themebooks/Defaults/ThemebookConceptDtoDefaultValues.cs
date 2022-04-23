using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenaBackend.DomainTest.Core.Themebooks.Defaults
{
    public static class ThemebookConceptDtoDefaultValues
    {
        public static Guid userId = Guid.NewGuid();
        public static Guid id = Guid.NewGuid();
        public static string question = "Test";
    }
}
