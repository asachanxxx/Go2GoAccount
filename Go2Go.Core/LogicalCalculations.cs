using Go2Go.Core.Genarators;
using Go2Go.Model;
using Go2Go.Model.Enum;
using Go2Go.Model.ViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Go2Go.Core
{
    public class LogicalCalculations : ILogicalCalculations
    {
        private readonly IOptions<CompanySettings> _options;
        private readonly ISerialGenarator _serialGenarator;

        public LogicalCalculations(IOptions<CompanySettings> options, ISerialGenarator serialGenarator)
        {
            _options = options;
            _serialGenarator = serialGenarator;
        }

        public async Task<decimal> CalCommissionPortion(TripCalViewModel tripCalViewModel)
        {
            decimal companyPayable = ((tripCalViewModel.KmPrice + tripCalViewModel.TimePrice) *  _options.Value.SCR) / 100;
            decimal commissionPortion = (companyPayable * _options.Value.ODR) / 100;
            return await Task.FromResult(commissionPortion);
        }

        public async Task<decimal> CalCompanyPayable(TripCalViewModel tripCalViewModel)
        {
            decimal companyPayable = ((tripCalViewModel.KmPrice + tripCalViewModel.TimePrice) * _options.Value.SCR) / 100;
            return await Task.FromResult(companyPayable);
        }

        public async Task<decimal> CalCompanyPortion(TripCalViewModel tripCalViewModel)
        {
            decimal companyPayable = ((tripCalViewModel.KmPrice + tripCalViewModel.TimePrice) * _options.Value.SCR) / 100;
            decimal CompanyPortion = (companyPayable * (100 - _options.Value.ODR)) / 100;
            return await Task.FromResult(CompanyPortion);
        }

        public async Task<decimal> CalDriverPortion(TripCalViewModel tripCalViewModel)
        {
            decimal driverPortion = ((tripCalViewModel.KmPrice + tripCalViewModel.TimePrice) * (100 - _options.Value.SCR)) / 100;
            return await Task.FromResult(driverPortion);
        }

        public async Task<UserLedger> GetUserLedgerObject(TripViewModel tripViewModel,decimal amt, TreansactionType treansactionType)
        {
            /*
             (Credit 1 and Debit  2)
             */
            UserLedger userLedger = new UserLedger()
            {
                Balance = 0,
                TrxDate = tripViewModel.Date,
                DriverId = tripViewModel.DriverId,
                DriverKey = tripViewModel.DriverKey,
                TripId = tripViewModel.TripId,
                TrxId = _serialGenarator.GetLedgerTrxNumber()

            };

            switch (treansactionType) {
                case TreansactionType.DriverPortion: //Credit
                    userLedger.Description = "Earning from the Trip";
                    userLedger.Amount = amt;
                    userLedger.Flag = 1;
                    userLedger.TreansactionType = treansactionType;
                    break;
                case TreansactionType.CommissionPortion: //Credit - added to the trip's originated driver
                    userLedger.Description = "Commission";
                    userLedger.Amount = amt;
                    userLedger.Flag = 1;
                    userLedger.TreansactionType = treansactionType;
                    userLedger.DriverId = tripViewModel.CommissionedDriverId;
                    userLedger.DriverKey = tripViewModel.CommissionedDriverKey;
                    break;
                case TreansactionType.CommissionPortionDebit: //Debited from original driver cut
                    userLedger.Description = "Commission";
                    userLedger.Amount = amt;
                    userLedger.Flag = 2;
                    userLedger.TreansactionType = treansactionType;
                    userLedger.DriverId = tripViewModel.DriverId;
                    userLedger.DriverKey = tripViewModel.DriverKey;
                    break;
                case TreansactionType.AppPricePortion: //debit app price debited from driver 
                    userLedger.Description = "App price";
                    userLedger.Amount = tripViewModel.AppPrice;
                    userLedger.Flag = 2;
                    userLedger.TreansactionType = treansactionType;
                    break;
                case TreansactionType.AppPricePortionCreditToCompany: //Credit - app price credited to company
                    userLedger.Description = "App price";
                    userLedger.Amount = tripViewModel.AppPrice;
                    userLedger.Flag = 1;
                    userLedger.TreansactionType = treansactionType;
                    userLedger.DriverId = 1;
                    userLedger.DriverKey = "Company";
                    break;
                case TreansactionType.CompanyPortion: //Debit - 10% froom KMPRICE + TIME Price debited from Driver
                    userLedger.Description = "Company Recivings";
                    userLedger.Amount = amt;
                    userLedger.Flag = 2;
                    userLedger.TreansactionType = treansactionType;
                    break;
                case TreansactionType.CompanyPortionCreditToCompany: //Credit 10% froom KMPRICE + TIME Price credit to Company
                    userLedger.Description = "Company reciveings Credit to Company";
                    userLedger.Amount = amt;
                    userLedger.Flag = 1;
                    userLedger.TreansactionType = treansactionType;
                    userLedger.DriverId = 1;
                    userLedger.DriverKey = "Company";
                    break;
                case TreansactionType.CompanyPayable: //Credit
                    userLedger.Description = "Total Company Payable of the ride";
                    userLedger.Amount = amt;
                    userLedger.Flag = 2;
                    userLedger.TreansactionType = treansactionType;
                    break;
            }
            
            return await Task.FromResult(userLedger);
        }
    }
}
