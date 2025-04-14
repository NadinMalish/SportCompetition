using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Роль участника
    /// </summary>
    public class Role: BaseEntity
    {
        [MaxLength(128)]
        public string Name { get; set; }
        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
