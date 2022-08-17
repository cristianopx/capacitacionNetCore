using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Model.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_userManager.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userManager.GetById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("/username/{username}")]
        public IActionResult Get([FromRoute] string username)
        {
            try
            {
                var user = _userManager.GetByUsername(username);
                return user == null ? NotFound() : Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto user)
        {
            try
            {
                if (user.Id == 0)
                {
                    if (_userManager.GetByUsername(user.Username) != null)
                        return StatusCode(409, $"The user {user.Username} already exists");
                    else
                        return Ok(await _userManager.SaveAsync(user));
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto user)
        {
            try
            {
                if (id == user.Id)
                {
                    var userToModified = _userManager.GetById(id);
                    if(userToModified != null)
                    {
                        if(userToModified.Username == user.Username)
                        {
                            await _userManager.PutAsync(id, user);
                            return Ok("User updated");
                        }
                        return StatusCode(409, "Unequal usernames");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return StatusCode(409, "Unequal Ids");

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = _userManager.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    await _userManager.DeleteAsync(id);
                    return Ok("User deleted");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
