using Microsoft.EntityFrameworkCore;
using ScheduleAppApi.Domain.Models;

namespace ScheduleAppApi.Infrastructure.Database
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ScheduledTime> ScheduledTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.ScheduledTimes)
                .WithOne(st => st.User)
                .HasForeignKey(st => st.UserId);
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(
                    r => (int)r,
                    r => (ERole)r);
        }
    }
}
