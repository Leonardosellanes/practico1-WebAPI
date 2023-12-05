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


    public class RegisterClienteModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "El email es requerido"), MinLength(4), MaxLength(128)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; } = string.Empty;
        public string? ShipAddress { get; set; }
        // Propiedades adicionales para el cliente
        public string? Name { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; } = String.Empty;
        public int? EmpresaId { get; set; }
    }

    public class RegisterAdminModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "El email es requerido"), MinLength(4), MaxLength(128)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; } = string.Empty;
        public bool? IsAdmin { get; set; } = true;
        // Propiedades adicionales para el administrador
        public string? Name { get; set; }
        public string? LName { get; set; }
        public int? EmpresaId { get; set; }
    }

    public class RegisterEmpleadoModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "El email es requerido"), MinLength(4), MaxLength(128)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        // Propiedades adicionales para el Empleado
        public string? Name { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; }
        public int? EmpresaId { get; set; }
    }

    public class RegistroEmpresaAdminRequest
    {   
        public string? NombreEmpresa { get; set; }
        public string? RUTEmpresa { get; set; }
        public string? EmailAdmin { get; set; }
        public string? PasswordAdmin { get; set; }
        public string? NombreAdmin { get; set; }
        public string? ApellidoAdmin { get; set; }
    }

}
