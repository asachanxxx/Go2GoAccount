using Go2Go.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Implementations.Services
{
    public interface IAuthService
    {
        Task<int> AddGUser(UserViewModel userViewModel);
        Task<int> UpdateGUser(UserViewModel userViewModel);
        Task<int> UpdatePassword(PasswordViewModel passwordViewModel);
        Task<int> SignIn(SignInViewModel signInViewModel);
        Task<TokenViewModel> SignInWeb(SignInViewModel signInViewModel);

        
    }
}
