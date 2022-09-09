using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Model.DTOs;
using Core.Utils;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userManager.GetAll());
        }

        // GET api/<UserController>/5
        [Authorize]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userManager.GetById(id);
            if(user != null)
            {
                user.Password = null;
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get([FromRoute] string username)
        {
            try
            {
                var user = await _userManager.GetByUsername(username);
                if (user != null)
                {
                    user.Password = null;
                    return Ok(user);
                }
                return NotFound();
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
                if (await _userManager.GetByUsername(user.Username) != null)
                    return StatusCode(409, new {message = $"The user {user.Username} already exists" } );
                user.Password = Encryptor.Encrypt(user.Password);
                await _userManager.SaveAsync(user);
                return Ok(new
                {
                    message = "User created successfully",
                    username = user.Username
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var identity  = HttpContext.User.Identity as ClaimsIdentity;
            var username = JWTUtil.GetTokenUsername(identity);
            if(username == null)
            {
                return BadRequest();
            }
            else
            {
                var user = await _userManager.GetByUsername(username);
                if (user == null)
                    return NotFound(new { message = "User not found" });
                if (user.Password != Encryptor.Encrypt(changePasswordDTO.OldPassword))
                    return BadRequest(new { message = "Wrong password" });
                await _userManager.ChangePassword(user.UserId, Encryptor.Encrypt(changePasswordDTO.NewPassword));
                return Ok(new { message = "Password saved" });
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userManager.GetById(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(id);
                    return Ok($"User {user.Username} deleted");
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
