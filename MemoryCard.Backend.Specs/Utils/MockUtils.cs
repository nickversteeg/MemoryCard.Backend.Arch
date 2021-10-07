using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Specs
{
    public static class MockUtils
    {
        public static readonly string GuidRegex = @"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$";
        public static readonly string JwtRegex = @"^([a-zA-Z0-9_=]+)\.([a-zA-Z0-9_=]+)\.([a-zA-Z0-9_\-\+\/=]*)";

        public static string Issuer { get; } = Guid.NewGuid().ToString();
        public static SecurityKey SecurityKey { get; }
        public static SigningCredentials SigningCredentials { get; }

        private static readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        private static readonly byte[] s_key = new byte[32];

        static MockUtils()
        {
            // JWT Setup
            _rng.GetBytes(s_key);
            SecurityKey = new SymmetricSecurityKey(s_key) { KeyId = Guid.NewGuid().ToString() };
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public static string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            return _tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
        }
    }
}
