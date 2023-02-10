using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HCAuthTest
{
    public class AuthController : Controller
    {
        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult Login(LoginModel user, [FromServices] IConfiguration config)
        {
            var isValidUser = true;

            if (!isValidUser)
                return Unauthorized();

            var key = Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:Key"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = config.GetValue<string>("Jwt:Issuer"),
                Audience = config.GetValue<string>("Jwt:Audience"),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return Ok(new LoginResult
            {
                Token = jwtToken
            });
        }
    }

    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginResult
    {
        public string Token { get; set; }
    }
}
