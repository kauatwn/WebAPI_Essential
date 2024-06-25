namespace CatalogDb.API.Exceptions
{
    public class UserCreationFailedException : Exception
    {
        public UserCreationFailedException()
        {
        }

        public UserCreationFailedException(string message) : base(message)
        {
        }
    }
}
