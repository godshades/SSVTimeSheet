using System;
using System.Data.SqlClient;

namespace SSVTimeSheet.Models
{
    public class SUserDao
    {
        SqlCommand cmd;
        SqlConnection sqlConnect;
        private string conn = "Server=192.168.10.220;Database=TimeSheet;User Id=waosa;Password=sqlSaPass;";
        public SUser CheckLogin(string UserId , string Password)
        {
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            cmd = new SqlCommand("SELECT * FROM dbo.SUser WHERE UserId='" + UserId + "' AND Password='" + Password + "'", sqlConnect);
            SqlDataReader reader = cmd.ExecuteReader();
            SUser s = new SUser();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        s.UserId = reader["UserId"].ToString();
                        //s.Password = reader["Password"].ToString();
                        s.Name = reader["Name"].ToString();
                        s.TeamId = reader["TeamId"] == null || reader["TeamId"].ToString() == string.Empty ? 0 : int.Parse(reader["TeamId"].ToString());
                        s.UserRole = reader["UserRole"] == null || reader["UserRole"].ToString() == string.Empty ? 0 : int.Parse(reader["UserRole"].ToString());
                        s.CreateDate = reader["CreateDate"] == null || reader["CreateDate"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(reader["CreateDate"].ToString());
                        s.Birthday = reader["Birthday"] == null || reader["Birthday"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(reader["Birthday"].ToString());
                        s.Email = reader["Email"].ToString();
                        s.TelNo = reader["TelNo"].ToString();
                        s.RestDay = reader["RestDay"] == null || reader["RestDay"].ToString() == string.Empty ? 0 : int.Parse(reader["RestDay"].ToString());
                        s.DelFlg = reader["DelFlg"] == null || reader["DelFlg"].ToString() == string.Empty ? false : bool.Parse(reader["DelFlg"].ToString());
                    }                    
                    return s;
                }
                else
                {
                    return null;
                }
            }
            catch
            {

                return null;
            }
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
