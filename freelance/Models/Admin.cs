using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace freelance.Models
{
    [Table("Table")]
    public class Admin
    {
        public string Id { set; get; }
        public string Email { set; get; }
        public bool EmailConfirmed { set; get; }
        public string PasswordHash { set; get; }
        public string SecurityStamp { set; get; }
        public string Phone { set; get; }
        public bool PhoneNumberConfirmed { set; get; }
        public bool TwoFactorEnabled { set; get; }
        public DateTime LockoutEndDateUtc { set; get; }
        public bool LockoutEnabled { set; get; }
        public int AccessFailedCount { set; get; }
        public string Name { set; get; }
        public string UserType { set; get; }

    }
}