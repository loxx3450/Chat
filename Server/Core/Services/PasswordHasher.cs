using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services
{
    /// <summary>
    /// Hashes password with PBKDF2 and salt.
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Length of salt in bytes.
        /// </summary>
        private const int saltLength = 16;

        /// <summary>
        /// Length of hash in bytes.
        /// </summary>
        private const int hashLength = 20;

        /// <summary>
        /// Count of iterations for hashing
        /// </summary>
        private const int hashingIterations = 100000;


        /// <summary>
        /// Verifies if a password is equal to a hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hash.</param>
        /// <returns>Could be verified?</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            //Get hash bytes
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            //Get salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, salt, 16);

            //Create hash with given salt
            byte[] hash = MakeHash(password, salt);

            //Verification
            for (int i = 0; i < 20; ++i)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }


        /// <summary>
        /// Creates a hash from a password with 100000 iterations and SHA256 algorithm.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The hash.</returns>
        public static string Hash(string password)
        {
            byte[] salt = MakeSalt();
            byte[] hash = MakeHash(password, salt);

            byte[] hashBytes = Combine(salt, hash);

            return Convert.ToBase64String(hashBytes);
        }


        /// <summary>
        /// Creates salt.
        /// </summary>
        /// <returns>The salt.</returns>
        private static byte[] MakeSalt() 
        {
            byte[] salt = new byte[saltLength];
            new RNGCryptoServiceProvider().GetBytes(salt);

            return salt;
        }


        /// <summary>
        /// Creates hash basing on password and salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>The hash.</returns>
        private static byte[] MakeHash(string password, byte[] salt)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, hashingIterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(hashLength);
        }


        /// <summary>
        /// Combines salt and hash.
        /// </summary>
        /// <param name="salt">The salt.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>The combined hash in bytes.</returns>
        private static byte[] Combine(byte[] salt, byte[] hash)
        {
            byte[] hashBytes = new byte[saltLength + hashLength];

            Array.Copy(salt, 0, hashBytes, 0, saltLength);
            Array.Copy(hash, 0, hashBytes, saltLength, hashLength);

            return hashBytes;
        }
    }
}
