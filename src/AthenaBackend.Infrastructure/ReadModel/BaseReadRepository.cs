using AthenaBackend.Common.DomainDrivenDesign;
using System;
using System.Threading.Tasks;

namespace AthenaBackend.Infrastructure.ReadModel
{
    public abstract class BaseReadRepository<T, TId>: IReadRepository<T, TId>
        where T : Aggregate<TId>
        where TId : struct
    {
        protected internal ReadDbContext Context;

        public BaseReadRepository(ReadDbContext context)
            => Context = context;

        public async Task<T> GetByKey(TId id)
            => await FindByKey(id)
            ?? throw new InvalidOperationException($"Cannot find {nameof(T)} with Id {id}");

        public async Task<T> FindByKey(TId id)
            => await Context.Set<T>().FindAsync(id);

        public abstract Task<T> FindByCode(string code);

        public abstract Task<T> GetByCode(string code);

        public abstract Task<bool> IsUniqueByCode(string code);
    }
}
