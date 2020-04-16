using Dapper;
using GoogleCalendarAPI.Models;
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
        IDbConnection dbConnection = new TimeSheetRepository().Connection;
        public bool SaveTime(RegistTime data)
        {
            using (dbConnection)
            {
                //try
                //{
                    string sQuery = @"INSERT INTO dbo.SWorkTime( UserId ,JobType ,StartDtTm ,EndDtTm ,LeaderId ,RestReasonId ,RestNameContact ,RestPhoneContact ,TotalTime ,Note ,InsertDt ,[Status]) OUTPUT Inserted.Id VALUES  (@UserId ,@JobType ,@StartDtTm ,@EndDtTm ,@LeaderId ,@RestReasonId ,@RestNameContact ,@RestPhoneContact ,@TotalTime ,@Note ,@InsertDt ,@Status)";
                    dbConnection.Open();
                    int Id = dbConnection.QuerySingle<int>(sQuery, data);
                    if (Id != 0)
                    {
                        TimeSheetEvent gEvent = new TimeSheetEvent();
                        gEvent.id = "ssvtime" + Id.ToString();
                        gEvent.text = data.JobType == 1 ? "Làm thêm": "Xin nghỉ" ;
                        gEvent.description = data.Note;
                        gEvent.Created = data.InsertDt;
                        gEvent.start_date = data.StartDtTm;
                        gEvent.end_date = data.EndDtTm;
                        string temp = new GoogleCalendar().CreateEvent(gEvent);
                    
                    }

                    return true;
                //}
                //catch
                //{
                //    return false;
                //}
            }



        }
        public List<RegistTime> GetTimeByUserId(string userId)
        {
            using (dbConnection)
            {
                string sQuery = @"SELECT * FROM dbo.SWorkTime WHERE UserId = @UserId";
                dbConnection.Open();
                try
                {
                    List<RegistTime> GetAll = dbConnection.Query<RegistTime>(sQuery, new { userId = userId }).ToList();
                    return GetAll;
                }
                catch
                {
                    return null;
                }
            }

        }
        public List<RegistTime> GetRequirementTime(string leaderId, int status)
        {
            using (dbConnection)
            {
                string sQuery = "";
                if (status == 1)
                {
                    sQuery = @"SELECT *,(SELECT  TypeValueName FROM SType t where r.RestReasonId = t.TypeValue and TypeCd = '3') AS RestReasonName , (SELECT TypeValueName FROM SType WHERE TypeValue = r.JobType AND TypeCd = '1') AS JobTypeName FROM dbo.SWorkTime r JOIN dbo.SUser u ON r.UserId = u.UserId WHERE r.[Status] = @Status AND r.LeaderId = @LeaderId";
                }
                else if (status == 3)
                {
                    sQuery = @"SELECT *,(SELECT  TypeValueName FROM SType t where r.RestReasonId = t.TypeValue and TypeCd = '3') AS RestReasonName , (SELECT TypeValueName FROM SType WHERE TypeValue = r.JobType AND TypeCd = '1') AS JobTypeName FROM dbo.SWorkTime r JOIN dbo.SUser u ON r.UserId = u.UserId WHERE r.[Status] = @Status AND r.LeaderId = (SELECT u.UserId FROM dbo.SUser u WHERE u.LeaderId = @LeaderId AND r.LeaderId = u.UserId)";
                }
                // nếu status = 1 thì truy vấn cho leader
                // status bằng 3 thì truy vấn cho manager
                List<RegistTime> GetAll = new List<RegistTime>();
                dbConnection.Open();
                try
                {
                    List<RegistTime> result = dbConnection.Query<RegistTime>(sQuery, new { Status = status, LeaderId = leaderId }).ToList();
                    return result;
                }
                catch
                {

                    return null;
                }
            }


        }
        public RegistTime GetRegistDetail(int registId)
        {
            using (dbConnection)
            {
                string sub1 = ",(select T.TypeValueName from dbo.SType as T where T.TypeValue = R.RestReasonId and TypeCd = '3')  as RestReasonName";
                string sub2 = ",(select T.TypeValueName from dbo.SType as T where T.TypeValue = R.JobType and TypeCd = '1') as JobTypeName";
                string sub3 = ",(SELECT T.TypeValueName FROM SType as T WHERE T.TypeValue = R.[Status] AND TypeCd = '4') AS StatusName";
                string sub4 = ",(select U.[FullName] from dbo.SUser as U where U.UserId = R.LeaderId) as LeaderName";
                string sQuery = @"SELECT * " + sub1 + sub2 + sub3 + sub4 + " FROM [dbo].[SWorkTime] as R WHERE Id= @registId";
                dbConnection.Open();
                try
                {
                    return dbConnection.QueryFirstOrDefault<RegistTime>(sQuery, new { registId = registId });
                }
                catch
                {

                    return null;
                }
            }
        }
        public bool ApproveTime(int id, int status)
        {
            using (dbConnection)
            {
                string sQuery = @"UPDATE dbo.SWorkTime SET [Status] = @Status WHERE Id = @Id";
                dbConnection.Open();
                try
                {
                    dbConnection.Execute(sQuery, new { Id = id, Status = status });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public int SttApprove(string leaderId, int sttapprove)
        {

            using (dbConnection)
            {
                string sQuery = "";
                if (sttapprove == 1)
                {
                    sQuery = @"SELECT COUNT(*) FROM dbo.SWorkTime WHERE [Status] = @Status AND LeaderId = @LeaderId";
                }
                else if (sttapprove == 3)
                {
                    sQuery = @"SELECT COUNT(*) FROM dbo.SWorkTime r WHERE [Status] = @Status AND LeaderId = (SELECT u.UserId FROM dbo.SUser u WHERE u.LeaderId = @LeaderId AND r.LeaderId = u.UserId)";
                }
                // nếu status = 0 thì truy vấn cho leader 
                // status bằng 2 thì truy vấn cho manager
                dbConnection.Open();
                try
                {
                    int result = dbConnection.ExecuteScalar<int>(sQuery, new { Status = sttapprove, LeaderId = leaderId });
                    return result;
                }
                catch
                {

                    return 0;
                }

            }
        }
        public List<SType> GetReason()
        {
            using (dbConnection)
            {

                string sQuery = @"SELECT [TypeValue],[TypeValueName] FROM [dbo].[SType] Where TypeCd = 3";
                dbConnection.Open();
                try
                {
                    List<SType> reason = dbConnection.Query<SType>(sQuery).ToList();
                    return reason;
                }
                catch
                {

                    return null;
                }
            }
        }
        public bool UpdateRegist(RegistTime data)
        {
            using (dbConnection)
            {
                string sQuery = @"UPDATE dbo.SWorkTime SET UserId = @UserId, LeaderId = @LeaderId, JobType = @JobType, StartDtTm = @StartDtTm, EndDtTm = @EndDtTm, [TotalTime] = @TotalTime ,RestReasonId = @RestReasonId, RestNameContact = @RestNameContact ,RestPhoneContact = @RestPhoneContact, Note = @Note, [Status] = @Status WHERE Id = @Id";
                dbConnection.Open();
                try
                {
                    data.Status = 1;

                    dbConnection.ExecuteScalar<RegistTime>(sQuery, data);
                    TimeSheetEvent gEvent = new TimeSheetEvent();
                    gEvent.id = "ssvtime" + data.Id.ToString();
                    gEvent.text = data.JobType == 1 ? "Làm thêm" : "Xin nghỉ";
                    gEvent.description = data.Note;
                    gEvent.Updated = DateTime.Now;
                    gEvent.start_date = data.StartDtTm;
                    gEvent.end_date = data.EndDtTm;
                    new GoogleCalendar().UpdateEvent(gEvent);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool DeleteTime(int id)
        {
            using (dbConnection)
            {
                string sQuery = @"DELETE FROM dbo.SWorkTime WHERE Id = @id";
                dbConnection.Open();
                try
                {
                    dbConnection.Execute(sQuery, new { id = id });
                    string eventId = "ssvtime" + id.ToString();
                    new GoogleCalendar().DeleteEvent(eventId);
                    return true;
                }
                catch
                {

                    return false;
                }
            }
        }
    }
}
