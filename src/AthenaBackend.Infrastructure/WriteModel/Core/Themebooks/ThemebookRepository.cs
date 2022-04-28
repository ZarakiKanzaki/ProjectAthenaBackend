using AthenaBackend.Domain.Core.Themebooks;
using AthenaBackend.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AthenaBackend.Infrastructure.WriteModel.Core.Themebooks
{
    public class ThemebookRepository : BaseWriteRepository<Themebook, Guid>, IThemebookRepository
    {
        public ThemebookRepository(WriteDbContext context) : base(context)
        {
        }

        public override async Task<Themebook> FindByCode(string code)
            => await Context.Themebooks.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(code.ToLower()));

        public override async Task<Themebook> GetByCode(string code)
            => await FindByCode(code) ?? throw new CannotFindEntityDomainException(nameof(Themebook), nameof(code), code);

        public override async Task<bool> IsUniqueByCode(string code)
            => (await FindByCode(code)) == null;
    }
}
