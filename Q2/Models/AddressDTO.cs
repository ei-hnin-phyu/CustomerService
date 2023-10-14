using System.ComponentModel.DataAnnotations;

namespace Q2.Models
{
    public class AddressDTO
    {
        public string Type { get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
    }
}
