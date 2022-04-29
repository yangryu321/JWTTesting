using JWTTesting.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {

        [HttpGet("GetFruits")]

        public IEnumerable<string> GetFruits()
        {
            return new string[] { "Apple", "Banana" };
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] LoginModel model)
        {
            if(model.Email == "123@gmail.com" && model.Password == "123456")
            {
                //generate and return a JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("My_secret_key_HAHAHAHAHHAHAHAHAHAHAHA");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenstring = tokenHandler.WriteToken(token);


                return Ok(new { Token = tokenstring });


            }
            else
            {
                return NotFound();
            }

        }



    }
}
