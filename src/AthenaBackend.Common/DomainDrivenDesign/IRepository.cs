using System.Threading.Tasks;

namespace AthenaBackend.Common.DomainDrivenDesign
{
    public interface IRepository<T, TId> : IRepository
                                           where T : Aggregate<TId>
                                           where TId : struct
    {
        Task Add(T entity);
        Task SaveChanges();
    }

    public interface IRepository { }
}
