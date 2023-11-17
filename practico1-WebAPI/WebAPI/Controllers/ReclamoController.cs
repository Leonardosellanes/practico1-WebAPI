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
            return Ok(_bl.Get());
        }

        // GET api/<ReclamoController>/1
        [ProducesResponseType(typeof(Reclamo), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_bl.Get(id));
        }

        // POST api/<ReclamoController>
        [ProducesResponseType(typeof(Reclamo), 200)]
        [HttpPost]
        //[Authorize(Roles = "ADMIN, OTRO")]
        public IActionResult Post([FromBody] Reclamo x)
        {
            _bl.Insert(x);
            return Ok();
        }

        // PUT api/<ReclamoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Reclamo reclamo)
        {
            _bl.Update(reclamo);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bl.Delete(id);
        }
    }
}
