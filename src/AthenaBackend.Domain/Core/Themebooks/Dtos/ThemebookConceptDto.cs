using System;
using System.Collections.Generic;

namespace AthenaBackend.Domain.Core.Themebooks.Dtos
{
    public class ThemebookConceptDto
    {
        public Guid? UserId { get; set; }
        public Guid? Id { get; set; }
        public string Question { get; set; }

        public List<string> Answers = new List<string>();
    }
}