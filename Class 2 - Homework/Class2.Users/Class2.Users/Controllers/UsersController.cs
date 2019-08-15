using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Class2.Users.Controllers
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<User> users = new List<User>()
        {
            new User(){ FirstName="John", LastName="Doe", Age=31},
            new User(){ FirstName="Jane", LastName="Doe", Age=25},
            new User(){ FirstName="Michael", LastName="Brown", Age=45},
            new User(){ FirstName="Helena", LastName="Craft", Age=18}
        };
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return users;
        }
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                return users[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound($"User with id: {id} is not found");

            }
            catch (Exception ex)
            {
                return BadRequest($"Problem with your request: {ex.Message}");
            }
        }
        [HttpGet("user/{id}")]
        public ActionResult<string> CheckUser(int id)
        {
            try
            {
                User user = users[id - 1];
                if (user.Age >= 18)
                {
                    return "User is adult";
                }
                return "User is not adult";
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound($"User with id: {id} is not found");

            }
            catch (Exception ex)
            {
                return BadRequest($"Problem with your request: {ex.Message}");
            }

        }

    }
}