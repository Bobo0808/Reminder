using Microsoft.EntityFrameworkCore;
using ReminderApp.Models;

namespace ReminderApp.Data
{
    public class ReminderDbContext : DbContext
    {
        public ReminderDbContext(DbContextOptions<ReminderDbContext> options) : base(options)
        {

        }
        public DbSet<Reminder> Reminder { get; set; }
        public DbSet<User> User { get; set; }
      


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=Reminder;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
           


        //}
    }
}
