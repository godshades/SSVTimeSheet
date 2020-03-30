using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSVTimeSheet.Model
{
    public class MySettingsConfiguration
    {
        public string ConnectionStringId { get; set; }
        public Jwt Jwt { get; set; }
    }
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
    }
}
