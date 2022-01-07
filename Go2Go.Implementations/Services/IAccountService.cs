using Go2Go.Model;
using Go2Go.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Implementations.Services
{
    public interface IAccountService
    {
        Task<bool> SaveTripRecord(TripViewModel tripViewModel);
        Task<UserBalance> GetLedgerBalanceForUser(int userId);
        Task<UserBalance> GetLedgerBalanceForKey(string userKey);
    }
}
