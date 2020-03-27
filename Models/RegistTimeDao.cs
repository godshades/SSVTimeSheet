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
                cmd.Parameters.AddWithValue("@ReasonId", data.ReasonId.ToString());
                cmd.Parameters.AddWithValue("@NameContact", data.NameContact == null || data.NameContact == string.Empty ? "Không" : data.NameContact.ToString());
                cmd.Parameters.AddWithValue("@PhoneContact", data.PhoneContact == null || data.PhoneContact == string.Empty ? "Không" : data.PhoneContact.ToString());
                cmd.Parameters.AddWithValue("@Note", data.Note.ToString());
                cmd.Parameters.AddWithValue("@Status", 1); // Trạng thái chưa duyệt == 1, Không duyệt == 2,Lead đã duyệt ==3, manager duyệt = 4;
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
        public List<RegistTime> GetTimeByUserId(string userId)
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
                        re.ReasonId = int.Parse(reader["ReasonId"].ToString());
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
        public List<RegistTime> GetRequirementTime(string leaderId, int status)
        {
            string nameQuery = "";
            if(status == 1)
            {
                nameQuery = "LeaderId";
            }else if(status == 3)
            {
                nameQuery = "ManagerId";
            }
            // nếu status = 1 thì truy vấn cho leader 
            // status bằng 3 thì truy vấn cho manager
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            cmd = new SqlCommand("SELECT *,(SELECT  TypeName FROM SType t where r.ReasonId = t.TypeId and GroupId = '3') as ReasonName FROM dbo.SRegistTime r JOIN dbo.SUser u ON r.UserId = u.UserId WHERE [Status] = '" + status +"' AND "+ nameQuery + " = '"+ leaderId + "'", sqlConnect);
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
                        re.Id = int.Parse(reader["Id"].ToString());
                        re.UserName = reader["Name"].ToString();                        
                        re.ClassifyTime = int.Parse(reader["ClassifyTime"].ToString());
                        re.StartTime = DateTime.Parse(reader["StartTime"].ToString());
                        re.EndTime = DateTime.Parse(reader["EndTime"].ToString());
                        re.Time = float.Parse(reader["Time"].ToString());                       
                        re.ReasonName = reader["ReasonName"].ToString();
                        re.NameContact = reader["NameContact"].ToString();
                        re.PhoneContact = reader["PhoneContact"].ToString();
                        re.Note = reader["Note"].ToString();
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
        public bool ApproveTime(int id, int status)
        {            
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            cmd = new SqlCommand("Sp_UpdateSttRegistTime", sqlConnect);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@Id", id.ToString());
                cmd.Parameters.AddWithValue("@Status", status.ToString());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {               
                if (sqlConnect != null)
                {
                    sqlConnect.Close();
                }
            }
        }        
        public int SttApprove(string leaderId, int sttapprove)
        {
            string nameQuery = "";
            if (sttapprove == 1)
            {
                nameQuery = "LeaderId";                
            }
            else if (sttapprove == 3)
            {
                nameQuery = "ManagerId";
            }
            // nếu status = 0 thì truy vấn cho leader 
            // status bằng 2 thì truy vấn cho manager
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.SRegistTime WHERE Status ='"+ sttapprove+"' AND "+ nameQuery +" = '" + leaderId + "'", sqlConnect);           
            Int32 result = (Int32)cmd.ExecuteScalar();           
            return result;
        }
    }
}
