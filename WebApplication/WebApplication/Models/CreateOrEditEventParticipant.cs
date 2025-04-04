namespace WebApplication.Models
{
    public class CreateOrEditEventParticipant
    {
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public Guid RoleId { get; set; }
        public Guid StatusId { get; set; }
    }
}
