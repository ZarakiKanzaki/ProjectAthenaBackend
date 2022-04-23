namespace AthenaBackend.Domain.Exceptions
{
    public class CodeAlreadyExistsDomainException : DomainException
    {
        public CodeAlreadyExistsDomainException(string entityName, string valueCode)
            : base(string.Format("There's alerady an {0} with code with value {1}", entityName, valueCode))
        {

        }
    }
}
