using Go2Go.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model
{
    public class Trip: IEntity
    {
        public int Id { get; set; }
        public string FKey { get; set; }
        public int TripId { get; set; }
        public string TripNo { get; set; }// TripId(Need to genarate this)
        public int DriverID { get; set; }
        public DateTime Date { get; set; }
        public decimal Fire { get; set; }
        public decimal Distance { get; set; }
        public decimal Duration { get; set; }

    }
}
