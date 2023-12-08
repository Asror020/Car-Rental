using Car_Rental.Models;
using Context;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Car_Rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public UsersController(ApplicationDbContext context) 
        {
            this.context = context;
        }

        [HttpGet("signin")]
        public ActionResult SignIn(string username, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                return BadRequest("User not found");
            return Ok(true);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return context.Users.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<User?> Get(int id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            return user == null ? BadRequest() : Ok(user);
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            var createdUser = context.Users.Add(user);
            context.SaveChanges();

            return user == null ? BadRequest() : Ok(user);
        }

        [HttpPut("{id}")]
        public ActionResult<User> Update(int id, [FromBody] User user)
        {
            var userToFind = context.Users.FirstOrDefault(x => x.Id == id);

            if (userToFind == null)
                return BadRequest();

            user.Id = id;
            var result = context.Users.Update(user);
            context.SaveChanges();

            return result == null ? BadRequest() : Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            var userToRemove = context.Users.FirstOrDefault(x => x.Id == id);
            if(userToRemove == null)
                return BadRequest();

            context.Users.Remove(userToRemove);
            context.SaveChanges();

            return Ok(true);
        }
    }
}
