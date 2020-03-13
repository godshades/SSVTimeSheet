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
        [HttpPost("InsertTime")]        
        public JsonResult InsertTime(RegistTime data)
        {            
          bool result =  new RegistTimeDao().SaveWorkTime(data);
            if (result)
            {
                return Json(data);
            }
            else
            {
                return Json(result);
            }

            
        }
    }
}