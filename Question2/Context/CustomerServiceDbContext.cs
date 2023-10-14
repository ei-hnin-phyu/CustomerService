using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Question2.Models;

namespace Question2.Context
{
    public class CustomerServiceDbContext : DbContext
    {
        public CustomerServiceDbContext(DbContextOptions<CustomerServiceDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(p => p.Addresses)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId);
        }
    }
}