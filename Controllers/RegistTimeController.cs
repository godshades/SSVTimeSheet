using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSVTimeSheet.Models;

namespace SSVTimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class RegistTimeController : Controller
    {
        [HttpPost("InsertTime")]
        public JsonResult InsertWorkTime(RegistTime data)
        {
            bool result = new RegistTimeDao().SaveTime(data);
            return Json(result);
        }        
        [HttpPost("GetTimeUser")]
        public JsonResult GetTimeUser(string userId)
        {
            List<RegistTime> result = new RegistTimeDao().GetAllTime(userId);
            return Json(result);
        }
    }
}