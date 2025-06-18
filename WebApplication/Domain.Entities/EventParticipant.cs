using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Участник мероприятия|Заявка
    /// </summary>
    public class EventParticipant : BaseEntity
    {       
        /// <summary>
        /// Код статуса
        /// </summary>
        public int ApplicationStatusId { get; set; }

        /// <summary>
        /// Дата подачи заявки
        /// </summary>
        public DateTime DateTime { get; set; }

        public bool IsDeleted { get; set; } = false;

        
        /// <summary>
        /// Статус заявки участника
        /// </summary>
        public required ApplicationStatus Status { get; set; }
    }
}
