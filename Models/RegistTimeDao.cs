using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SSVTimeSheet.Models
{
    public class RegistTimeDao
    {
        SqlCommand cmd;
        SqlConnection sqlConnect;
        string conn = "Server=192.168.10.220;Database=TimeSheet;User Id=waosa;Password=sqlSaPass;";

        public bool SaveWorkTime(RegistTime data)
        {
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            
            try
            {


                cmd = new SqlCommand("insert into SRegistTime (UserId,LeaderId,ClassifyTime,StartWorkTime,EndWorkTime,WorkTime,Note) values ('"
                + data.UserId + "','"
                + data.LeaderId + "','"
                + data.ClassifyTime + "','"
                + data.StartWorkTime + "','"
                + data.EndWorkTime + "','"
                + data.WorkTime + "','"
                + data.Note+ "')", sqlConnect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {                
                return false;
            }
            finally
            {
                sqlConnect.Close();
            }


        }

    }
}
