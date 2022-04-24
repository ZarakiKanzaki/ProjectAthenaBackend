namespace AthenaBackend.Domain.Exceptions
{
    public class CannotFindEntityDomainException : DomainException
    {
        public CannotFindEntityDomainException(string nameOfEntity, string propertyName, string propertyValue)
            : base($"Cannot find {nameOfEntity} with {propertyName} with value {propertyValue}")
        {

        }
    }
}
