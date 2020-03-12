﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSVTimeSheet.Models
{
    public class SUser
    {
        public SUser()
        {

        }

        public string UserId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Int16 TeamId { get; set; }
        public Int16 UserRole { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string TelNo { get; set; }
        public Int16 RestDay { get; set; }
        public bool DelFlg { get; set; }
    }
}
