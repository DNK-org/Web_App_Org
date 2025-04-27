using Azure_API_Test.Tokens;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Azure_API_Test.Request.Request;

namespace Azure_API_Test.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Token_Generator _tg;


        public AccountController(Token_Generator tg)
        {
        _tg = tg;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            if (login.Username == "admin" && login.Password == "password") // dummy validation
            {
                var token = _tg.GenerateToken(login.Username);
                var refreshToken = _tg.GenerateRefreshToken(); // New refresh token

                // Ideally, store this refresh token in database against the user for validation later
                // For demo, you can return it directly

                return Ok(new
                {
                    token,
                    refreshToken
                });
            }



            return Unauthorized();
        }
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest refreshRequest)
        {
            // Ideally, validate the refresh token from the database

            // For demo: Always assume refreshToken is valid

            if (refreshRequest.RefreshToken == null)
            {
                return BadRequest("Invalid refresh token");
            }

            var newAccessToken = _tg.GenerateToken("admin"); // Normally, retrieve username from DB
            var newRefreshToken = _tg.GenerateRefreshToken(); // Generate new refresh token also if you want to rotate

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

    }
}

