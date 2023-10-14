using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Q2.Context;
using Q2.Models;
using Q2.Repositories;

namespace Q2.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICustomerProfileRepository _repository;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ICustomerProfileRepository repository, ILogger<EditModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Customer { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cid = (int)id;
            var customer = await _repository.GetCustomerProfile(cid);
            if (customer == null)
            {
                return NotFound();
            }
            Customer = convertToInputModel(customer);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try
            {
                await _repository.UpdateCustomerProfile(convertToCustomer(Customer));
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e.Message);
                throw;
            }

            return RedirectToPage("./Index");
        }
        public class InputModel
        {
            public int Id { get; set; }
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Phone")]
            public string Phone { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Fax")]
            public string? Fax { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Remarks")]
            public string? Remarks { get; set; }
            public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();
        }
        private InputModel convertToInputModel(Customer customer)
        {
            return new InputModel
            {
                Id = customer.Id,
                Email = customer.Email,
                Name = customer.Name,
                Phone = customer.Phone,
                Fax = customer.Fax,
                Remarks = customer.Remarks,
                Addresses = customer.Addresses.Select(x => new AddressDTO
                {
                    Address = x.Address,
                    City = x.City,
                    Country = x.Country,
                    PostalCode = x.PostalCode,
                    Type = x.Type,
                }).ToList(),
            };
        }
        private Customer convertToCustomer(InputModel customer)
        {
            return new Customer
            {
                Id = customer.Id,
                Email = customer.Email,
                Name = customer.Name,
                Phone = customer.Phone,
                Fax = customer.Fax,
                Remarks = customer.Remarks,
                Addresses = customer.Addresses.Select(x => new CustomerAddress
                {
                    Address = x.Address,
                    City = x.City,
                    Country = x.Country,
                    PostalCode = x.PostalCode,
                    Type = x.Type,
                }).ToList(),
            };
        }
    }
}
