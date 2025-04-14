using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationStatus : BaseEntity
    {
        [MaxLength(128)]
        public string Name { get; set; }
        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
