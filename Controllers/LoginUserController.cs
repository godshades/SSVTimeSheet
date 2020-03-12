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
        [HttpPost]
        [Route("/loginUser/loginUserss")]
        public bool LoginUserss(string vinhnc, string aa)
        {
            string conn = "Server=192.168.10.220;Database=TimeSheet;User Id=waosa;Password=sqlSaPass;";
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                SqlCommand sql = new SqlCommand("SELECT * FROM SUser WHERE UserId= AND Password=",connection);

                try
                {                   
                    SqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        string file1 = reader["UserId"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return true;
        }

        public class Test
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}