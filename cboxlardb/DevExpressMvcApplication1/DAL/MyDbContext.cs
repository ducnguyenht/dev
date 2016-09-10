using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DevExpressMvcApplication1.Models;

namespace DevExpressMvcApplication1.DAL
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