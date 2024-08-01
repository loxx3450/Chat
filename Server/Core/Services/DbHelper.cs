using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services
{
    public static class DbHelper
    {
        private static string connString = ConfigurationManager.AppSettings["connString"];

        private static NpgsqlConnection conn = null!;

        public static void OpenConnection()
        {
            conn = new NpgsqlConnection(connString);
            conn.Open();
        }

        public static void CloseConnection() 
        { 
            conn.Close();
        }

        public static object? ExecuteScalar(NpgsqlCommand cmd)
        {
            cmd.Connection = conn;

            return cmd.ExecuteScalar();
        }

        public static void ExecuteNonQuery(NpgsqlCommand cmd)
        {
            cmd.Connection = conn;

            cmd.ExecuteNonQuery();
        }

        public static NpgsqlDataReader ExecuteReader(NpgsqlCommand cmd)
        {
            cmd.Connection = conn;

            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        public static string FormulateBooleanRequest(string commandText)
        {
            return "SELECT " +
                   "CASE " +
                       "WHEN EXISTS " +
                       "(" +
                           $"{commandText}" +
                       ") " +
                       "THEN 1 " +
                       "ELSE 0 " +
                   "END;";
        }
    }
}
