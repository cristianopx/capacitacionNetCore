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
        public IEnumerable<UserDto> GetAll()
        {
            return _userManager.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserDto Get(int id)
        {
            return _userManager.GetById(id);
        }

        [HttpGet("/username/{username}")]
        public UserDto Get([FromRoute] string username)
        {
            return _userManager.GetByUsername(username);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<UserDto> Post([FromBody] UserDto user)
        {
            return await _userManager.Save(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
