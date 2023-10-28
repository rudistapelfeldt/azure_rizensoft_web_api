using Microsoft.AspNetCore.Http;
using Webapi.Models;

namespace Webapi.Interfaces
{
    public interface ITokenService
	{
		string CreateToken(User user);
	}
}

