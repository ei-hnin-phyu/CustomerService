namespace Q2.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public byte[] Photo { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Remarks { get; set; }
    }
}
