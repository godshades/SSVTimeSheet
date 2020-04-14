using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SSVTimeSheet.Models
{
    public class TimeSheetRepository
    {
        private string connectionString;        
        public TimeSheetRepository()
        {
            connectionString = @"Server=QUANGQUYEN;Database=SSVTimeSheet;User Id=sa;Password=123456; Connection Timeout = 100000;";
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
    }
}
