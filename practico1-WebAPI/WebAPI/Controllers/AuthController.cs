using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using DataAccessLayer;
using DataAccessLayer.EFModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.DTOs;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Auth Controlles es el responsable de gestionar todos los endpoints correspondientes a 
    /// la autenticación del sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly DBContextCore db;

        public AuthController(
                UserManager<ApplicationUser> _userManager,
                RoleManager<IdentityRole> _roleManager,
                IConfiguration _configuration,
                IBL_ApplicationUsers _blPersonas,
                DBContextCore _db)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            configuration = _configuration;
            db = _db;
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                ApplicationUser user = await userManager.FindByNameAsync(userName: model.Email);

                // Si el usuario existe, está activo, no está bloqueado y la contraseña es correcta
                if (user != null &&
                    !await userManager.IsLockedOutAsync(user) &&
                    await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);

                    // Restauramos la cantidad de fallos.
                    await userManager.ResetAccessFailedCountAsync(user);

                    return Ok(new LoginResponse
                    {
                        StatusOk = true,
                        StatusMessage = "Usuario logueado correctamente!",
                        IdUsuario = user.Id,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        Email = user.Email,
                        ExpirationMinutes = Convert.ToInt32((token.ValidTo - DateTime.UtcNow).TotalMinutes)
                    });
                }
                else
                {
                    // Si el usuario no existe
                    if (user == null)
                        return Unauthorized(new LoginResponse
                        {
                            StatusOk = false,
                            StatusMessage = $"Usuario o contraseña incorrecta",
                        });

                    // Si el usuario esta bloqueado
                    if (await userManager.IsLockedOutAsync(user))
                    {
                        return Unauthorized(new LoginResponse
                        {
                            StatusOk = false,
                            StatusMessage = "La cuenta ha sido bloqueada debido a demasiados intentos de inicio de sesión fallidos. Inténtalo de nuevo más tarde.",
                        });
                    }

                    // Si llegamos hasta acá es porque la contraseña es incorrecta.
                    // Registramos el fallo de contraseña incorrecta
                    await userManager.AccessFailedAsync(user);
                    return Unauthorized(new LoginResponse
                    {
                        StatusOk = false,
                        StatusMessage = $"Usuario o contraseña incorrecta",
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Error en Login, " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(model.Email);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "El usuario ya existe!"));

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    Password = model.Password
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    string errors = "";
                    result.Errors.ToList().ForEach(x => errors += x.Description + ". ");
                    return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear usuario! Revisar los datos ingresados y probar nuevamente. Errores: " + errors));
                }

                await userManager.AddToRoleAsync(user, "USER");

                // Envío notificación de activación.
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                // ToDo: Agregar lógica para enviar el correo de confirmación si es necesario.

                return Ok(new StatusDTO(true, "Usuario creado correctamente! Se le ha enviado un email para establecer la contraseña"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, ex.Message));
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")] // Solo los usuarios con rol ADMIN pueden acceder a este endpoint
        [Route("RegisterEmpleado")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public async Task<IActionResult> CreateEmpleado([FromBody] RegisterEmpleadoModel model)
        {
            try
            {
                // Crear instancia de ApplicationUser para Empleado
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email, // Asegúrate de tener la propiedad Email en el modelo
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    LName = model.LName,
                    Address = model.Address,
                    // Agrega más propiedades según sea necesario para la creación de empleados
                };

                // Crear el usuario en la base de datos
                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    string errors = string.Join(". ", result.Errors.Select(x => x.Description));
                    return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear empleado. Revisar los datos ingresados y probar nuevamente. Errores: " + errors));
                }

                // Agregar el usuario al rol de EMPLEADO (puedes ajustar el nombre del rol según sea necesario)
                await userManager.AddToRoleAsync(user, "EMPLEADO");

                // ToDo: Agregar lógica adicional si es necesario.

                return Ok(new StatusDTO(true, "Empleado creado correctamente!"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, ex.Message));
            }
        }


        [HttpPost]
        [Route("RegisterCliente")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public async Task<IActionResult> RegisterCliente([FromBody] RegisterClienteModel model)
        {
            try
            {
                // Verificar si el usuario ya existe
                var userExists = await userManager.FindByNameAsync(model.Email);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "El usuario ya existe!"));

                // Crear instancia de ApplicationUser para Cliente
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    LName = model.LName,
                    Address = model.ShipAddress
                };

                // Crear el usuario en la base de datos
                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    string errors = string.Join(". ", result.Errors.Select(x => x.Description));
                    return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear usuario. Revisar los datos ingresados y probar nuevamente. Errores: " + errors));
                }

                // Agregar el usuario al rol de Cliente
                await userManager.AddToRoleAsync(user, "USER");

                // ToDo: Agregar lógica para enviar el correo de confirmación si es necesario.

                return Ok(new StatusDTO(true, "Usuario creado como Cliente correctamente!"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, ex.Message));
            }
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")] // Solo los usuarios con rol ADMIN pueden acceder a este endpoint
        [Route("RegisterAdmin")]
        [ProducesResponseType(typeof(StatusDTO), 200)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminModel model)
        {
            try
            {
                // Verificar si el usuario ya existe
                var userExists = await userManager.FindByNameAsync(model.Email);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "El usuario ya existe!"));

                // Crear instancia de ApplicationUser para Administrador
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    LName = model.LName,
                    Address = model.Address,
                    IsAdmin = true
                };

                // Crear el usuario en la base de datos
                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    string errors = string.Join(". ", result.Errors.Select(x => x.Description));
                    return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear usuario. Revisar los datos ingresados y probar nuevamente. Errores: " + errors));
                }

                // Agregar el usuario al rol de ADMIN
                await userManager.AddToRoleAsync(user, "ADMIN");

                // ToDo: Agregar lógica para enviar el correo de confirmación si es necesario.

                return Ok(new StatusDTO(true, "Usuario creado como Administrador correctamente!"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, ex.Message));
            }
        }




        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            string? JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET");
            if (string.IsNullOrEmpty(JWT_SECRET))
                JWT_SECRET = configuration["JWT:Secret"];

            SymmetricSecurityKey? authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}