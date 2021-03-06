using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenaBackend.Common.DomainDrivenDesign
{
    public interface IRepository<T, TId> : IRepository
                                           where T : Aggregate<TId>
                                           where TId : struct
    {
        Task<T> FindByKey(TId id);
        Task<T> FindByCode(string code);
        Task<T> GetByKey(TId id);
        Task<T> GetByCode(string code);
        Task<bool> IsUniqueByCode(string code);
        Task Add(T entity);
        Task SaveChanges();
    }

    public interface IRepository { }

    public interface IReadRepository<T, TId>
        where T : class
        where TId : struct
    {
        Task<T> FindByKey(TId id);
        Task<T> FindByCode(string code);
        Task<T> GetByKey(TId id);
        Task<T> GetByCode(string code);
        Task<IEnumerable<T>> GetList();
    }
}
