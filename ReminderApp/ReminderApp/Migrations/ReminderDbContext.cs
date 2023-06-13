using Microsoft.EntityFrameworkCore;
using ReminderApp.Models;


namespace ReminderApp.Migrations
{
    public class ReminderDbContext : DbContext
    {
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=Reminder;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reminder>()
                .HasKey(r => r.R_Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.U_Id);

            modelBuilder.Entity<Reminder>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.U_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
