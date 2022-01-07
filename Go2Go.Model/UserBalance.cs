using Go2Go.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model
{
    public class UserBalance
    {
        public int Id { get; set; }
        public string DriverId { get; set; }
        public string UserKey { get; set; }
        public string FName { get; set; }
        public UserTypes UserType { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Balance { get; set; }

    }
}
