using static Q2.Pages.CreateModel;
using System.ComponentModel.DataAnnotations;
namespace Q2.Models
{
    public class CustomerDTO
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
