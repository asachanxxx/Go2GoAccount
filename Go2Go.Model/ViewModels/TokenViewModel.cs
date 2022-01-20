using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model.ViewModels
{
    public class TokenViewModel
    {
        public int UserID { get; set; }
        public string FKey { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserType { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string Token { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }

    }
}
