﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Q2.Context;
using Q2.Models;
using Q2.Repositories;

namespace Q2.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerProfileRepository _customerProfileRepository;

        public CreateModel(ICustomerProfileRepository repository)
        {
            _customerProfileRepository = repository;
        }

        public IActionResult OnGet()
        {
            Input = new CustomerDTO
            {
                Addresses = new List<AddressDTO> { new AddressDTO() }
            };
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid || Input == null)
                {
                    return Page();
                }

                var customer = await Input.convertToCustomerAsync();
                await _customerProfileRepository.CreateCustomerProfile(customer);               


                return RedirectToPage("./Index");
            }catch (Exception ex)
            {
                throw;
            }
        }
        [BindProperty]
        public CustomerDTO Input { get; set; }
    }   

}