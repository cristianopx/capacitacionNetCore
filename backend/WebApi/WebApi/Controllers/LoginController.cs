using Core.Interfaces;
using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ILoginManager loginManager;
        public LoginController(IConfiguration config, ILoginManager loginManager)
        {
            _configuration = config;
            this.loginManager = loginManager;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] LoginDto login)
        {
            try
            {
                login.Password = Encryptor.Encrypt(login.Password);
                if (await loginManager.LoginAsync(login.Username, login.Password))
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("username", login.Username),
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signIn);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token)});
                }
                return BadRequest(new
                {
                    message = "Username or password incorrect"  
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }
    }
}
