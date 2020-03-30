using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SSVTimeSheet.Model;

namespace SSVTimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private MySettingsConfiguration _config;

        public UserController(MySettingsConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string id, string pass)
        {
            IActionResult res = Unauthorized();
            SUserModel model = new SUserModel(_config);
            var user = model.GetSUser(id, pass);
            if (user == null)
            {
                res = NotFound();
            }
            else
            {
                var tokenString = GenerateToken(user);
                res = Ok(new { token = tokenString });
            }

            return res;
        }

        private string GenerateToken(SUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Jwt.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserId),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate,user.Birthday.ToString("yyyyMMdd"))
            };

            var token = new JwtSecurityToken(_config.Jwt.Issuer,
                _config.Jwt.Issuer,
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}