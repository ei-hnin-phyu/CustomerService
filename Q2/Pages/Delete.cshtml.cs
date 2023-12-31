﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

using Q2.Models;
using Q2.Repository;

namespace Q2.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerProfileRepository _repository;
        private readonly ILogger<EditModel> _logger;
        public DeleteModel(ICustomerProfileRepository repository, ILogger<EditModel> logger)
        {
            _repository = repository;
            _logger = logger;
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
            _logger.LogInformation($"Customere Deleted {id}");
            return RedirectToPage("./Index");
        }
    }
}
