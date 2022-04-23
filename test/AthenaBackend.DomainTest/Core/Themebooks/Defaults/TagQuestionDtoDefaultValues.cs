using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenaBackend.DomainTest.Core.Themebooks.Defaults
{
    public static class TagQuestionDtoDefaultValues
    {
        public static Guid? id_empty = null;
        public static Guid? id = Guid.NewGuid();
        public static string question = "TEST";
        public static short type = 0;
    }
}
