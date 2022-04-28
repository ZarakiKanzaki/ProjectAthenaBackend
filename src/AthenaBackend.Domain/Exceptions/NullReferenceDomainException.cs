namespace AthenaBackend.Domain.Exceptions
{
    public class NullReferenceDomainException : DomainException
    {
        public NullReferenceDomainException(string parameter)
            : base($"{parameter} cannot be null.")
        {

        }
    }
}
