using Hyperlogy.Common.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.Common.JWT
{
    public static class TokenMananger
    {
        public static string GenerateToken(string UserName, string PublicKey, int expireMinutes = 1440)
        {
            // convert config secret key to bytes
            var symmetricKey = Encoding.ASCII.GetBytes(WebJwtConstants.SECRET_KEY);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            // create descriptor for token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // payload
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("username", UserName),
                    new Claim("publickey", PublicKey)
                }),
                // thoi gian hieu luc cua token ( DateTime )
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                // ma khoa secret key
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // create token
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
