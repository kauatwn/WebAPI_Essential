namespace CatalogDb.API.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException()
        {
        }

        public AuthenticationException(string message) : base(message)
        {
        }
    }
}
