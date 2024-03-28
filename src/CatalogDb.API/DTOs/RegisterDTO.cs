using System.ComponentModel.DataAnnotations;

namespace CatalogDb.API.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "E-mail is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        public string Password { get; set; } = string.Empty;
    }
}
