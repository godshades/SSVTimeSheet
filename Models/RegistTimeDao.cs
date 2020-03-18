using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SSVTimeSheet.Models
{
    public class RegistTimeDao
    {
        SqlCommand cmd;
        SqlConnection sqlConnect;
        private string conn = "Server=192.168.10.220;Database=TimeSheet;User Id=waosa;Password=sqlSaPass;";

        public bool SaveWorkTime(RegistTime data)
        {
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            cmd = new SqlCommand("Sp_InsertWorkTime", sqlConnect);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                cmd.Parameters.AddWithValue("@UserId", data.UserId.ToString());
                cmd.Parameters.AddWithValue("@LeaderId", data.LeaderId.ToString());
                cmd.Parameters.AddWithValue("@ClassifyTime", data.ClassifyTime.ToString());
                cmd.Parameters.AddWithValue("@StartWorkTime", data.StartWorkTime.ToString());
                cmd.Parameters.AddWithValue("@EndWorkTime", data.EndWorkTime.ToString());
                cmd.Parameters.AddWithValue("@WorkTime", data.WorkTime.ToString());
                cmd.Parameters.AddWithValue("@Note", data.Note.ToString());
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
        public bool SaveRestTime(RegistTime data)
        {
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            cmd = new SqlCommand("Sp_InsertRestTime", sqlConnect);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                cmd.Parameters.AddWithValue("@UserId", data.UserId.ToString());
                cmd.Parameters.AddWithValue("@LeaderId", data.LeaderId.ToString());
                cmd.Parameters.AddWithValue("@StartRestTime", data.StartRestTime.ToString());
                cmd.Parameters.AddWithValue("@EndRestTime", data.EndRestTime.ToString());
                cmd.Parameters.AddWithValue("@RestTime", data.RestTime.ToString());
                cmd.Parameters.AddWithValue("@Note", data.Note.ToString());
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
