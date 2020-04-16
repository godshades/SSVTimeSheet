using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSVTimeSheet.Models;
using GoogleCalendarAPI.Models;

namespace SSVTimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class RegistTimeController : Controller
    {       
        [HttpPost("InsertTime")]
        public JsonResult InsertWorkTime(RegistTime data)
        {
            data.InsertDt = DateTime.Now;
            data.Status = 1;
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
        public JsonResult GetRequirementTime(string leaderId, int status)
        {
            List<RegistTime> result = new RegistTimeDao().GetRequirementTime(leaderId, status);
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
        [HttpGet("GetLeaderByUser")]
        public JsonResult GetLeaderByUser(int userTypeID)
        {
            List<SUser> result = new SUserDao().GetLeaderByUser(userTypeID);
            return Json(result);
        }
        [HttpGet("GetReason")]
        public JsonResult GetReason()
        {
            List<SType> result = new RegistTimeDao().GetReason();
            return Json(result);
        }
        [HttpGet("GetRegistDetail")]
        public JsonResult GetRegistDetail(int registId)
        {
            RegistTime result = new RegistTimeDao().GetRegistDetail(registId);
            return Json(result);
        }
        [HttpPost("UpdateRegist")]
        public JsonResult UpdateRegist(RegistTime data)
        {
            bool result = new RegistTimeDao().UpdateRegist(data);
            
            return Json(data);
        }
        [HttpDelete("DeleteTime")]
        public JsonResult DeleteTime(int id)
        {
            bool result = new RegistTimeDao().DeleteTime(id);            
            return Json(result);
        }
    }
}