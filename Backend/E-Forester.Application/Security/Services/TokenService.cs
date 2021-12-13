using E_Forester.Application.Security.Interfaces;
using E_Forester.Model.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_Forester.Application.Security.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
     
        public string GenerateToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var durationTime = Convert.ToInt32(_configuration["JWT:DurationInMinutes"]);

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));


            var jwtSecurityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(durationTime),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                SigningCredentials = signingCredentials,
                IssuedAt = DateTime.UtcNow,
            });

            var token = tokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }

        public RefreshToken GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            var durationTime = Convert.ToInt32(_configuration["RefreshToken:DurationInHours"]);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(durationTime)
            };

            return refreshToken;
        }
    }
}
