using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Exceptions;
using AthenaBackend.Infrastructure.ReadModel.Core.User.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenaBackend.Infrastructure.ReadModel.Core.User
{
    public class UserReadRepository : BaseReadRepository<UserUI, Guid>, IReadRepository<UserUI, Guid>
    {
        public UserReadRepository(ReadDbContext context) : base(context)
        {
        }

        public async Task<UserUI> FindByCode(string code) => await FindUser(code);

        private Task<UserUI> FindUser(string code) => Context.UserUI.FirstOrDefaultAsync(x => x.GuildMemberId.ToLower().Equals(code.ToLower()));

        public async Task<UserUI> FindByKey(Guid id) => await Context.UserUI.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<UserUI> GetByCode(string code) => await FindUser(code)
                                                         ?? throw new CannotFindEntityDomainException(nameof(UserUI), nameof(code), code);

        public async Task<UserUI> GetByKey(Guid id) => await FindByKey(id)
                                                    ?? throw new CannotFindEntityDomainException(nameof(UserUI), nameof(id), id);

        public async Task<IEnumerable<UserUI>> GetList() => await Context.UserUI.ToListAsync();
    }
}
