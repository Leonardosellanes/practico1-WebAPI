using BusinessLayer.IBLs;
using DataAccessLayer.EFModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace AcmeSolutions.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IBL_ApplicationUsers _bl;

        public AppUsersController(IBL_ApplicationUsers bl)
        {
            _bl = bl;
        }

        [HttpGet("{empresaId}")]
        public IActionResult Get(int empresaId)
        {
            var users = _bl.Get(empresaId);
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ApplicationUser user)
        {
            _bl.Insert(user);
            return Ok("User added successfully.");
        }

        // Agrega métodos para actualizar y eliminar según sea necesario.
    }

}

