namespace Question2.Models
{
    public class CustomerAddress
    {
        public string Type { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
