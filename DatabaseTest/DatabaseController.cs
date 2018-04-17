using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseTest
{
    class DatabaseController
    {
        private static string url = "EALSQL1.eal.local";
        private static string database = "DB2017_B12";
        private static string username = "USER_B12";
        private static string password = "SesamLukOp_12";

        public static void InsertQuery(string procedureName, Dictionary<string, object> args)
        {
            string connectionString = string.Format("Server={0}; Database={1}; User Id={2}; Password={3};", url, database, username, password);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                }
            }
        }
        

    }
}
