using Go2Go.Core;
using Go2Go.Model.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Web.Authentication
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Authenticate(string userName, string password)
        {
            if (!UserData.users.Any(x => x.Key.Equals(userName) && x.Value.Equals(password))) {
                return null;
            }
            var key = _configuration.GetValue<string>("jwtconfig:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescripter = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity( new Claim[] { 
                    new Claim(ClaimTypes.NameIdentifier,userName),
                     new Claim(Go2GoClaimTypes.UserName,userName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);

        }
    }
}
