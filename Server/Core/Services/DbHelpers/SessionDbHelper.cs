using CommonLibrary.Models;
using CommonLibrary.Models.EF;
using Microsoft.EntityFrameworkCore;
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
        //Status_id's
        private const bool LOGGED_IN = true;
        private const bool LOGGED_OUT = false;


        // =============== BooleanRequests ===============
        public static bool IsActualSessionFounded(string ip)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            return dbContext.Sessions
                            .Any(sess => sess.Ip == ip
                                && sess.IsLoggedIn == LOGGED_IN
                                && sess.UpdatedAt >= DateTime.UtcNow.AddDays(-3));                  //after 3 days session will be expired
        }

        public static bool IsSessionFounded(int user_id, string ip)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            return dbContext.Sessions
                            .Any(sess => sess.UserId == user_id && sess.Ip == ip);
        }


        // =============== SELECT Requests ===============
        public static int GetUserId(string ip)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            return dbContext.Sessions
                            .Where(sess => sess.Ip == ip
                                && sess.IsLoggedIn == LOGGED_IN)
                            .Select(sess => sess.UserId)
                            .SingleOrDefault();
        }


        // =============== INSERT Requests ===============
        public static void CreateNewSession(int user_id, string ip)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            dbContext.Sessions
                     .Add(new Session(user_id, ip));

            dbContext.SaveChanges();
        }


        // =============== UPDATE Requests ===============
        public static void UpdateExistedSession(int user_id, string ip)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            dbContext.Sessions
                     .Where(sess => sess.UserId == user_id && sess.Ip == ip)
                     .ExecuteUpdate(setter => setter
                         .SetProperty(sess => sess.IsLoggedIn, LOGGED_IN)
                         .SetProperty(sess => sess.UpdatedAt, DateTime.UtcNow));

            dbContext.SaveChanges();
        }
    }
}
