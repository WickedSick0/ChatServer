using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}