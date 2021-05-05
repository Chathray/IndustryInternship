using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Idis.Infrastructure
{
    public static class DataProvider
    {
        //--
        public static DataTable ExecReader(this IDbConnection conn, string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                MySqlCommand command = new(query, conn as MySqlConnection);
                MySqlDataAdapter adapter = new(command);
                DataTable data = new();
                adapter.Fill(data);
                return data;
            }
            catch (InfrastructureException)
            {
                conn.Close();
                return null;
            }
        }

        public static DataSet ExecReaders(this IDbConnection conn, string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                MySqlCommand command = new(query, conn as MySqlConnection);
                MySqlDataAdapter adapter = new(command);
                DataSet data = new();
                adapter.Fill(data);
                return data;
            }
            catch (InfrastructureException)
            {
                conn.Close();
                return null;
            }
        }


        //--
        public static bool ExecNonQuery(this IDbConnection conn, string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                MySqlCommand command = new(query, conn as MySqlConnection);
                int ok = command.ExecuteNonQuery();

                conn.Close();
                return ok != -1;
            }
            catch (InfrastructureException)
            {
                conn.Close();
                return false;
            }
        }

        //--
        public static object ExecScalar(this IDbConnection conn, string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                MySqlCommand command = new(query, conn as MySqlConnection);
                return command.ExecuteScalar();
            }
            catch (InfrastructureException)
            {
                conn.Close();
                return null;
            }
        }
    }
}
