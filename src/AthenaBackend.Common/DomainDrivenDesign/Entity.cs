namespace AthenaBackend.Common.DomainDrivenDesign
{
    public abstract class Entity
    {
        public virtual bool IsDeleted { get; protected set; } = false;
    }
}