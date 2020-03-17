using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSVTimeSheet.Models;

namespace SSVTimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistTimeController : Controller
    {
        [HttpPost("InsertWorkTime")]
        public JsonResult InsertWorkTime(RegistTime data)
        {
            bool result = new RegistTimeDao().SaveWorkTime(data);
            return Json(result);
        }
        [HttpPost("InsertRestTime")]
        public JsonResult InsertRestTime(RegistTime data)
        {
            bool result = new RegistTimeDao().SaveRestTime(data);
            return Json(result);
        }
    }
}