namespace CatalogDb.API.Exceptions
{
    public class InvalidRefreshTokenException : Exception
    {
        public InvalidRefreshTokenException()
        {
        }

        public InvalidRefreshTokenException(string message) : base(message)
        {
        }
    }
}
