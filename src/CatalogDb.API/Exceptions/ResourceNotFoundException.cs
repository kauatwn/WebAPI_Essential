namespace CatalogDb.API.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
        {
        }
        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }
}
