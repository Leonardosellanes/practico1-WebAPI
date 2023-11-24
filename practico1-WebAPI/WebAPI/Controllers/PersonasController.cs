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
    public class PersonasController : ControllerBase
    {
        private readonly IBL_Personas _bl;

        public PersonasController(IBL_Personas bl)
        {
            _bl = bl;
        }

        // GET: api/<PersonasController>
        [ProducesResponseType(typeof(List<Persona>), 200)]
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

        // GET api/<PersonasController>/12345678
        [ProducesResponseType(typeof(Persona), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(string documento)
        {
            try
            {
                return Ok(_bl.Get(documento));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
            
        }

        // POST api/<PersonasController>
        [ProducesResponseType(typeof(Persona), 200)]
        [HttpPost]
        [Authorize(Roles = "ADMIN, OTRO")]
        public IActionResult Post([FromBody] Persona x)
        {
            try
            {
                _bl.Insert(x);
            return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
           
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
