using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSVTimeSheet.Models
{
    public class RegistTime
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string LeaderId { get; set; }
        public string LeaderName { get; set; }
        public int JobType { get; set; }
        public string JobTypeName { get; set; }
        public DateTime StartDtTm { get; set; }
        public DateTime EndDtTm { get; set; }
        public string RestReasonName { get; set; }
        public int RestReasonId { get; set; }
        public string RestNameContact { get; set; }
        public string RestPhoneContact { get; set; }
        public float TotalTime { get; set; }        
        public string Note { get; set; }
        public DateTime InsertDt { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }



    }
}
