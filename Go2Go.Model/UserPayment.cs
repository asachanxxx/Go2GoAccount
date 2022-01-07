using Go2Go.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model
{
    public class UserPayment : IEntity
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmt { get; set; }
    }
}
