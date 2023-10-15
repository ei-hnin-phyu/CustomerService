using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol.Core.Types;
using Q2.Models;
using Q2.Repository;
using System.Net.Sockets;

namespace Q2.Tests
{
    public class UnitTest1
    {
        private readonly SqliteConnection _connection;
        private readonly CustomerServiceContext _context;
        
        public UnitTest1()
        {
            // Set up an in-memory database for testing
            _connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<CustomerServiceContext>()
                .UseSqlite(_connection)
                .Options;
            
            _context = new CustomerServiceContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task CreateCustomer_ShouldAddNewCustomerAsync()
        {
            // Arrange
            var repository = new CustomerRepository(_context);
            Customer customer = new Customer { Name = "John Doe", Email = "johndoe@testmail.com", Phone = "082357" };
            // Act
            await repository.CreateCustomerProfile(customer);

            // Assert
            var savedCustomer = _context.Customers.Where(x => x.Id == customer.Id).FirstOrDefault();
            Assert.Equal(customer.Name, savedCustomer.Name);
        }

        [Fact]
        public async Task ReadCustomer_ShouldRetrieveCustomerAsync()
        {
            // Arrange
            var repository = new CustomerRepository(_context);
            var customer = new Customer { Name = "Alice", Email = "alice@testmail.com", Phone = "123456" };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Act
            var retrievedCustomer = await repository.GetCustomerProfile(customer.Id);

            // Assert
            Assert.NotNull(retrievedCustomer);
            Assert.Equal(customer.Name, retrievedCustomer.Name);
        }

        [Fact]
        public async Task UpdateCustomer_ShouldUpdateCustomerAsync()
        {
            var repository = new CustomerRepository(_context);
            var customer = new Customer { Name = "Bob", Email = "bob@testmail.com",Phone="123456" };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Act
            customer.Name = "Bob Smith";
            await repository.UpdateCustomerProfile(customer);

            // Assert
            var updatedCustomer = _context.Customers.Where(c => c.Id == customer.Id).FirstOrDefault();
            Assert.Equal(customer.Name, updatedCustomer.Name);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldRemoveCustomerAsync()
        {
            // Arrange
            var repository = new CustomerRepository(_context);
            var customer = new Customer { Name = "Eve", Email = "eve@testmail.com", Phone = "123456" };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Act
            await repository.DeleteCustomerProfile(customer.Id);

            // Assert
            var deleted = _context.Customers.Where(c => c.Id == customer.Id);
            Assert.Empty(deleted);
        }
    }
}