using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class MyContext : DbContext
    {
        public DbSet<USER> Users { get; set; }

        public DbSet<MESSAGE> Messages { get; set; }

        public DbSet<CHATROOM> Chatrooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
