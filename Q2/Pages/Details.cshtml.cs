using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Q2.Models;
using Q2.Repository;

namespace Q2.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerProfileRepository _repository;

        public DetailsModel(ICustomerProfileRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int cid = (int)id;
            var customer = await _repository.GetCustomerProfile(cid);
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
