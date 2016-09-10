namespace MVCLargeDB01.Migrations
{
    using MVCLargeDB01.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCLargeDB01.DAL.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MVCLargeDB01.DAL.MyDbContext";
        }

        protected override void Seed(MVCLargeDB01.DAL.MyDbContext context)
        {
            var countries = new List<Country>();

            for (int i = 0; i < 500; i++)
            {
                countries.Add(new Country { Code = "CODE" + i.ToString(), Description = "Country" + i.ToString() });
            }
            countries.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();

            var premises = new List<Premise>();

            for (int i = 0; i < 500; i++)
            {
                //premises.Add(new Premise { Code = "P"+i.ToString(), Address = i.ToString() +  " Manto Street", City = "Manchester", CountryID = i+1, Post_Code = "M8 2L", Whse_No = "555", Whse_Type = "TYPE1", System_Code="00XXA"});
                premises.Add(new Premise { Code = "P" + i.ToString(), Address = i.ToString() + " Manto Street", City = "Manchester", CountryID = 300, Post_Code = "M8 2L", Whse_No = "555", Whse_Type = "TYPE1", System_Code = "00XXA" });

            }

            premises.ForEach(s => context.Premises.Add(s));
            context.SaveChanges();
        }
    }
}
