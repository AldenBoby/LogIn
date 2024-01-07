using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RembraceLogIn.Shared.Models;
using static RembraceLogIn.Shared.Models.AccountModel;

namespace RembraceLogIn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager; // existing .net packages used to handle user creation and details, passwords are hased through this as well
        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterModel model)
        {
            var newUser = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(newUser, model.Password!);

            if(!result.Succeeded) // try and create an account, if unsuccessful provide the reasons in errors
            {
                var errors = result.Errors.Select(x => x.Description); // errors from the usermanager listed

                return BadRequest(new RegisterResult { Successful = false, Errors = errors }); //request returned with json response
            }

            // Create an Account in the accounts table for every user
            var account = new Account { User = newUser};
            newUser.Accounts = new List<Account> {account};
            await _userManager.UpdateAsync(newUser);

            if (newUser.Email!.ToLower().StartsWith("admin")) 
            {
                await _userManager.AddToRoleAsync(newUser, "Admin");
			}
            else
            {
                await _userManager.AddToRoleAsync(newUser, "User");
            }


            return Ok(new RegisterResult { Successful = true });

        }
    }
}
