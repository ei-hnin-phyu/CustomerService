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
    public class IndexModel : PageModel
    {
        private readonly ICustomerProfileRepository _repository;

        public IndexModel(ICustomerProfileRepository repository)
        {
            _repository = repository;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Customer = await _repository.GetAllCustomerProfiles();
        }
    }
}
