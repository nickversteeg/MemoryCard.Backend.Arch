﻿using MemoryCard.Backend.Models;
using MemoryCard.Backend.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Services.Interfaces
{
    public class JwtAuthService : IAuthService
    {
        IRepository<UserCredentialsModel> userCredentialsRepository;

        public JwtAuthService(IRepository<UserCredentialsModel> userCredentialsRepository )
        {

        }

        public string Authenticate(string username, string password)
        {
            var user = userCredentialsRepository.FindFirst(user =>
            {
                return user.Username == username &&
                user.Password == password;
            });

            if(user == null)
            {
                return "";
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes("This Is A Very Long Key Test To Use");

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}