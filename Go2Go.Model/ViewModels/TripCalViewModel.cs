using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model.ViewModels
{
    public class TripCalViewModel
    {
        public decimal TotalFare { get; set; }
        public decimal KmPrice { get; set; }
        public decimal AppPrice { get; set; }
        public decimal TimePrice { get; set; }
    }
}
