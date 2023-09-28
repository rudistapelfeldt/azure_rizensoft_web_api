using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Webapi.Helper
{
    public class TokenHelper
    {
        public const string Issuer = "https://apitemplaterizensoft.azurewebsites.net/";

        public const string LocalIssuer = "https://localhost:7260";

        public const string Audience = "https://apitemplaterizensoft.azurewebsites.net/";

        public const string LocalAudience = "https://localhost:7260";

        public static async Task<string> GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[32];

            using var randomNumberGenerator = RandomNumberGenerator.Create();

            await System.Threading.Tasks.Task.Run(() => randomNumberGenerator.GetBytes(secureRandomBytes));

            var refreshToken = Convert.ToBase64String(secureRandomBytes);

            return refreshToken;
        }
    }
}

