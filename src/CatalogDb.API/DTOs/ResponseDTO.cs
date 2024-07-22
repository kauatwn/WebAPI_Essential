using System.Net;

namespace CatalogDb.API.DTOs
{
    public record ResponseDTO(HttpStatusCode Status, string Message);
}
