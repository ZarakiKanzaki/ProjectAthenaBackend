namespace AthenaBackend.Common.DomainDrivenDesign
{
    public abstract class Entity
    {
        public void Delete() => IsDeleted = true;
        public virtual bool IsDeleted { get; protected set; } = false;
    }
}