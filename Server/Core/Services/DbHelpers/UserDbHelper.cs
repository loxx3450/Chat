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
    internal class UserDbHelper
    {
        // =============== BooleanRequests ===============
        public static bool UserExists(string email)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            return dbContext.Users
                            .Any(user => user.Email == email && user.VerifiedEmail == true);
        }

        public static bool EmailExists(string email)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            return dbContext.Users
                            .Any(user => user.Email == email);
        }


        // =============== SELECT Requests ===============
        public static int GetUserId(string email)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            return dbContext.Users
                            .Where(user => user.Email == email)
                            .Select(user => user.Id)
                            .SingleOrDefault();
        }

        public static string? GetPassword(string email)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            return dbContext.Users
                            .Where(user => user.Email == email)
                            .Select(user => user.Password)
                            .SingleOrDefault();
        }


        // =============== INSERT Requests ===============
        public static void CreateNewUser(string email, string username, string password)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            dbContext.Users
                     .Add(new User(username, email, password));

            dbContext.SaveChanges();
        }


        // =============== UPDATE Requests
        public static void CreateNewUserOnUnverifiedEmail(string email, string username, string password)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            dbContext.Users
                     .Where(user => user.Email == email)
                     .ExecuteUpdate(setter => setter
                         .SetProperty(user => user.Username, username)
                         .SetProperty(user => user.Password, password)
                         .SetProperty(user => user.CreatedAt, DateTime.UtcNow)
                         .SetProperty(user => user.UpdatedAt, DateTime.UtcNow));

            dbContext.SaveChanges();
        }

        public static void MarkUserAsVerified(int userId)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            dbContext.Users
                     .Where(user => user.Id == userId)
                     .ExecuteUpdate(setter => setter
                         .SetProperty(user => user.VerifiedEmail, true));

            dbContext.SaveChanges();
        }

        public static void UpdatePassword(int userId, string newPassword)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            dbContext.Users
                     .Where(user => user.Id == userId)
                     .ExecuteUpdate(setter => setter
                         .SetProperty(user => user.Password, newPassword)
                         .SetProperty(user => user.UpdatedAt, DateTime.UtcNow));

            dbContext.SaveChanges();
        }
    }
}
