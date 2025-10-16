using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using nextstep.application.Abstractions.Service;
using nextstep.domain.Entities;
using nextstep.application.DTOs.Responses;
using Microsoft.Extensions.Configuration;

namespace nextstep.application.Services
   
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            var secret = config["Jwt:Key"];
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }

        // a public method called GenerateToken with return type as token
        public Token GenerateToken(User user)
        {
            // defining its variables- credentials
            // SigningCredentials defines how the token will be signed and _key is from appsettings, hmac is the algorithm to hash 
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);


            // Define what information (claims) will be stored inside the JWT
            // "id" → user’s unique ID (for identifying the user)
            // "sub" → the subject (usually the user's email)
            // "jti" → a unique ID for this specific token instance (helps prevent reuse)
            var claims = new[]
            {
        new Claim("id", user.id.ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, user.email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var tokenExpiry = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: "nextstep-app",
                audience: "nextstep-users",
                claims: claims,
                expires: tokenExpiry,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token()
            {
                token = tokenString,
                tokenExpiry = tokenExpiry
            };
        }
    }
}

