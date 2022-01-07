using Go2Go.Model.Base;
using Go2Go.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model
{
    public class UserLedger : IEntity
    {
        public int Id { get; set; }
        public string TrxId { get; set; }
        public int DriverId { get; set; }
        public string DriverKey { get; set; }
        /// <summary>
        /// Save this in db As RefId
        /// </summary>
        public int TripId { get; set; } 
        public TreansactionType TreansactionType { get; set; }
        public string Description { get; set; }
        public int Flag { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime TrxDate { get; set; }
        public DateTime RecordDate { get; set; }

    }
}
