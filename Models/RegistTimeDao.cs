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
            cmd = new SqlCommand("Sp_InsertTime", sqlConnect);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                cmd.Parameters.AddWithValue("@UserId", data.UserId.ToString());
                cmd.Parameters.AddWithValue("@LeaderId", data.LeaderId.ToString());
                cmd.Parameters.AddWithValue("@ClassifyTime", data.ClassifyTime.ToString());
                cmd.Parameters.AddWithValue("@StartTime", data.StartTime.ToString());
                cmd.Parameters.AddWithValue("@EndTime", data.EndTime.ToString());
                cmd.Parameters.AddWithValue("@Time", data.Time.ToString());
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
            cmd = new SqlCommand("Sp_InsertTime", sqlConnect);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                cmd.Parameters.AddWithValue("@UserId", data.UserId.ToString());
                cmd.Parameters.AddWithValue("@LeaderId", data.LeaderId.ToString());
                cmd.Parameters.AddWithValue("@StartTime", data.StartTime.ToString());
                cmd.Parameters.AddWithValue("@EndTime", data.EndTime.ToString());
                cmd.Parameters.AddWithValue("@Time", data.Time.ToString());
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

        public RegistTime GetAllTime(string userId)
        {
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            cmd = new SqlCommand("SELECT * FROM dbo.SRegistTime WHERE UserId ='" + userId + "'", sqlConnect);
            SqlDataReader reader = cmd.ExecuteReader();
            RegistTime re = new RegistTime();            
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {                                                
                        re.LeaderId = reader["LeaderId"].ToString();
                        re.ClassifyTime = reader["ClassifyTime"].ToString();
                        re.StartTime = DateTime.Parse(reader["StartTime"].ToString());
                        re.EndTime = DateTime.Parse(reader["EndTime"].ToString());
                        re.Time = float.Parse(reader["Time"].ToString());                        
                        re.Note =  reader["Note"].ToString();
                        re.Status = int.Parse(reader["Status"].ToString());
                    }
                    return re;
                }
                else
                {
                    return null;
                }
            }
            //catch
            //{

            //    return null;
            //}
            finally
            {

                if (reader != null)
                {
                    reader.Close();
                }
                if (sqlConnect != null)
                {
                    sqlConnect.Close();
                }
            }

        }
    }
}
