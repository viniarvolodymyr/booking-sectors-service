﻿using System;

namespace SoftServe.BookingSectors.WebAPI.BLL.Helpers.Jwt
{
    public class JwtIssuerOptions
    {
        public string Role { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public int AccessExpirationMins { get; set; }
        public int RefreshExpirationMins { get; set; }
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor => TimeSpan.FromMinutes(AccessExpirationMins);
        public string JtiGenerator => Guid.NewGuid().ToString();
        public Microsoft.IdentityModel.Tokens.SigningCredentials SigningCredentials { get; set; }
    }
}