using CommonLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services.DbHelpers
{
    internal class UserDbHelper
    {
        // =============== BooleanRequests ===============
        public static bool UserExists(string email)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            string cmdText = "SELECT 1 " +
                             "FROM users " +
                             $"WHERE email = @email " +
                                $"AND verified_email = @verified_email";

            cmd.CommandText = DbHelper.FormulateBooleanRequest(cmdText);

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@verified_email", true);

            return Convert.ToBoolean(DbHelper.ExecuteScalar(cmd));
        }

        public static bool EmailExists(string email)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            string cmdText = "SELECT 1 " +
                             "FROM users " +
                             $"WHERE email = @email";

            cmd.CommandText = DbHelper.FormulateBooleanRequest(cmdText);

            cmd.Parameters.AddWithValue("@email", email);

            return Convert.ToBoolean(DbHelper.ExecuteScalar(cmd));
        }


        // =============== SELECT Requests ===============
        public static int GetUserId(string email)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "SELECT id " +
                              "FROM users " +
                              $"WHERE email = @email;";

            cmd.Parameters.AddWithValue("@email", email);

            return Convert.ToInt32(DbHelper.ExecuteScalar(cmd));
        }

        public static string? GetPassword(string email)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "SELECT password " +
                              "FROM users " +
                              $"WHERE email = @email";

            cmd.Parameters.AddWithValue("@email", email);

            return Convert.ToString(DbHelper.ExecuteScalar(cmd));
        }


        // =============== INSERT Requests ===============
        public static void CreateNewUser(string email, string username, string password)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "INSERT INTO users " +
                              "(username, email, password, verified_email, created_at, updated_at)" +
                              $"VALUES(@username, @email, @password, @verified_email, @now, @now);";

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@verified_email", false);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);

            DbHelper.ExecuteNonQuery(cmd);
        }


        // =============== UPDATE Requests
        public static void CreateNewUserOnUnverifiedEmail(string email, string username, string password)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "UPDATE users " +
                              "SET username = @username, password = @password, created_at = @now, updated_at = @now " +
                              $"WHERE email = @email";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);

            DbHelper.ExecuteNonQuery(cmd);
        }

        public static void MarkUserAsVerified(int userId)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "UPDATE users " +
                              "SET verified_email = @verified_email " +
                              $"WHERE id = @id";

            cmd.Parameters.AddWithValue("@id", userId);
            cmd.Parameters.AddWithValue("@verified_email", true);

            DbHelper.ExecuteNonQuery(cmd);
        }

        public static void UpdatePassword(int userId, string newPassword)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "UPDATE users " +
                              $"SET password = @password, " +
                                  $"updated_at = @now " +
                              $"WHERE id = @id;";

            cmd.Parameters.AddWithValue("@id", userId);
            cmd.Parameters.AddWithValue("@password", PasswordHasher.Hash(newPassword));
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);

            DbHelper.ExecuteNonQuery(cmd);
        }
    }
}
