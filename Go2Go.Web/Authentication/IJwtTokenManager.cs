using Go2Go.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Go2Go.Web.Authentication
{
    public interface IJwtTokenManager
    {
        Task<string> Authenticate(string userName, string password);
        Task<TokenViewModel> AuthenticateWeb(string userName, string password);
        
    }
}
