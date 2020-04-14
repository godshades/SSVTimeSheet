using Dapper;
using SSVTimeSheet.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SSVTimeSheet.Models
{
    public class SUserDao
    {
        public SUser CheckLogin(string UserId, string Password)
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {
                string sQuery = @"SELECT * , (SELECT TypeValueName FROM dbo.SType t WHERE u.UserTypeId = t.TypeValue AND t.TypeCd = '2') AS [UserTypeName] FROM dbo.SUser u WHERE UserId= @UserId AND Password=@Password";
                dbConnection.Open();
                try
                {
                    SUser s = dbConnection.QueryFirstOrDefault<SUser>(sQuery, new { UserId = UserId, Password = Password });
                    s.Password = null; // Để không trả về password cho client
                    return s;
                }
                catch
                {
                    return null;
                  
                }
               
            }

        }
        public List<SUser> GetLeaderByUser(int typeId)
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {
                int leadId = 0;
                if (typeId == 2 || typeId == 1 || typeId == 3)
                {
                    leadId = 4;
                }
                else leadId = 5;
                string sQuery = @"SELECT [UserId], [FullName] FROM dbo.SUser WHERE [UserTypeId] = @leadId";
                dbConnection.Open();
                try
                {
                    List<SUser> GetLeader = dbConnection.Query<SUser>(sQuery, new { leadId = leadId }).ToList();
                    return GetLeader;
                }
                catch
                {

                    return null;
                }
            }
        }
        public List<SUser> GetAllUser()
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {
                string sQuery = @"SELECT *,(SELECT FullName FROM dbo.SUser s WHERE u.LeaderId = s.UserId) AS [LeaderName],(SELECT TypeValueName FROM dbo.SType t WHERE u.UserTypeId = t.TypeValue AND t.TypeCd = '2') AS [UserTypeName] FROM [SSVTimeSheet].[dbo].[SUser] u WHERE u.DelFlg = 'false'";
                dbConnection.Open();
                try
                {
                    List<SUser> result = dbConnection.Query<SUser>(sQuery).ToList();
                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }
        public bool DeleteUser(string userId)
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {
                string sQuery = @"UPDATE dbo.SUser SET DelFlg = 'true' WHERE UserId = @UserId";
                dbConnection.Open();
                try
                {
                    dbConnection.Execute(sQuery, new { UserId = userId });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public List<SUser> GetAllLeader()
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {
                string sQuery = @"SELECT [UserId], [FullName] FROM dbo.SUser WHERE [UserTypeId] = '4' OR [UserTypeId] = '5' OR [UserTypeId] = '6'";
                dbConnection.Open();
                try
                {
                    List<SUser> result = dbConnection.Query<SUser>(sQuery).ToList();
                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }
        public List<SType> GetAllUserType()
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {
                string sQuery = @"SELECT * FROM dbo.SType WHERE TypeCd ='2'";
                dbConnection.Open();
                try
                {
                    List<SType> result = dbConnection.Query<SType>(sQuery).ToList();
                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }
        public SUser GetUserById(string userId)
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {
                string sQuery = @"SELECT *,(SELECT FullName FROM dbo.SUser s WHERE u.LeaderId = s.UserId) AS [LeaderName],(SELECT TypeValueName FROM dbo.SType t WHERE u.UserTypeId = t.TypeValue AND t.TypeCd = '2') AS [UserTypeName] FROM [SSVTimeSheet].[dbo].[SUser] u WHERE UserId = @UserId";
                dbConnection.Open();
                try
                {
                    SUser result = dbConnection.QueryFirstOrDefault<SUser>(sQuery, new { UserId = userId });
                    return result;
                }
                catch
                {

                    return null;
                }
            }
        }
        public int AddUser(SUser data)
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {
                string checkQuery = @"SELECT COUNT(*) FROM dbo.SUser WHERE UserId = @UserId";
                string sQuery = @"INSERT INTO dbo.SUser( UserId ,[Password] ,FullName ,LeaderId ,UserTypeId ,JoinDt ,BirthDt ,Email ,TelNo ,RestDay ,DelFlg)VALUES  ( @UserId ,@Password ,@FullName ,@LeaderId ,@UserTypeId ,@JoinDt ,@BirthDt ,@Email ,@TelNo ,@RestDay ,@DelFlg) ";
                dbConnection.Open();
                try
                {

                    int checkUser = dbConnection.ExecuteScalar<int>(checkQuery, new { UserId = data.UserId });
                    if (checkUser > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        data.Password = Encryptor.MD5Hash(data.Password);
                        data.DelFlg = false;
                        dbConnection.Execute(sQuery, data);
                        return 2;
                    }

                }
                catch
                {
                    return 0;
                }
            }
        }
        public bool EditUser(SUser data)
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {

                string sQuery = @"UPDATE dbo.SUser SET FullName = @FullName , LeaderId = @LeaderId, UserTypeId = @UserTypeId, JoinDt = @JoinDt,BirthDt = @BirthDt, Email = @Email, TelNo = @TelNo, RestDay = @RestDay WHERE UserId = @UserId";
                dbConnection.Open();
                try
                {                   
                    dbConnection.Execute(sQuery, data);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Client
        /// </summary>
        /// <returns>bool</returns>
        public bool UpdateUser(SUser data)
        {
            using (IDbConnection dbConnection = new TimeSheetRepository().Connection)
            {

                string sQuery = @"UPDATE dbo.SUser SET FullName = @FullName ,BirthDt = @BirthDt, Email = @Email, TelNo = @TelNo WHERE UserId = @UserId";
                dbConnection.Open();
                try
                {
                    dbConnection.Execute(sQuery, data);
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
