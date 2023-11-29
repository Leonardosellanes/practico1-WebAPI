using BusinessLayer.IBLs;
using DataAccessLayer.EFModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DTOs;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IBL_Empresas _bl;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EmpresaController(IBL_Empresas bl, UserManager<ApplicationUser> userManager, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _bl = bl;
            _userManager = userManager;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
        }

        // GET: api/<EmpresaController>
        [ProducesResponseType(typeof(List<Empresa>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_bl.Get());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
            
        }

        // GET api/<EmpresaController>/1
        [ProducesResponseType(typeof(Empresa), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_bl.Get(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
            
        }

        [ProducesResponseType(typeof(Empresa), 200)]
        [HttpPost("RegistrarEmpresa")]
        public async Task<IActionResult> Post([FromBody] RegistroEmpresaAdminRequest request)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedBl = scope.ServiceProvider.GetRequiredService<IBL_Empresas>();

                    int empresaId = await scopedBl.InsertAsync(new Empresa { Nombre = request.NombreEmpresa, RUT = request.RUTEmpresa });
                    Console.WriteLine($"Empresa ID después de la inserción: {empresaId}");
                    RegisterAdminModel adminModel = new RegisterAdminModel
                    {
                        Email = request.EmailAdmin,
                        Password = request.PasswordAdmin,
                        IsAdmin = true,
                        Name = request.NombreAdmin,
                        LName = request.ApellidoAdmin,
                        EmpresaId = empresaId,
                    };

                    IActionResult result = await RegisterAdmin(adminModel);

                    if (result is ObjectResult objectResult && objectResult.StatusCode == StatusCodes.Status200OK)
                    {
                        return Ok(new { EmpresaId = empresaId });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error al registrar el administrador.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
        }

        private async Task<IActionResult> RegisterAdmin(RegisterAdminModel model)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.Email);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, "El usuario ya existe!"));
                Console.WriteLine($"Valor de empresaId antes de crear adminModel: {model.EmpresaId}");

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    LName = model.LName,
                    Address = model.Address,
                    IsAdmin = true,
                    EmpresaId = model.EmpresaId,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    string errors = string.Join(". ", result.Errors.Select(x => x.Description));
                    return StatusCode(StatusCodes.Status500InternalServerError, new StatusDTO(false, "Error al crear usuario. Revisar los datos ingresados y probar nuevamente. Errores: " + errors));
                }

                await _userManager.AddToRoleAsync(user, "ADMIN");

                // correo?

                return Ok(new StatusDTO(true, "Usuario creado como Administrador correctamente!"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new StatusDTO(false, ex.Message));
            }
        }


        // PUT api/<EmpresaController>
        [HttpPut]
        public IActionResult Put( [FromBody] Empresa e)
        {
            try
            {
                _bl.Update(e);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
            
        }

        // DELETE api/<EmpresaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _bl.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
            
        }

    }
}