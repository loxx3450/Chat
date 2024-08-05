using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services.DbHelpers
{
    internal static class SessionDbHelper
    {
        private const int LOGGED_IN = 1;

        public static bool IsActualSessionFounded(string ip)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            string cmdText = "SELECT 1 " +
                             "FROM sessions " +
                             $"WHERE ip = @ip " +
                                 $"AND status_id = @loggedIn " +
                                 $"AND EXTRACT(day FROM age(@now, updated_at)) <= 3";               //after 3 days session will be expired

            cmd.CommandText = DbHelper.FormulateBooleanRequest(cmdText);

            cmd.Parameters.AddWithValue("@ip", ip);
            cmd.Parameters.AddWithValue("@loggedIn", LOGGED_IN);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);

            return Convert.ToBoolean(DbHelper.ExecuteScalar(cmd));
        }

        public static bool IsSessionFounded(int user_id, string ip)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            string cmdText = "SELECT 1 " +
                             "FROM sessions " +
                             $"WHERE user_id = @id " +
                                 $"AND ip = @ip";

            cmd.CommandText = DbHelper.FormulateBooleanRequest(cmdText);

            cmd.Parameters.AddWithValue("@id", user_id);
            cmd.Parameters.AddWithValue("@ip", ip);

            return Convert.ToBoolean(DbHelper.ExecuteScalar(cmd));
        }

        public static void CreateNewSession(int user_id, string ip)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "INSERT INTO sessions " +
                  "(user_id, ip, status_id, updated_at) " +
                  $"VALUES(@id, @ip, @loggedIn, @now);";

            cmd.Parameters.AddWithValue("@id", user_id);
            cmd.Parameters.AddWithValue("@ip", ip);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@loggedIn", LOGGED_IN);

            DbHelper.ExecuteNonQuery(cmd);
        }

        public static void UpdateExistedSession(int user_id, string ip)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "UPDATE sessions " +
                                  $"SET updated_at = @now, status_id = @loggedIn " +
                                  $"WHERE user_id = @id " +
                                      $"AND ip = @ip;";

            cmd.Parameters.AddWithValue("@id", user_id);
            cmd.Parameters.AddWithValue("@ip", ip);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@loggedIn", LOGGED_IN);

            DbHelper.ExecuteNonQuery(cmd);
        }
    }
}
