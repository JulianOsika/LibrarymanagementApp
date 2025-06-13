using LibraryManagement.Presentation.WebAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace LibraryManagement.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private string GenerateToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperBezpiecznyKluczJWT_doTokenów123456!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            if (loginDto.Username == "admin" && loginDto.Password == "admin123")
                return Ok(GenerateToken("admin", "Admin"));
            else if (loginDto.Username == "user" && loginDto.Password == "user123")
                return Ok(GenerateToken("user", "User"));
            else return Unauthorized();
        }
    }
}
