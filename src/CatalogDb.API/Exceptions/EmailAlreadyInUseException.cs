namespace CatalogDb.API.Exceptions
{
    public class EmailAlreadyInUseException : Exception
    {
        public EmailAlreadyInUseException()
        {
        }

        public EmailAlreadyInUseException(string message) : base(message)
        {
        }
    }
}
