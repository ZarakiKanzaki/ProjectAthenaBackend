using System;
using System.Collections.Generic;

namespace AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos
{
    public class ThemebookConceptDto
    {
        public Guid? UserId { get; set; }
        public Guid? Id { get; set; }
        public string Question { get; set; }

        public List<string> Answers { get; set; } = new List<string>();
    }
}