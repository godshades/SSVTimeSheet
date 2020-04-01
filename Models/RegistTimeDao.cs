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
                        re.Id = int.Parse(reader["Id"].ToString());
                        re.ClassifyTime = int.Parse(reader["ClassifyTime"].ToString());
                        re.StartTime = DateTime.Parse(reader["StartTime"].ToString());
                        re.EndTime = DateTime.Parse(reader["EndTime"].ToString());
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
            if (status == 1)
            {
                nameQuery = "LeaderId";
            }
            else if (status == 3)
            {
                nameQuery = "ManagerId";
            }
            // nếu status = 1 thì truy vấn cho leader 
            // status bằng 3 thì truy vấn cho manager
            sqlConnect = new SqlConnection(conn);
            sqlConnect.Open();
            cmd = new SqlCommand("SELECT *,(SELECT  TypeName FROM SType t where r.ReasonId = t.TypeId and GroupId = '3') as ReasonName FROM dbo.SRegistTime r JOIN dbo.SUser u ON r.UserId = u.UserId WHERE [Status] = '" + status + "' AND " + nameQuery + " = '" + leaderId + "'", sqlConnect);
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
        public RegistTime GetRegistDetail(int registId)
        {
            using(sqlConnect = new SqlConnection(conn))
            {
                string sub1 = ",(select T.TypeName from dbo.SType as T where T.TypeId = R.ReasonId and GroupId = '3')  as ReasonName";
                string sub2 = ",(select T.TypeName from dbo.SType as T where T.TypeId = R.ClassifyTime and GroupId = '1') as ClassifyName";
                string sub3 = ",(SELECT T.TypeName FROM SType as T WHERE T.TypeId = R.[Status] AND GroupId = '4') AS StatusName";
                string sub4 = ",(select U.[Name] from dbo.SUser as U where U.UserId = R.LeaderId) as LeaderName";
                sqlConnect.Open();
                cmd = new SqlCommand("SELECT * "+sub1+sub2+sub3+sub4+  " FROM [dbo].[SRegistTime] as R WHere Id= '" + registId  + "'", sqlConnect);
                SqlDataReader reader = cmd.ExecuteReader();
                RegistTime re = new RegistTime();
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            re.Id = int.Parse(reader["Id"].ToString());
                            re.LeaderName = reader["LeaderName"].ToString();
                            re.LeaderId = reader["LeaderId"].ToString();
                            re.ClassifyTime = int.Parse(reader["ClassifyTime"].ToString());
                            re.ClassifyName = reader["ClassifyName"].ToString();
                            re.StartTime = DateTime.Parse(reader["StartTime"].ToString());
                            re.EndTime = DateTime.Parse(reader["EndTime"].ToString());
                            re.Time = float.Parse(reader["Time"].ToString());
                            re.ReasonId = int.Parse(reader["ReasonId"].ToString());
                            re.ReasonName = reader["ReasonName"].ToString();
                            re.NameContact = reader["NameContact"].ToString();
                            re.PhoneContact = reader["PhoneContact"].ToString();
                            re.Note = reader["Note"].ToString();
                            re.StatusName = reader["StatusName"].ToString();                           
                        }
                        return re;
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
            cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.SRegistTime WHERE Status ='" + sttapprove + "' AND " + nameQuery + " = '" + leaderId + "'", sqlConnect);
            Int32 result = (Int32)cmd.ExecuteScalar();
            return result;
        }
        public List<SType> GetReason()
        {
            using (sqlConnect = new SqlConnection(conn))
            {
                sqlConnect.Open();
                cmd = new SqlCommand("SELECT [TypeId],[TypeName] FROM [dbo].[SType] Where GroupId = 3", sqlConnect);
                SqlDataReader reader = cmd.ExecuteReader();
                SType t = null;
                List<SType> reason = new List<SType>();
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            t = new SType();
                            t.TypeId = int.Parse(reader["TypeId"].ToString());
                            t.TypeName = reader["TypeName"].ToString();
                            reason.Add(t);
                        }
                        return reason;
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
