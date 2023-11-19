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
                ApplicationUser user = await userManager.FindByNameAsync(userName: model.Username);

                // Si el usuario existe, está activo, no está bloqueado y la contraseña es correcta
                if (user != null &&
                    !await userManager.IsLockedOutAsync(user) &&
                    await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
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

                // Creamos el usuario.
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email
                };

                var result = await userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    string errors = "";
                    result.Errors.ToList().ForEach(x => errors += x.Description + ". ");
                    return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear usuario! Revisar los datos ingresados y probar nuevamente. Errores: " + errors));
                }

                // Asignar Rol User
                await userManager.AddToRoleAsync(user, "OTRO");

                // Envío notificación de activación.
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var param = new Dictionary<string, string?>
                {
                        {"token", token },
                        {"email", user.Email }
                };

                var resetPassResult = await userManager.ResetPasswordAsync(user, token, model.Password);

                // ToDo
                //var callback = QueryHelpers.AddQueryString(blNotificaciones.GetUrlRestorePassword(), param);
                //blNotificaciones.EnviarForgotPassword(user.Email, persona.GetFullName(), callback);

                return Ok(new StatusDTO(true, "Usuario creado correctamente! Se le ha enviado un email para establecer la contraseña"));
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