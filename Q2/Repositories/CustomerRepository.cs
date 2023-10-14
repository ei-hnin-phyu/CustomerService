using Microsoft.EntityFrameworkCore;
using Q2.Context;
using Q2.Models;

namespace Q2.Repositories
{
    public interface ICustomerProfileRepository
    {
        public Task CreateCustomerProfile(Customer customer);
        public Task UpdateCustomerProfile(Customer customer);
        public Task DeleteCustomerProfile();
        public Task<Customer> GetCustomerProfile(int id);
        public Task<List<Customer>> GetAllCustomerProfiles();
    }
    public class CustomerRepository : ICustomerProfileRepository
    {
        private readonly ILogger<CustomerRepository> logger;
        private readonly CustomerServiceContext _context;
        public CustomerRepository(CustomerServiceContext dbContext, ILogger<CustomerRepository> logger)
        {
            _context = dbContext;
            this.logger = logger;
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

        public async Task UpdateCustomerProfile(Customer customer)
        {
            try
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
            }catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
            }
        }

        async Task<List<Customer>> ICustomerProfileRepository.GetAllCustomerProfiles()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
