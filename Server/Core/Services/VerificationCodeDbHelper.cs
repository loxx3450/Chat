using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services
{
    internal static class VerificationCodeDbHelper
    {
        public static void SaveVerificationCode(string email, string code)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "INSERT INTO verification_codes(user_id, code, created_at)" +
                              $"VALUES(@id, @code, @now);";

            cmd.Parameters.AddWithValue("@id", UserDbHelper.GetUserId(email));
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);

            DbHelper.ExecuteNonQuery(cmd);
        }

        public static bool IsCodeValid(int userId, string code)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            string cmdText = "SELECT 1 " +
                             "FROM verification_codes " +
                             $"WHERE user_id = @id " +
                                 $"AND code = @code " +
                                 $"AND used = false " +
                                 $"AND EXTRACT(epoch FROM age(@now, created_at)) / 60 <= 15";           //if code is not expired

            cmd.CommandText = DbHelper.FormulateBooleanRequest(cmdText);

            cmd.Parameters.AddWithValue("@id", userId);
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);

            return Convert.ToBoolean(DbHelper.ExecuteScalar(cmd));
        }

        public static void ChangeCodeStateToUsed(int userId, string code) 
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "UPDATE verification_codes " +
                              "SET used = true " +
                              $"WHERE user_id = @id " +
                                  $"AND code = @code";

            cmd.Parameters.AddWithValue("@id", userId);
            cmd.Parameters.AddWithValue("@code", code);

            DbHelper.ExecuteNonQuery(cmd);
        }
    }
}
