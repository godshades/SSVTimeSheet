using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSVTimeSheet.Controllers
{
    public class SendMailController : Controller
    {
        [HttpPost]
        public IActionResult SendDataToMail(ReportEmployee e)
        {
            int a = e.ReportId;

            return View();
        }

        public class ReportEmployee
        {
            public int ReportId { get; set; }
        }
    }
}