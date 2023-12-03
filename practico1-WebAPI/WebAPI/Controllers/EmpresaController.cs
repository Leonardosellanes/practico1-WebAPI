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
        [HttpPost]
        public IActionResult Post([FromBody] Empresa e)
        {
            try
            {
                _bl.Insert(e);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
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

        [ProducesResponseType(typeof(Empresa), 200)]
        [HttpGet("Reportes/{id}")]
        public IActionResult Reportes(int id)
        {
            try
            {
                return Ok(_bl.Reportes(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }

        }

    }
}