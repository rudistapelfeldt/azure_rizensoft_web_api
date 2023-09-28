using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Webapi.Helper
{
    public static class PasswordEncoder
	{
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            
            RandomNumberGenerator.Create().GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public static byte[] GetSecureSalt()
        {
            return RandomNumberGenerator.GetBytes(32);
        }

        public static string HashUsingPbkdf2(string password, byte[] salt)
        {
            byte[] derivedKey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, iterationCount: 300000, 32);
            return Convert.ToBase64String(derivedKey);
        }
    }
}

