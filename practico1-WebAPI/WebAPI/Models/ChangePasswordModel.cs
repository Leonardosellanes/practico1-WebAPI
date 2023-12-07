using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y como máximo {1} caracteres de longitud.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación de la contraseña no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
