using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

    public class User
    {
        public int ID { get; set; }
        required public string Name { get; set; }
    }

    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        // Static mock data
         private static ConcurrentDictionary<int, User> _users = new ConcurrentDictionary<int, User>(
            new[]
            {
                new KeyValuePair<int, User>(1, new User{ID=1, Name="Alice"}),
                new KeyValuePair<int, User>(2, new User{ID=2, Name="Bob"}),
                new KeyValuePair<int, User>(3, new User{ID=3, Name="Charlie"}),
                new KeyValuePair<int, User>(4, new User{ID=4, Name="Diana"}),
                new KeyValuePair<int, User>(5, new User{ID=5, Name="Ethan"}),
                new KeyValuePair<int, User>(6, new User{ID=6, Name="Fiona"}),
                new KeyValuePair<int, User>(7, new User{ID=7, Name="George"}),
                new KeyValuePair<int, User>(8, new User{ID=8, Name="Hannah"}),
                new KeyValuePair<int, User>(9, new User{ID=9, Name="Ian"}),
                new KeyValuePair<int, User>(10, new User{ID=10, Name="Julia"})
            }
        );


        // GET: api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
                return Ok(_users.Values);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
                if (_users.TryGetValue(id, out var user))
                    return Ok(user);

                return NotFound($"User with ID {id} not found.");
        }

        // POST: api/user
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
                if (_users.ContainsKey(newUser.ID))
                    return Conflict($"User with ID {newUser.ID} already exists.");
                if(newUser.Name.Trim().Length == 0)
                    return BadRequest($"Users missing mandatory 'name' field");

                _users[newUser.ID] = newUser;
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.ID }, newUser);
          
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User updatedUser)
        {
                if (!_users.ContainsKey(id))
                    return NotFound($"User with ID {id} not found.");

                _users[id] = updatedUser;
                return Ok(updatedUser);
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
               if (_users.TryRemove(id, out _))
                    return NoContent();

                return NotFound($"User with ID {id} not found.");
        }
    }

