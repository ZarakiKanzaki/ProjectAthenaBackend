using System;
using System.Collections.Generic;

namespace AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI
{
    public class ThemebookConceptUI
    {
        public Guid ThemebookId { get; set; }
        public string Question { get; set; }

        public List<string> Answers { get; set; } = new List<string>();
    }
}