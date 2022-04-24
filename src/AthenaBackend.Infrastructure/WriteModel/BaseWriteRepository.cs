using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace AthenaBackend.Infrastructure.WriteModel
{
    public abstract class BaseWriteRepository<T, TId> : IRepository<T, TId>
        where T : Aggregate<TId>
        where TId : struct
    {
        protected internal WriteDbContext Context;

        public BaseWriteRepository(WriteDbContext context)
            => Context = context;
        public async Task SaveChanges() => await Context.SaveChangesAsync();

        public async Task Add(T entity)
            => await Context.AddAsync(entity);

        public async Task<T> GetByKey(TId id)
            => await FindByKey(id)
            ?? throw new CannotFindEntityDomainException(nameof(T), nameof(id), id.ToString());

        public async Task<T> FindByKey(TId id)
            => await Context.Set<T>().FindAsync(id);

        public abstract Task<T> FindByCode(string code);

        public abstract Task<T> GetByCode(string code);

        public abstract Task<bool> IsUniqueByCode(string code);
    }
}
