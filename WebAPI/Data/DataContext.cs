using System;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User>Users { get; set; }
         public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity models or relationships here
        }

        public void InitializeDatabase()
        {
            // Check if the database has been created
            if (Database.EnsureCreated())
            {
                // Database was just created, so seed the initial data
                SeedData();
            }
        }

        private void SeedData()
        {
            // Perform your data seeding here
            Users.Add(new User { Name = "John Wick" });
            Users.Add(new User { Name = "Eben Varghese Paul" });
            SaveChanges();
        }
    }
}

