using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoProductoController : ControllerBase
    {
        private readonly IBL_CarritoProducto _bl;

        public CarritoProductoController(IBL_CarritoProducto bl)
        {
            _bl = bl;
        }

        /*[ProducesResponseType(typeof(List<Carrito>), 200)]
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

        }*/


        [ProducesResponseType(typeof(Carrito), 200)]
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

        [ProducesResponseType(typeof(Carrito), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Carrito c)
        {
            try
            {
                _bl.Insert(c);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }

        }

        [HttpPut]
        public IActionResult Put([FromBody] Carrito c)
        {
            try
            {
                _bl.Update(c);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }

        }

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
