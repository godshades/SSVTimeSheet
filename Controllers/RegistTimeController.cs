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
    public class RegistTimeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Index();
        }
        public IActionResult workTime()
        {
            return workTime();
        }

    }
}