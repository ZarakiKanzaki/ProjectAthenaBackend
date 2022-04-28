using System;

namespace AthenaBackend.Domain.Core.Characters.Dtos
{
    public class CharacterThemebookTagDto
    {
        public Guid? Id { get; set; }
        public Guid CharacterThemebookId { get; set; }
        public Guid TagId { get; set; }
        public string TagName { get; set; }
    }

}