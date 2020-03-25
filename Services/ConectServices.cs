using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SSVTimeSheet.Services
{
    public class ConectServices
    {
        SqlConnection sqlConnect;
        public SqlCommand OpenConection(string ProcName)
        {
            try
            {
                string conn = "Server=192.168.10.220;Database=TimeSheet;User Id=waosa;Password=sqlSaPass;";
                sqlConnect = new SqlConnection(conn);
                sqlConnect.Open();
                SqlCommand cmd = new SqlCommand(ProcName, sqlConnect);
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd;
            }
            catch
            {
                return null;
            }

            
        }
        public void CloseConection()
        {
            try
            {
                sqlConnect.Close();
            }
            catch
            {
                
            }
        }
    }
}
