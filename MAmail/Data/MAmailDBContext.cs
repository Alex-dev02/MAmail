using MAmail.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAmail.Data
{
    public class MAmailDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Not safe
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MAmailDB;Trusted_Connection=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
    }
}
