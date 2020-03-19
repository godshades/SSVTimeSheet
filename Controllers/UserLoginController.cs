using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SSVTimeSheet.Models;


namespace SSVTimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public UserLoginController(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateJSONWebToken(SUser userInfo)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] 
            {
            new Claim(JwtRegisteredClaimNames.NameId, userInfo.UserId),
            new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Name),
            new Claim("Role", userInfo.UserRole.ToString()),
            new Claim("TeamId", userInfo.UserRole.ToString()),
            };


            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Authencate(LoginRequest request)
            {

            if (!ModelState.IsValid)
            {
                return Ok(new { StatusCode = 400 });
            }
            var user = new SUserDao().CheckLogin(request);
            if (user == null)
            {
                return Ok(new { StatusCode = 400 });
            }
            var resultToken = GenerateJSONWebToken(user);
            return Ok(new { token = resultToken });
        }
    }
}