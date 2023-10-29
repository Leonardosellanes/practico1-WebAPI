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
    public class AppUsersController : Controller
    {
        private IBL_ApplicationUsers _bl;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppUsersController(IBL_ApplicationUsers bl, UserManager<ApplicationUser> userManager)
        {
            _bl = bl;
            _userManager = userManager;
        }
    }
}

