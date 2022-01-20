using Go2Go.Model.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Model.Security
{
    public class GUser
    {
        [Key]
        public int UserID { get; set; }
        [MaxLength(40)]
        public string LoginName { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public GRole Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public Guid Salt { get; set; }
        public string Email { get; set; }
        public string FKey { get; set; }
        public string PhoneNumber { get; set; }
        public UserTypes UserType { get; set; }
    }
}
