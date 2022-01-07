using Go2Go.Model.Base;
using Go2Go.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model
{
    public class TripPayment : IEntity
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int DriverId { get; set; }
        public int CommissionedDriverId { get; set; }
        public string FKey { get; set; }
        public decimal Distance { get; set; }
        public decimal Duration { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public decimal AppPrice { get; set; }
        public decimal Commission { get; set; }
        public decimal TimePrice { get; set; }
        public decimal TotalFare { get; set; }
        public decimal CompanyPayable { get; set; }
        public bool CommissionApplicable { get; set; }
    }
}
