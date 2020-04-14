using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSVTimeSheet.Models
{
    public class SUser
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string LeaderId { get; set; }        
        public string LeaderName { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public DateTime JoinDt { get; set; }
        public DateTime BirthDt { get; set; }
        public string Email { get; set; }
        public string TelNo { get; set; }
        public int RestDay { get; set; }
        public bool DelFlg { get; set; }
    }
}
