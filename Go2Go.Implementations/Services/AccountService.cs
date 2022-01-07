using Go2Go.Core;
using Go2Go.Core.Exceptions;
using Go2Go.Core.Factories;
using Go2Go.Core.Genarators;
using Go2Go.Data.Context;
using Go2Go.Model;
using Go2Go.Model.Enum;
using Go2Go.Model.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Go2Go.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogicalCalculations _logicalCalculations;
        private readonly Go2GoContext _go2GoContext;
        private readonly ISerialGenarator _serialGenarator;
        int x = 10;
        public AccountService(ILogicalCalculations logicalCalculations, Go2GoContext go2GoContext, ISerialGenarator serialGenarator )
        {
            _logicalCalculations = logicalCalculations;
            _go2GoContext = go2GoContext;
            _serialGenarator = serialGenarator;
        }

        public async Task<UserBalance> GetLedgerBalanceForUser(int userId)
        {
            var userBalance = await _go2GoContext.UserBalances.FindAsync(userId);
            return userBalance;
        }

        public async Task<UserBalance> GetLedgerBalanceForKey(string userKey)
        {
            var userBalance = await _go2GoContext.UserBalances.FirstOrDefaultAsync(a => a.UserKey == userKey);
            return userBalance;
        }

        public async Task<bool> SaveTripRecord(TripViewModel tripViewModel)
        {
            if (tripViewModel == null) {
                throw new ValidationException("Trip details not complete");
            }
            using var transaction = _go2GoContext.Database.BeginTransaction();
            try
            {
                Trip trip = new Trip
                {
                    Date = tripViewModel.Date,
                    Distance = tripViewModel.Distance,
                    DriverID = tripViewModel.DriverId,
                    Duration = tripViewModel.Duration,
                    Fire = tripViewModel.TotalFare,
                    FKey = tripViewModel.FKey,
                    TripId = tripViewModel.TripId,
                    TripNo = _serialGenarator.GetTripNumber()

                };

                var tripRecord = await _go2GoContext.Trips.AddAsync(trip);
                var tripx = (Trip)tripRecord.Entity;
                await _go2GoContext.SaveChangesAsync();

                TripCalViewModel tripCalViewModel = new TripCalViewModel()
                {
                    TotalFare = tripViewModel.TotalFare,
                    AppPrice = tripViewModel.AppPrice,
                    KmPrice = tripViewModel.KmPrice,
                    TimePrice = tripViewModel.TimePrice
                };
                var driverPortion = await _logicalCalculations.CalDriverPortion(tripCalViewModel);
                var companyPayable = await _logicalCalculations.CalCompanyPayable(tripCalViewModel);
                var commissionPortion = await _logicalCalculations.CalCommissionPortion(tripCalViewModel);
                var companyPortion = await _logicalCalculations.CalCompanyPortion(tripCalViewModel);
              
                List<UserLedger> userLedgers = new List<UserLedger>();
                //userLedgers.Add(await _logicalCalculations.GetUserLedgerObject(tripViewModel, driverPortion, TreansactionType.DriverPortion));
                userLedgers.Add(await _logicalCalculations.GetUserLedgerObject(tripViewModel, companyPortion, TreansactionType.CompanyPortion));
                userLedgers.Add(await _logicalCalculations.GetUserLedgerObject(tripViewModel, companyPortion, TreansactionType.CompanyPortionCreditToCompany));
                userLedgers.Add(await _logicalCalculations.GetUserLedgerObject(tripViewModel, commissionPortion, TreansactionType.CommissionPortion));
                userLedgers.Add(await _logicalCalculations.GetUserLedgerObject(tripViewModel, commissionPortion, TreansactionType.CommissionPortionDebit));
                userLedgers.Add(await _logicalCalculations.GetUserLedgerObject(tripViewModel, 0, TreansactionType.AppPricePortion));
                userLedgers.Add(await _logicalCalculations.GetUserLedgerObject(tripViewModel, 0, TreansactionType.AppPricePortionCreditToCompany));

                foreach (var item in userLedgers)
                {
                    item.TripId = tripx.Id;
                    await ExcuteUserLedgers(item);
                }
                transaction.Commit();

            }
            catch (Exception ex)
            {

                return false;
            }
            return true;
        }
        private async Task ExcuteUserLedgers(UserLedger userLedger)
        {
            try
            {
                DataTable addedCategories = DataTableFactory.GetUserLedgersTable();
                DataRow drow = addedCategories.NewRow();
                drow["Id"] = 0;
                drow["TrxId"] = userLedger.TrxId;
                drow["DriverId"] = userLedger.DriverId;
                drow["DriverKey"] = userLedger.DriverKey;
                drow["RefId"] = userLedger.TripId;
                drow["TreansactionType"] = userLedger.TreansactionType;
                drow["Description"] = userLedger.Description;
                drow["Amount"] = userLedger.Amount;
                drow["Flag"] = userLedger.Flag;
                drow["Balance"] = 0;
                //drow["TrxDate"] = userLedger.TrxDate.ToUniversalTime();
                addedCategories.Rows.Add(drow);

                SqlParameter tvpParam = new SqlParameter { ParameterName = "@UserLedger", Value = addedCategories };
                tvpParam.TypeName = "dbo.tblUserBalance";
                string sql = "EXEC SP_AddUserLedgerEntry @UserLedger";
                var rowsAffected = await _go2GoContext.Database.ExecuteSqlRawAsync(sql, tvpParam);
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
}