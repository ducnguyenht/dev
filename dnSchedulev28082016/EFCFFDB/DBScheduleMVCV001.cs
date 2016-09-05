namespace dnSchedulev01.EFCFFDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBScheduleMVCV001 : DbContext
    {
        public DBScheduleMVCV001()
            : base("name=DBScheduleMVCV001")
        {
        }

        public virtual DbSet<Opportunity> Opportunitys { get; set; }
        public virtual DbSet<ScheduleCalendar> ScheduleCalendars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Opportunity>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ScheduleCalendar>()
                .Property(e => e.Price)
                .HasPrecision(10, 4);
        }
    }
}
