using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "El nombre de Usuario es requerido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public required string Password { get; set; }
    }

}
