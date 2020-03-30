using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SSVTimeSheet.Models
{
    public class SUserDao
    {
        SqlCommand cmd;
        SqlConnection sqlConnect;
        private string conn = "Server=192.168.10.220;Database=TimeSheet;User Id=waosa;Password=sqlSaPass;";
        public SUser CheckLogin(string UserId, string Password)
        {
            using (sqlConnect = new SqlConnection(conn))
            {
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
                            s.Name = reader["Name"].ToString();
                            s.LeadUser = reader["LeadUser"].ToString();
                            s.TypeId = reader["TypeId"] == null || reader["TypeId"].ToString() == string.Empty ? 0 : int.Parse(reader["TypeId"].ToString());
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

                }
            }

        }
        public List<SUser> GetAllLeader(int typeId)
        {
            using (sqlConnect = new SqlConnection(conn))
            {
                int leadId = 0;
                if (typeId == 2)
                {
                    leadId = 3;
                }
                else leadId = 2;
                sqlConnect.Open();
                cmd = new SqlCommand("SELECT [UserId], [Name] FROM dbo.SUser WHERE [TypeId] ='" + leadId + "'", sqlConnect);
                // Nếu là User(type != 2 thì query leader(type = 2), Nếu là leader(type = 2) query manager(type = 3)
                SqlDataReader reader = cmd.ExecuteReader();
                List<SUser> GetLeader = new List<SUser>();
                SUser s = null;

                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            s = new SUser();
                            s.UserId = reader["UserId"].ToString();
                            s.Name = reader["Name"].ToString();
                            GetLeader.Add(s);
                        }
                        return GetLeader;
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
                }
            }
        }
        
    }
}
