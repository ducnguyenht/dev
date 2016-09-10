using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCLargeDB01.Models;

namespace MVCLargeDB01.DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=DefaultConnection")
        {
        }
        public DbSet<Premise> Premises { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}