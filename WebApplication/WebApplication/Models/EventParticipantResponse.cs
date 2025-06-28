namespace WebApplication.Models
{
    public class EventParticipantResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
<<<<<<< HEAD
        public required ApplicationStatusResponse Status { get; set; }
=======
        public RoleResponse Role { get; set; }
        public ApplicationStatusResponse Status { get; set; }
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
    }
}
