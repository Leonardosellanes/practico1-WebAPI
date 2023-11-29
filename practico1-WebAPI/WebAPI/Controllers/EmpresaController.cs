using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IBL_Empresas _bl;

        public EmpresaController(IBL_Empresas bl)
        {
            _bl = bl;
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

        // POST api/<EmpresaController>
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

    }
}