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
        public string UserName { get; set; }
        public string LeaderId { get; set; }
        public int ClassifyTime { get; set; }
        public string ClassifyName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ReasonName { get; set; }
        public int ReasonId { get; set; }
        public string NameContact { get; set; }
        public string PhoneContact { get; set; }
        public float Time { get; set; }        
        public string Note { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }



    }
}
