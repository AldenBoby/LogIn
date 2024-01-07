using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RembraceLogIn.Server.Data;
using static RembraceLogIn.Shared.Models.AccountModel;

namespace RembraceLogIn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RembraceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RembraceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Account
        [HttpGet("{Username}")]
        public async Task<ActionResult<ApplicationUser>> GetAccountById(string Username)
        {
            var consumer = await _context.Users
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.UserName == Username);
            return Ok(consumer);
        }
    }
}
