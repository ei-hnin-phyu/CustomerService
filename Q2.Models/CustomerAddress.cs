namespace Q2.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
