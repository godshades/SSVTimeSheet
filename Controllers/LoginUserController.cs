using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSVTimeSheet.Models;

namespace SSVTimeSheet.Controllers
{   
    public class LoginUserController : Controller
    {
        public Int16 INT_ZERO = 0;

        [HttpPost]
        [Route("/loginUser/loginUserss")]
        public bool LoginUserss(string username, string pass)
        {
            SUser s = new SUser();
            try
            {
                string conn = "Server=192.168.10.220;Database=TimeSheet;User Id=waosa;Password=sqlSaPass;";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    SqlCommand sql = new SqlCommand("SELECT * FROM SUser WHERE UserId='" + username + "' AND Password='" + pass + "'", connection);

                    SqlDataReader reader = sql.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            s.UserId = reader["UserId"].ToString();
                            s.Password = reader["Password"].ToString();
                            s.Name = reader["Name"].ToString();
                            s.TeamId = reader["TeamId"] == null || reader["TeamId"].ToString() == string.Empty ? INT_ZERO : Int16.Parse(reader["TeamId"].ToString());
                            s.UserRole = reader["UserRole"] == null || reader["UserRole"].ToString() == string.Empty ? INT_ZERO : Int16.Parse(reader["UserRole"].ToString());
                            s.CreateDate = reader["CreateDate"] == null || reader["CreateDate"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(reader["CreateDate"].ToString());
                            s.Birthday = reader["Birthday"] == null || reader["Birthday"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(reader["Birthday"].ToString());
                            s.Email = reader["Email"].ToString();
                            s.TelNo = reader["TelNo"].ToString();
                            s.RestDay = reader["RestDay"] == null || reader["RestDay"].ToString() == string.Empty ? INT_ZERO : Int16.Parse(reader["RestDay"].ToString());
                            s.DelFlg = reader["DelFlg"] == null || reader["DelFlg"].ToString() == string.Empty ? false : bool.Parse(reader["DelFlg"].ToString());
                        }
                    }
                    else
                    {
                        return false;
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }
                
            return true;
        }

        [HttpGet]
        [Route("/loginUser/getAll")]
        public JsonResult GetAll()
        {
            List<Test> list = new List<Test>();

            Test t = new Test();

            list.Add(new Test { id = 1, name = "Vinh", age = 18 });
            list.Add(new Test { id = 2, name = "Name", age = 19 });
            list.Add(new Test { id = 3, name = "Nguyễn", age = 10 });
            list.Add(new Test { id = 4, name = "Nguyễn", age = 10 });

            return new JsonResult(list);
        }

        public class Test
        {
            public int id { get; set; }
            public string name { get; set; }
            public int age { get; set; }
        }
        
    }
}