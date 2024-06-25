namespace CatalogDb.API.Exceptions
{
    public class UserAlreadyInRoleException : Exception
    {
        public UserAlreadyInRoleException()
        {
        }

        public UserAlreadyInRoleException(string message) : base(message)
        {
        }
    }
}
