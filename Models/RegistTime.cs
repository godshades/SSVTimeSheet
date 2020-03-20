using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSVTimeSheet.Models
{
    public class RegistTime
    {
        public string UserId { get; set; }
        public string LeaderId { get; set; }
        public string ClassifyTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float Time { get; set; }        
        public string Note { get; set; }
        public int Status { get; set; }



    }
}
