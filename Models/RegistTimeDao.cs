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

        public bool SaveTime(RegistTime data)
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
                cmd.Parameters.AddWithValue("@Reason", data.Reason.ToString());
                cmd.Parameters.AddWithValue("@NameContact", data.NameContact == null || data.NameContact == string.Empty ? "Không" : data.NameContact.ToString());
                cmd.Parameters.AddWithValue("@PhoneContact", data.PhoneContact == null || data.PhoneContact == string.Empty ? "Không" : data.PhoneContact.ToString());
                cmd.Parameters.AddWithValue("@Note", data.Note.ToString());
                cmd.Parameters.AddWithValue("@Status", 0); // Trạng thái chưa duyệt == 0, Không duyệt == 1, đã duyệt ==2;
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
        public List<RegistTime> GetAllTime(string userId)
        {
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            cmd = new SqlCommand("SELECT * FROM dbo.SRegistTime WHERE UserId ='" + userId + "'", sqlConnect);
            SqlDataReader reader = cmd.ExecuteReader();
            List<RegistTime> GetAll = new List<RegistTime>();
            RegistTime re = null;            
            try
            {
                if (reader.HasRows)
                {
                     while (reader.Read())
                    {
                        re = new RegistTime();
                        re.LeaderId = reader["LeaderId"].ToString();
                        re.ClassifyTime = int.Parse(reader["ClassifyTime"].ToString());
                        re.StartTime = DateTime.Parse(reader["StartTime"].ToString());
                        re.EndTime = DateTime.Parse(reader["EndTime"].ToString());
                        re.Time = float.Parse(reader["Time"].ToString());
                        re.Reason = int.Parse(reader["Reason"].ToString());
                        re.NameContact = reader["NameContact"].ToString();
                        re.PhoneContact = reader["PhoneContact"].ToString();
                        re.Note =  reader["Note"].ToString();
                        re.Status = int.Parse(reader["Status"].ToString());
                        GetAll.Add(re);
                    }
                    return GetAll;
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
