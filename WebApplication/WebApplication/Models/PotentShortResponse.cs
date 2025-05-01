namespace WebApplication.Models
{
    public class PotentShortResponse
    {
        public int Id { get; set; }
        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string? Surname { get; set; }

        public DateOnly date_birth { get; set; }

        public string gender { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }

    }
}
