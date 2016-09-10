using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCLargeDB01.Models;

namespace MVCLargeDB01.DAL
{
    public class DBInitializer : DropCreateDatabaseAlways<MyDbContext>
    //public class DBInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {

            var countries = new List<Country>();

            for (int i = 0; i<500; i++)
            {
                countries.Add(new Country { Code = "CODE" + i.ToString(), Description = "Country" + i.ToString()});
            }
            countries.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();

            var premises = new List<Premise>();

            for (int i = 0; i<500; i++)
            {
                //premises.Add(new Premise { Code = "P"+i.ToString(), Address = i.ToString() +  " Manto Street", City = "Manchester", CountryID = i+1, Post_Code = "M8 2L", Whse_No = "555", Whse_Type = "TYPE1", System_Code="00XXA"});
                premises.Add(new Premise { Code = "P"+i.ToString(), Address = i.ToString() +  " Manto Street", City = "Manchester", CountryID = 300, Post_Code = "M8 2L", Whse_No = "555", Whse_Type = "TYPE1", System_Code="00XXA"});

            }

            premises.ForEach(s => context.Premises.Add(s));
            context.SaveChanges();
        }
    }
}