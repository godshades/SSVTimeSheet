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
        [HttpGet("GetTimeUser")]
        public JsonResult GetTimeUser(string userId)
        {
            List<RegistTime> result = new RegistTimeDao().GetTimeByUserId(userId);
            return Json(result);
        }
        [HttpGet("GetRequirementTime")]
        public JsonResult GetRequirementTime(string leaderId)
        {
            List<RegistTime> result = new RegistTimeDao().GetRequirementTime(leaderId);
            return Json(result);
        }
        [HttpGet("ApproveTime")]
        public JsonResult ApproveTime(int id, int status)
        {
            bool result = new RegistTimeDao().ApproveTime(id, status);
            return Json(result);
        }
        [HttpGet("SttApprove")]
        public JsonResult SttApprove(string leaderId, int sttapprove)
        {
            var result = new RegistTimeDao().SttApprove(leaderId, sttapprove);
            return Json(result);
        }
    }
}