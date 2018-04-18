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
                    foreach (KeyValuePair<string, object> arg in args)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + arg.Key, arg.Value));
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("FEJL: " + e.Message);
                    throw e;
                }
            }
        }
        public static object[][] RetrieveQuery(string procedureName, Dictionary<string, object> args)
        {
            string connectionString = string.Format("Server={0}; Database={1}; User Id={2}; Password={3};", url, database, username, password);
            using (SqlConnection conn = new SqlConnection(connectionString))
            { 
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (KeyValuePair<string, object> arg in args)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + arg.Key, arg.Value));
                    }

                    SqlDataReader itemReader = cmd.ExecuteReader();
                    List<object> cols = new List<object>();
                    List<object[]> rows = new List<object[]>();
                    if (itemReader.HasRows)
                    {
                        for (int i = 0; i < itemReader.FieldCount; i++)
                        {
                            cols.Add(itemReader.GetName(i));
                        }

                        while (itemReader.Read())
                        {
                            object[] row = new object[itemReader.FieldCount];
                            itemReader.GetValues(row);
                            rows.Add(row);
                        }
                    }

                    object[][] result = new object[1 + rows.Count][];
                    result[0] = cols.ToArray();
                    for (int i = 0; i < rows.Count; i++)
                    {
                        result[1 + i] = rows[i].ToArray();
                    }
                    return result;
                }
                catch (SqlException e)
                {
                    Console.WriteLine("FEJL: " + e.Message);
                    throw e;
                }
            }
        }
        

    }
}
