using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            new User(){ FirstName="Michael", LastName="Brown", Age=4},
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
        [HttpGet("checkUser/{id}")]
        public ActionResult<string> CheckUser(int id)
        {
            try
            {
                User user = users[id - 1];
                if (user.Age >= 18)
                {
                    return $"User with id {id} is adult!";
                }
                return $"User with id {id} is not adult!";
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
        [HttpPost]
        public IActionResult Post()
        {
            string body;
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                body = sr.ReadToEnd();
            }
            User user = JsonConvert.DeserializeObject<User>(body);
            users.Add(user);
            return Ok($"User with id {users.Count - 1} has been added!");
        }
    }
}