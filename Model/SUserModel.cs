using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SSVTimeSheet.Model
{
    public class SUserModel
    {
        private readonly SettingsModel _setting;

        public SUserModel(SettingsModel settings)
        {
            _setting = settings;
        }
       
        private SUser GetSUser(string id, string pass)
        {
            SUser user = new SUser();
            string sqlSelect = "Select * from SUser where UserId = @UserId and Password = @Password";
            using(var con = new SqlConnection(_setting.ConnectionStringId))
            {
                user = con.QueryFirstOrDefault<SUser>(sqlSelect, new { UserId = id, Password = pass });
            }

            return user;
        }
    }

    public class SUser
    {
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
