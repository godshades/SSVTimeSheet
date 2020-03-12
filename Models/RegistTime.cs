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
        public DateTime StartWorkTime { get; set; }
        public DateTime EndWorkTime { get; set; }
        public float WorkTime { get; set; }
        public DateTime StartRestTime { get; set; }
        public DateTime EndRestTime { get; set; }
        public int RestTime { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }



    }
}
