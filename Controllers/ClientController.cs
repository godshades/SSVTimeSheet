using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSVTimeSheet.Models;

namespace SSVTimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        [HttpPost("UpdateUser")]
        public JsonResult UpdateUser(SUser data)
        {
            bool result = new SUserDao().UpdateUser(data);
            return Json(result);
        }
    }
}