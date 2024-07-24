using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services
{
    public static class CommandExecutor
    {
        private static string connString = ConfigurationManager.AppSettings["connString"];

        public static object? ExecuteScalar(string commandText)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand(commandText, conn);

            return cmd.ExecuteScalar();
        }

        public static void ExecuteNonQuery(string commandText) 
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand(commandText, conn);

            cmd.ExecuteNonQuery();
        }

        public static NpgsqlDataReader ExecuteReader(string commandText) 
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand(commandText, conn);

            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
    }
}
