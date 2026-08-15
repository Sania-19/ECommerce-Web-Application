using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopVerseECommercePlatform.Application.Abstraction.IJWTProvider;
using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopVerseECommercePlatform.Infrastructure.JWTProvider
{
    public class JWTProvider(IConfiguration configuration) : IJWTProvider
    {
        public string GenerateToken(User user)
        {
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity
                (
                    new List<Claim>
                    {
                          new Claim(UserClaims.Id,user.Id.ToString()),
                          new Claim(UserClaims.Email,user.Email),
                          new Claim(UserClaims.PhoneNo,user.PhoneNo),
                          new Claim(UserClaims.UserRole,user.UserRole.ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddMinutes(30),
                IssuedAt = DateTime.UtcNow,
                Audience = configuration["JWT:Audience"],
                Issuer = configuration["JWT:Issuer"],
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                   , SecurityAlgorithms.HmacSha256
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
