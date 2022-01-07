using Go2Go.Model.Base;
using Go2Go.Model.Enum;
using System;

namespace Go2Go.Model
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FKey { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public UserTypes UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string GenaratedId { get; set; }
    }
}
