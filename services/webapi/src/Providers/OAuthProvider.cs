using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using Webapi.Domains;

namespace webapi.Providers
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        readonly IServiceProvider _serviceProvider;

        public OAuthProvider(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); // For simplicity, we accept all clients (not recommended for production!)
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Validate the user's credentials (e.g., check against a database)
            if (await IsValidUser(context.UserName, context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

                // You can add more claims based on user roles or additional information
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));

                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                context.Rejected();
            }
        }

        // Implement a method to validate user credentials (e.g., query a database)
        private async Task<bool> IsValidUser(string userName, string password)
        {
            var userDomain = _serviceProvider.GetService(typeof(UserDomain)) as UserDomain;
            var result = await userDomain.Search(userName, password);
            return result.Success;
        }
    }
}

