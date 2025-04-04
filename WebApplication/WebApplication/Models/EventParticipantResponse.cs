namespace WebApplication.Models
{
    public class EventParticipantResponse
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public RoleResponse Role { get; set; }
        public ApplicationStatusResponse Status { get; set; }
    }
}
