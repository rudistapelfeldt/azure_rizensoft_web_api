﻿namespace Webapi.Models
{
    public class RefreshTokenRequest
	{
        public int UserId { get; set; }

        public string RefreshToken { get; set; }
    }
}

