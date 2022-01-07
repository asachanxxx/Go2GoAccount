using Go2Go.Model;
using Go2Go.Model.Enum;
using Go2Go.Model.ViewModels;
using System.Threading.Tasks;

namespace Go2Go.Core
{
    public interface ILogicalCalculations
    {
        Task<decimal> CalDriverPortion(TripCalViewModel tripCalViewModel);
        Task<decimal> CalCompanyPayable(TripCalViewModel tripCalViewModel);
        Task<decimal> CalCommissionPortion(TripCalViewModel tripCalViewModel);
        Task<decimal> CalCompanyPortion(TripCalViewModel tripCalViewModel);
        Task<UserLedger> GetUserLedgerObject(TripViewModel tripViewModel, decimal amt, TreansactionType treansactionType);

    }
}