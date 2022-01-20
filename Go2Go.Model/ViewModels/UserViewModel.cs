using Go2Go.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model.ViewModels
{
    public class UserViewModel
    {
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FKey { get; set; }
        public string PhoneNumber { get; set; }
        public UserTypes UserType { get; set; }
    }
}
