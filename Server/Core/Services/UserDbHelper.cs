using CommonLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services
{
    internal class UserDbHelper
    {
        public static bool UserExists(string email)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            string cmdText = "SELECT 1 " +
                             "FROM users " +
                             $"WHERE email = @email";

            cmd.CommandText = DbHelper.FormulateBooleanRequest(cmdText);

            cmd.Parameters.AddWithValue("@email", email);
            
            return Convert.ToBoolean(DbHelper.ExecuteScalar(cmd));
        }

        public static int GetUserId(string email)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "SELECT id " +
                              "FROM users " +
                              $"WHERE email = @email;";

            cmd.Parameters.AddWithValue("@email", email);

            return Convert.ToInt32(DbHelper.ExecuteScalar(cmd));
        }
    }
}
