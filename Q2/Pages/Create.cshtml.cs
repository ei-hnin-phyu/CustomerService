using System;
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
        private readonly ICustomerProfileRepository _repository;

        public CreateModel(ICustomerProfileRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            Input = new CreateInput
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
                await _repository.CreateCustomerProfile(customer);               


                return RedirectToPage("./Index");
            }catch (Exception ex)
            {
                throw;
            }
        }
        [BindProperty]
        public CreateInput Input { get; set; }
        public class CreateInput
        {
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Phone")]
            public string Phone { get; set; }
            [Display(Name = "Photo")]
            public IFormFile? Photo { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Fax")]
            public string? Fax { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Remarks")]
            public string? Remarks { get; set; }
            public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();
            public async Task<Customer> convertToCustomerAsync()
            {
                string fileName = Photo.FileName;
                byte[] fileBytes;

                using (var stream = new MemoryStream())
                {
                    await Photo.CopyToAsync(stream);
                    fileBytes = stream.ToArray();
                }
                var customer = new Customer
                {
                    Email = Email,
                    Photo = fileBytes,
                    Fax = Fax,
                    Name = Name,
                    Phone = Phone,
                    Remarks = Remarks,
                    Addresses = Addresses.Select(x => new CustomerAddress
                    {
                        Address = x.Address,
                        City = x.City,
                        Country = x.Country,
                        PostalCode = x.PostalCode,
                        Type = x.Type
                    }).ToList()
                };
                return customer;
            }

        }
    }   

}
