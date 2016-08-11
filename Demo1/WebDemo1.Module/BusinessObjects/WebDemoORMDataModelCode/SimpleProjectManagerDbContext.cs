using System.Data.Entity;

namespace WebDemo1.Module.BusinessObjects.WebDemoORMDataModelCode
{
    public class SimpleProjectManagerDbContext : DbContext
    {
        //...
        public DbSet<Customer> Customer { get; set; }

        public DbSet<Testimonial> Testimonial { get; set; }
    }
}