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
    internal static class VerificationCodeDbHelper
    {
        // =============== BooleanRequests ===============
        public static bool IsCodeValid(int userId, string verificationCode)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            return dbContext.VerificationCodes
                            .Any(code => code.UserId == userId 
                                && code.Code == verificationCode 
                                && code.Used == false 
                                && code.CreatedAt >= DateTime.UtcNow.AddMinutes(-15));            //if code is not expired
        }


        // =============== INSERT Requests ===============
        public static void SaveVerificationCode(string email, string verificationCode)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            dbContext.VerificationCodes
                     .Add(new VerificationCode(UserDbHelper.GetUserId(email), verificationCode));

            dbContext.SaveChanges();
        }


        // =============== UPDATE Requests ===============
        public static void ChangeCodeStateToUsed(int userId, string verificationCode)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            dbContext.VerificationCodes
                     .Where(code => code.UserId == userId && code.Code == verificationCode)
                     .ExecuteUpdate(setter => setter
                         .SetProperty(code => code.Used, true));

            dbContext.SaveChanges();
        }
    }
}
