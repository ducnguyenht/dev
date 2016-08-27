namespace dnmvcschedule.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CarsDBContext : DbContext
    {
        public CarsDBContext()
            : base("name=CarsDBContextConnectionString")
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarScheduling> CarSchedulings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CarScheduling>()
                .Property(e => e.Price)
                .HasPrecision(10, 4);
        }
    }
}
