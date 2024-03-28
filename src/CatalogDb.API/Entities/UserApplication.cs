using Microsoft.AspNetCore.Identity;

namespace CatalogDb.API.Entities
{
    public class UserApplication : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokeExpiryTime { get; set; }
    }
}
