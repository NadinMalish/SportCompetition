namespace WebApplication.Models
{
    public class EditEventParticipantRequest
    {
        public bool IsCaptainConfirmed { get; set; }
        public int SetStatusId { get; set; }
        public string Comment { get; set; }
        public bool IsActual { get; set; }
        public int StatusId { get; set; }
    }
}
