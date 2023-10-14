using Microsoft.EntityFrameworkCore;
using Q2.Context;
using Q2.Models;

namespace Q2.Repositories
{
    public interface ICustomerProfileRepository
    {
        public Task CreateCustomerProfile(Customer customer);
        public Task UpdateCustomerProfile();
        public Task DeleteCustomerProfile();
        public Task<Customer> GetCustomerProfile(int id);
        public Task<List<Customer>> GetAllCustomerProfiles();
    }
    public class CustomerRepository : ICustomerProfileRepository
    {
        private readonly CustomerServiceContext _context;
        public CustomerRepository(CustomerServiceContext dbContext)
        {
            _context = dbContext;
        }
        public async Task CreateCustomerProfile(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public Task DeleteCustomerProfile()
        {
            throw new NotImplementedException();
        }

        public Task GetAllCustomerProfiles()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerProfile(int id)
        {
            return await _context.Customers.Include(x => x.Addresses).FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task UpdateCustomerProfile()
        {
            throw new NotImplementedException();
        }

        async Task<List<Customer>> ICustomerProfileRepository.GetAllCustomerProfiles()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
