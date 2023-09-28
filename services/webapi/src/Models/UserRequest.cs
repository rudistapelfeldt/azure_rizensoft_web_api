using Swashbuckle.AspNetCore.Annotations;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.Models
{
    public class UserRequest
	{
		public User User { get; set; }
	}
}

