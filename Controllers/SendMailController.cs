using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [Route("/sendMail/sendDataToMail")]
        public bool SendDataToMail([FromBody]ReportEmployee report)
        {
            string[] task_p = report.Percent.Split('\n');

            StringBuilder sb = new StringBuilder();

            sb.Append("Báo cáo công việc trong ngày hôm nay@@");
            sb.Append("1) Nội dung công việc thực hiện ngày hôm nay:@");
            sb.Append("   - " + report.Content+"@");
            sb.Append("2) Mức độ hoàn thành công việc (%):@");
            for (int i = 0; i < task_p.Length; i++)
            {
                sb.Append("   - " + task_p[i] + "@");
            }          
            sb.Append("3) Vấn đề phát sinh:@");
            if(!String.IsNullOrEmpty(report.Err.Replace(" ", string.Empty)))
            {
                sb.Append("   - " + report.Err + "@");
            }        
            sb.Append("4) Kế hoạch tiếp theo:@");
            sb.Append("   - " + report.Tmcontent+"@");
            sb.Append("Xin cảm ơn!");

            sb = sb.Replace("@", Environment.NewLine);

            try
            {
                SmtpClient client = new SmtpClient("saisystem.vn", 587);

                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(report.Mail, report.Pass);

                MailMessage message = new MailMessage(report.Mail, report.Mailsender);
                message.Subject = "Báo cáo công việc";
                message.Body = sb.ToString();

                client.Send(message);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public class ReportEmployee
        {
            public string Name { get; set; }

            public string Mail { get; set; }

            public string Pass { get; set; }

            public string Mailsender { get; set; }

            public string Content { get; set; }

            public string Percent { get; set; }

            public string Err { get; set; }

            public string Tmcontent { get; set; }
        }
    }
}