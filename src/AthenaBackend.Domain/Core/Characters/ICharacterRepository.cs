using AthenaBackend.Common.DomainDrivenDesign;
using System;

namespace AthenaBackend.Domain.Core.Characters
{
    public interface ICharacterRepository : IRepository<Character, Guid>
    {
    }
}
