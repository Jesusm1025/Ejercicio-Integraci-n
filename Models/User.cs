namespace ApiUsuario.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Phone> Phones { get; set; } = new List<Phone>();
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Modified { get; set; } = DateTime.UtcNow;
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;
        public Guid Token { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = false;
    }
    public class Phone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Number { get; set; }
        public string CityCode { get; set; }
        public string CountryCode { get; set; }
    }
}
