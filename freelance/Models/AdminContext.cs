using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace freelance.Models
{
    public class AdminContext :DbContext
    {
        public DbSet<Admin> Admins { get; set; }

    }
}