using System;

namespace AthenaBackend.Domain.Core.Themebooks.Dtos
{
    public class ThemebookImprovementDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Decription { get; set; }
    }
}