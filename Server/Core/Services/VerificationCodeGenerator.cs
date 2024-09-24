using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services
{
    internal static class VerificationCodeGenerator
    {
        private const int DEFAULT_CODE_LENGTH = 6;

        public static string GetCode()
        {
            return RandomNumberGenerator.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", DEFAULT_CODE_LENGTH);
        }

        public static string GetCode(int length)
        {
            return RandomNumberGenerator.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length);
        }
    }
}
