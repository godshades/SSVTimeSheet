using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSVTimeSheet.Models;

namespace SSVTimeSheet.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        [HttpGet("GetAllUser")]
        public JsonResult GetAllUser()
        {
            List<SUser> result = new SUserDao().GetAllUser();
            return Json(result);
        }

        [HttpGet("DeleteUser")]
        public JsonResult DeleteUser(string userId)
        {
            bool result = new SUserDao().DeleteUser(userId);
            return Json(result);
        }
        [HttpGet("GetAllLeader")]
        public JsonResult GetAllLeader()
        {
            List<SUser> result = new SUserDao().GetAllLeader();
            return Json(result);
        }
        [HttpGet("GetAllUserType")]
        public JsonResult GetAllUserType()
        {
            List<SType> result = new SUserDao().GetAllUserType();
            return Json(result);
        }
        [HttpGet("GetUserById")]
        public JsonResult GetUserById(string userId)
        {
            SUser result = new SUserDao().GetUserById(userId);
            return Json(result);
        }
        [HttpPost("AddUser")]
        public JsonResult AddUser(SUser data)
        {
            int result = new SUserDao().AddUser(data);
            return Json(result);
        }
        [HttpPost("EditUser")]
        public JsonResult EditUser(SUser data)
        {
            bool result = new SUserDao().EditUser(data);
            return Json(result);
        }
    }
}