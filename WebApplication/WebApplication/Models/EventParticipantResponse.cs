namespace WebApplication.Models
{
    public class EventParticipantResponse
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public required ApplicationStatusResponse Status { get; set; }
    }
}
