namespace WebApplication.Models
{
    public class CreateOrEditEventParticipant
    {
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
    }
}
