using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Mvc;
using Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly IBL_Sucursales _bl;

        public SucursalController(IBL_Sucursales bl)
        {
            _bl = bl;
        }

        // GET: api/<SucursalController>
        [ProducesResponseType(typeof(List<Sucursal>), 200)]
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

        // GET api/<SucursalController>/1
        [ProducesResponseType(typeof(Sucursal), 200)]
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

        // POST api/<SucursalController>
        [ProducesResponseType(typeof(Sucursal), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Sucursal s)
        {
            try
            {
                _bl.Insert(s);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
            
        }

        // PUT api/<SucursalController>
        [HttpPut]
        public IActionResult Put([FromBody] Sucursal s)
        {
            try
            {
                _bl.Update(s);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
            
        }

        // DELETE api/<SucursalController>/5
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
