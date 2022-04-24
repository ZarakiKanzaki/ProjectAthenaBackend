using AthenaBackend.Common.DomainDrivenDesign;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthenaBackend.Common.DependecyInjection
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<T> ConfigureAsAggregate<T, TId>(this EntityTypeBuilder<T> thisBuilder)
            where T : Aggregate<TId>
            where TId : struct
        {
            thisBuilder.HasKey(x => x.Id);

            thisBuilder.Property(x => x.Version).IsRowVersion();

            thisBuilder.Ignore(x => x.Events)
                .Ignore(x => x.DispatchedEventsAsync)
                .Ignore(x => x.IsThereAnyAsyncDispatchedEvents)
                .Ignore(x => x.IsThereAnyAsyncEventToDispatch)
                .Ignore(x => x.IsThereAnySyncEventToDispatch)
                .Ignore(x => x.AsyncEventsToDispatch)
                .Ignore(x => x.SyncEventsToDispatch);

            return thisBuilder;
        }
    }
}
