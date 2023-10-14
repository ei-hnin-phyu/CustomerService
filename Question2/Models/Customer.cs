namespace Question2.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<CustomerAddress> Addresses { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Remarks { get; set; }
    }
}
