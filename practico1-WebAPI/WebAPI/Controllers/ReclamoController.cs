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
    public class ReclamoController : ControllerBase
    {
        private readonly IBL_Reclamos _bl;

        public ReclamoController(IBL_Reclamos bl)
        {
            _bl = bl;
        }

        // GET: api/<ReclamoController>
        [ProducesResponseType(typeof(List<Reclamo>), 200)]
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

        // GET api/<ReclamoController>/1
        [ProducesResponseType(typeof(Reclamo), 200)]
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

        // POST api/<ReclamoController>
        [ProducesResponseType(typeof(Reclamo), 200)]
        [HttpPost]
        //[Authorize(Roles = "ADMIN, OTRO")]
        public IActionResult Post([FromBody] Reclamo x)
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

        // PUT api/<ReclamoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Reclamo reclamo)
        {
            try
            {
                _bl.Update(reclamo);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
            
        }

        // DELETE api/<ValuesController>/5
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
