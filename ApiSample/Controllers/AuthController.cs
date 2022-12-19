using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiSample.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase {
    private readonly IConfiguration config;

    public AuthController(IConfiguration config) {
        this.config = config;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginModel model) {
        if(
            model.Username.Equals("liron", StringComparison.InvariantCultureIgnoreCase) &&
            model.Pwd.Equals("123456", StringComparison.InvariantCultureIgnoreCase)
         ) {

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, "18"),
                new Claim(ClaimTypes.Name, "Liron Cohen"),
                new Claim(ClaimTypes.DateOfBirth, "42")
            };

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(180),
                Issuer = "Sela",
                //Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Auth:Secret"])),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return Ok(stringToken);

            //return Ok(18);
        }

        return Unauthorized();
    }
}
