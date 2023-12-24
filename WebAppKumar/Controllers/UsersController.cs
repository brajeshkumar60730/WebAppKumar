using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppKumar.Data;
using WebAppKumar.Model;

namespace WebAppKumar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly webAppDbContext _webAppDbContext;
        public UsersController(webAppDbContext webAppDbContext)
        {
            _webAppDbContext = webAppDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var users = await _webAppDbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] User userRequest)
        {
            userRequest.Id = Guid.NewGuid();
            await _webAppDbContext.Users.AddAsync(userRequest);
            await _webAppDbContext.SaveChangesAsync();

            return Ok(userRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await _webAppDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, User updateUserRequest)
        {
            var user = await _webAppDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUserRequest.Name;
            user.Email = updateUserRequest.Email;
            user.Password = updateUserRequest.Password;


            await _webAppDbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _webAppDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _webAppDbContext.Users.Remove(user);
            await _webAppDbContext.SaveChangesAsync();
            return Ok(user);

        }


    }
}
