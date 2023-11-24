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
    public class CategoriaController : ControllerBase
    {
        private readonly IBL_Categorias _bl;

        public CategoriaController(IBL_Categorias bl)
        {
            _bl = bl;
        }

        // GET: api/<CategoriasController>
        [ProducesResponseType(typeof(List<Categoria>), 200)]
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

        // GET api/<CategoriasController>/1
        [ProducesResponseType(typeof(Categoria), 200)]
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
        [ProducesResponseType(typeof(Categoria), 200)]
        [HttpPost]
        //[Authorize(Roles = "ADMIN, OTRO")]
        public IActionResult Post([FromBody] Categoria x)
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

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                 _bl.Update(categoria);
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
