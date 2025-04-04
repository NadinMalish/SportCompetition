using System.ComponentModel.DataAnnotations;

namespace WebApplication.Core.Domain
{
    public class ApplicationStatus : BaseEntity
    {
        [MaxLength(128)]
        public string Name { get; set; }
        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
