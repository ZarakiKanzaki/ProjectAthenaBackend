using System;

namespace AthenaBackend.Domain.Core.Themebooks.Dtos
{
    public class ThemebookImprovementDto
    {
        public Guid? UserId { get; set; }
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Decription { get; set; }
    }
}