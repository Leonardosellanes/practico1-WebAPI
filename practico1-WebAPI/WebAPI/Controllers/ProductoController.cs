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
    public class ProductoController : ControllerBase
    {
        private readonly IBL_Productos _bl;

        public ProductoController(IBL_Productos bl)
        {
            _bl = bl;
        }

        // GET: api/<ProductoController>
        [ProducesResponseType(typeof(List<Producto>), 200)]
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

        // GET api/<ProductoController>/1
        [ProducesResponseType(typeof(Producto), 200)]
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

        // POST api/<CategoriaController>
        [ProducesResponseType(typeof(Producto), 200)]
        [HttpPost]
        //[Authorize(Roles = "ADMIN, OTRO")]
        public IActionResult Post([FromBody] Producto p)
        {
            try
            {
                _bl.Insert(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Mensaje error:" + ex.Message);
            }
            
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Producto producto)
        {
            try
            {
                _bl.Update(producto);
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
