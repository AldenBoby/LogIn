using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RembraceLogIn.Shared.Models;
using static RembraceLogIn.Shared.Models.AccountModel;

namespace RembraceLogIn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager; // existing .net packages used to handle user creation and details, passwords are hased through this as well
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]

        public ActionResult<IEnumerable<ApplicationUser>> Get()
        {
            var users = _userManager.Users.ToList(); // place all the users and their relevant information in the database
            return users;

        }
    }
}
