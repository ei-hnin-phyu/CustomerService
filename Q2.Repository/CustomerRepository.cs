using Microsoft.EntityFrameworkCore;
using Q2.Models;
using System.Data.Common;

namespace Q2.Repository
{
    public interface ICustomerProfileRepository
    {
        public Task CreateCustomerProfile(Customer customer);
        public Task UpdateCustomerProfile(Customer customer);
        public Task DeleteCustomerProfile(int id);
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

        public async Task DeleteCustomerProfile(int id)
        {
            var customer = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<Customer> GetCustomerProfile(int id)
        {
            return await _context.Customers.Include(x => x.Addresses).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateCustomerProfile(Customer customer)
        {
            var existcustomer = await _context.Customers.Include(c => c.Addresses).Where(c => c.Id == customer.Id).FirstOrDefaultAsync();
            if (existcustomer != null)
            {
                existcustomer.Name = customer.Name;
                existcustomer.Email = customer.Email;
                existcustomer.Phone = customer.Phone;
                existcustomer.Remarks = customer.Remarks;
                existcustomer.Addresses = customer.Addresses;
                existcustomer.Fax = customer.Fax;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Customer>> GetAllCustomerProfiles()
        {
            return await _context.Customers.ToListAsync();
        }

    }
}
