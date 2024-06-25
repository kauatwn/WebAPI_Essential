namespace CatalogDb.API.Exceptions
{
    public class RoleCreationFailedException : Exception
    {
        public RoleCreationFailedException()
        {
        }

        public RoleCreationFailedException(string message) : base(message)
        {
        }
    }
}
