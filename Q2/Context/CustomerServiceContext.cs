using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2.Context
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
            optionsBuilder.UseSqlite("Data Source=customermanager.db");
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
}
