using Go2Go.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model.ViewModels
{
    public class TripViewModel
    {
        public string FKey { get; set; }
        public string DriverKey { get; set; }
        public int TripId { get; set; }// TripId(Need to genarate this)
        public string TripNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Distance { get; set; }
        public decimal Duration { get; set; }
        public int DriverId { get; set; }
        public int CommissionedDriverId { get; set; }
        public string CommissionedDriverKey { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public decimal AppPrice { get; set; }
        public decimal Commission { get; set; }
        public decimal TimePrice { get; set; }
        public decimal TotalFare { get; set; }
        public decimal KmPrice { get; set; }
        public decimal CompanyPayable { get; set; }
        public bool CommissionApplicable { get; set; }
    }
}
