using Go2Go.Core;
using Go2Go.Data.Context;
using Go2Go.Model.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace Go2Go.Implementations.Services
{
    public class AuthService : IAuthService
    {

        private readonly ILogicalCalculations _logicalCalculations;
        private readonly Go2GoContext _go2GoContext;
        int x = 10;
        public AuthService(ILogicalCalculations logicalCalculations, Go2GoContext go2GoContext)
        {
            _logicalCalculations = logicalCalculations;
            _go2GoContext = go2GoContext;
        }

        public async Task<int> AddGUser(UserViewModel userViewModel)
        {
            string sql = "Sp_GAddUser";

            using (var connection = _go2GoContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                var affectedRows = await connection.QueryFirstOrDefaultAsync<int>(sql,
                    new
                    {
                        pLogin = userViewModel.LoginName,
                        pPassword = userViewModel.PasswordHash,
                        pFirstName = userViewModel.FirstName,
                        pLastName = userViewModel.LastName,
                        pRoleId = userViewModel.RoleId,
                        pPhoneNumber = userViewModel.PhoneNumber,
                        pEmail = userViewModel.Email,
                        pFKey = userViewModel.FKey,
                        pUserType = userViewModel.UserType,
                    },
                    commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public async Task<int> SignIn(SignInViewModel signInViewModel)
        {
            try
            {
                string sql = "Sp_GLogin";

                using (var connection = _go2GoContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    var affectedRows = await connection.QueryFirstOrDefaultAsync<int>(sql,
                        new
                        {
                            @pLoginName = signInViewModel.UserName,
                            @pPassword = signInViewModel.Password,
                        },
                        commandType: CommandType.StoredProcedure);
                    return affectedRows;
                }
            }
            catch (Exception ex) {
                return 500;
            }
        }

        public async Task<TokenViewModel> SignInWeb(SignInViewModel signInViewModel)
        {
            try
            {
                string sql = "Sp_GLoginWeb";

                using (var connection = _go2GoContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    var affectedRows = await connection.QueryFirstOrDefaultAsync<TokenViewModel>(sql,
                        new
                        {
                            @pLoginName = signInViewModel.UserName,
                            @pPassword = signInViewModel.Password,
                        },
                        commandType: CommandType.StoredProcedure);
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving the GUser Data",ex);
            }
        }

        public async Task<int> UpdateGUser(UserViewModel userViewModel)
        {
            return await Task.FromResult<int>(200);
        }

        public Task<int> UpdatePassword(PasswordViewModel passwordViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
