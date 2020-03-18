using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSVTimeSheet.Models;

namespace SSVTimeSheet.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : Controller
    {
        [HttpPost("Login")]
        public JsonResult Login(string UserId, string Password)
        {
            bool result = new SUserDao().CheckLogin(UserId, Password);
            return Json(result);
        }
    }
}