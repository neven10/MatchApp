using Data.Model;
using MatchApp.API.Models;
using MatchApp.Data;
using MatchApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MatchApp.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MatchDbContext _context;

        public UsersController(MatchDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<User>> Auth(UserDTO user)
        {
            var getuser = await Authenticate(user.UserName, user.Password);
            if (getuser == null)
                return NotFound();
            else
                return Ok();
 
     
          

        }

        private async Task<User> Authenticate (string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == username && x.Password==password);
            if (user == null)
                return null;
            return user;
        }



   
        [HttpPost("[action]")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

           

    }
}
