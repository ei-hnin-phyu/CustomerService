using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Context;
using Q2.Models;
using Q2.Repositories;

namespace Q2.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Q2.Context.CustomerServiceContext _context;
        private readonly ICustomerProfileRepository _customerProfileRepository;

        public DetailsModel(Q2.Context.CustomerServiceContext context,ICustomerProfileRepository repository)
        {
            _customerProfileRepository = repository;
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }
            int cid = (int)id;
            var customer = await _customerProfileRepository.GetCustomerProfile(cid);
            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;                
            }
            return Page();
        }
    }
}
