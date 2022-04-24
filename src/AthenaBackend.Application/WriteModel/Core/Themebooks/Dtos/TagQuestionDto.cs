using System;
using System.Collections.Generic;

namespace AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos
{
    public class TagQuestionDto
    {
        public Guid? Id { get; set; }
        public string Question { get; set; }
        public short Type { get; set; }

        public List<string> Answers = new List<string>();
    }
}