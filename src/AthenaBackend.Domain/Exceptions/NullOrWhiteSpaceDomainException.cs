namespace AthenaBackend.Domain.Exceptions
{
    public class NullOrWhiteSpaceDomainException : DomainException
    {
        public NullOrWhiteSpaceDomainException(string parameter)
            : base($"{parameter} cannot be null or white space.")
        {

        }
    }
}
