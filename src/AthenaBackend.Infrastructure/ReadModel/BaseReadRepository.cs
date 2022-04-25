namespace AthenaBackend.Infrastructure.ReadModel
{
    public abstract class BaseReadRepository<T, TId>
        where T : class
        where TId : struct
    {
        protected internal ReadDbContext Context;

        public BaseReadRepository(ReadDbContext context)
            => Context = context;
    }
}
