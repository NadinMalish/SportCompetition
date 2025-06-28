using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationStatus : BaseEntity
    {
        [MaxLength(128)]
<<<<<<< HEAD
        public required string Name { get; set; }
=======
        public string Name { get; set; }
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
