using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RegisterModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "El email es requerido"), MinLength(4), MaxLength(128)]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "La contraseña es requerida"), MinLength(4), MaxLength(128)]
        public string Password { get; set; } = string.Empty;
    }
}
