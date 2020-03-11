using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SSVTimeSheet.Controllers
{
    public class SendMailController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SendDataToMail([FromBody]ReportEmployee report)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Dear A.Hùng@@");
            sb.Append("Báo cáo công việc trong ngày hôm nay@@");
            sb.Append("1) Nội dung công việc thực hiện ngày hôm nay:@");
            sb.Append("   - " + report.Content);
            sb.Append("2) Mức độ hoàn thành công việc (%):@");
            sb.Append("   - " + report.Percent);
            sb.Append("3) Vấn đề phát sinh:@");
            sb.Append("   - " + report.Err);
            sb.Append("4) Kế hoạch tiếp theo:@");
            sb.Append("   - " + report.Tmcontent);
            sb.Append("Nhờ anh xác nhận giúp em. Em xin cảm ơn!");

            sb = sb.Replace("@", Environment.NewLine);

            try
            {
                SmtpClient client = new SmtpClient("saisystem.vn", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("vinhnc@saisystem.vn", "20150601");

                MailMessage message = new MailMessage("vinhnc@saisystem.vn", "quyennq@saisystem.vn");
                message.Subject = "Báo cáo công việc";
                message.Body = sb.ToString();

                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        public class ReportEmployee
        {
            public string Name { get; set; }

            public string Mail { get; set; }

            public string Mailsender { get; set; }

            public string Content { get; set; }

            public string Percent { get; set; }

            public string Err { get; set; }

            public string Tmcontent { get; set; }
        }
    }
}