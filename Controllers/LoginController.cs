using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Rudhire_BE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rudhire_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly RudHireDbContext _context;
        private readonly IConfiguration _config;

        public LoginController(RudHireDbContext context, IConfiguration config)
        {
            _context = context;

            _config = config;
        }

        [HttpPost("login")]
        public IActionResult UserLogin(LoginRequest loginRequest)
        {
            var user = _context.TblUserDetails.SingleOrDefault(x => x.UserName == loginRequest.UserName && x.Password == loginRequest.Password);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok("Logged in as " + user.FirstName + " " + user.LastName + " Successfully"+"\nToken: "+token);
            }

            return NotFound("Invalid User Name or password");
        }

        private string GenerateToken(TblUserDetail user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384Signature);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
                );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
