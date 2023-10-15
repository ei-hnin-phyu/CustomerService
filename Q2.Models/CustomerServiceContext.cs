using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Q2.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Q2.Models
{
    public class CustomerServiceContext : DbContext
    {
        
        public CustomerServiceContext(DbContextOptions<CustomerServiceContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite($"Data Source={Constants.path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>()
                .HasMany(p => p.Addresses)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId);
        }
    }

    public class DbContextFactory : IDesignTimeDbContextFactory<CustomerServiceContext>
    {
        public CustomerServiceContext CreateDbContext(string[] args)
        {            
            var optionsBuilder = new DbContextOptionsBuilder<CustomerServiceContext>();
            optionsBuilder.UseSqlite($"Data Source={Constants.path}");

            return new CustomerServiceContext(optionsBuilder.Options);
        }
    }
    public static class Constants
    {
        public static string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, "customerprofile.db");
    }
}
