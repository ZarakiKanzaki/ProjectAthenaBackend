using System;
using System.Collections.Generic;

namespace AthenaBackend.Domain.Core.Themebooks.Dtos
{
    public class TagQuestionDto
    {
        public Guid? Id { get; set; }
        public string Question { get; set; }
        public short Type { get; set; }

        public List<string> Answers { get; set; } = new List<string>();
    }
}