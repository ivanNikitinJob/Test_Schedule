using Microsoft.EntityFrameworkCore;
using Entities;

namespace Database
{
    public class EFContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Interval> Intervals { get; set; }

        public EFContext(DbContextOptions<EFContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasData(DataSeed.Groups);
            modelBuilder.Entity<Address>()
                .HasData(DataSeed.Addresses);
            modelBuilder.Entity<Schedule>();
            modelBuilder.Entity<Interval>()
                .HasData(DataSeed.Intervals);

            base.OnModelCreating(modelBuilder);
        }
    }
}
