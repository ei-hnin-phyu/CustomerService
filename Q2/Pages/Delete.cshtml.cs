using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Q2.Context;
using Q2.Models;
using Q2.Repositories;

namespace Q2.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerProfileRepository _repository;

        public DeleteModel(ICustomerProfileRepository repository)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cid = (int)id;
            await _repository.DeleteCustomerProfile(cid);

            return RedirectToPage("./Index");
        }
    }
}
