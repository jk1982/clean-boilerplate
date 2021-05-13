namespace API.DTO
{
    public class PersonContactDTO
    {
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public AddressDTO Address { get; set; }
    }
}