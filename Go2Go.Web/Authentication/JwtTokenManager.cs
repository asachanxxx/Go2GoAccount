using Go2Go.Core;
using Go2Go.Implementations.Services;
using Go2Go.Model.Security;
using Go2Go.Model.ViewModels;
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
        private readonly IAuthService _authService;

        public JwtTokenManager(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }
        public async Task<string> Authenticate(string userName, string password)
        {
           var statusCOde = await  _authService.SignIn(new Model.ViewModels.SignInViewModel() { UserName = userName, Password = password });

            if (statusCOde != 200) {
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
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);

        }

        public async Task<TokenViewModel> AuthenticateWeb(string userName, string password)
        {
            var tokenViewModel = await _authService.SignInWeb(new Model.ViewModels.SignInViewModel() { UserName = userName, Password = password });

            if (tokenViewModel == null || tokenViewModel.UserID <= 0)
            {
                tokenViewModel.ResponseCode = 201;
                tokenViewModel.ResponseMessage = "UserName Or Password Incorrect";
                return tokenViewModel;
            }
            var key = _configuration.GetValue<string>("jwtconfig:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescripter = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,userName),
                     new Claim(Go2GoClaimTypes.UserName,userName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripter);
            var tokenx= tokenHandler.WriteToken(token);
            tokenViewModel.Token = tokenx;
            tokenViewModel.ResponseCode = 200;
            return tokenViewModel;
        }
    }
}
