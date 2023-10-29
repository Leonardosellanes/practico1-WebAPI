using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Mvc;
using Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IBL_Facturas _bl;

        public FacturaController(IBL_Facturas bl)
        {
            _bl = bl;
        }

        // GET: api/<FacturaController>
        [ProducesResponseType(typeof(List<Factura>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bl.Get());
        }

        // GET api/<FacturaController>/5
        [ProducesResponseType(typeof(Factura), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_bl.Get(id));
        }

        // POST api/<FacturaController>
        [ProducesResponseType(typeof(Factura), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Factura f)
        {
            _bl.Insert(f);
            return Ok();
        }

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Factura f)
        {
            _bl.Update(f);
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bl.Delete(id); 
        }
    }
}
