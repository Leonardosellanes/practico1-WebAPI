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
    public class OpinionController : ControllerBase
    {
        private readonly IBL_Opiniones _bl;

        public OpinionController(IBL_Opiniones bl)
        {
            _bl = bl;
        }

        // GET: api/<CategoriasController>
        [ProducesResponseType(typeof(List<Opinion>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bl.Get());
        }

        // GET api/<CategoriasController>/1
        [ProducesResponseType(typeof(Opinion), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_bl.Get(id));
        }

        // POST api/<CategoriaController>
        [ProducesResponseType(typeof(Opinion), 200)]
        [HttpPost]
        //[Authorize(Roles = "ADMIN, OTRO")]
        public IActionResult Post([FromBody] Opinion x)
        {
            _bl.Insert(x);
            return Ok();
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Opinion opinion)
        {
            _bl.Update(opinion);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bl.Delete(id);
        }
    }
}
