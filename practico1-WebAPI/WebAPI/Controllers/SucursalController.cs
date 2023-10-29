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
            return Ok(_bl.Get());
        }

        // GET api/<SucursalController>/1
        [ProducesResponseType(typeof(Sucursal), 200)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_bl.Get(id));
        }

        // POST api/<SucursalController>
        [ProducesResponseType(typeof(Sucursal), 200)]
        [HttpPost]
        public IActionResult Post([FromBody] Sucursal s)
        {
            _bl.Insert(s);
            return Ok();
        }

    }
}
