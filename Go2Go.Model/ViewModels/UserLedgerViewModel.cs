using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model.ViewModels
{
    public class UserLedgerViewModel
    {
        public string DriverKey { get; set; }
        public int TripId { get; set; }
        public string Description { get; set; }
        public string TrxId { get; set; }
        public int Flag { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime TrxDate { get; set; }
    }
}
