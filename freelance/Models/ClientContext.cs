using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace freelance.Models
{
    public class ClientContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

    }
}
