using AthenaBackend.Domain.Core.Characters;
using AthenaBackend.Domain.Core.Themebooks;
using AthenaBackend.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AthenaBackend.Infrastructure.WriteModel.Core.Characters
{
    public class CharacterRepository : BaseWriteRepository<Character, Guid>, ICharacterRepository
    {
        public CharacterRepository(WriteDbContext context) : base(context)
        {
        }

        public override async Task<Character> FindByCode(string code)
            => await FindCharacter(code);

        private async Task<Character> FindCharacter(string code)
            => await Context.Characters.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(code.ToLower()));

        public override async Task<Character> GetByCode(string code)
            => await FindByCode(code)
            ?? throw new CannotFindEntityDomainException(nameof(Themebook), nameof(code), code);

        public override async Task<bool> IsUniqueByCode(string code)
            => (await FindByCode(code)) == null;
    }
}
