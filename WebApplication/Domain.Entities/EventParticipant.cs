using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Участник мероприятия|Заявка
    /// </summary>
    public class EventParticipant : BaseEntity
    {
        /// <summary>
        /// Статус заявки участника
        /// </summary>
        public required ApplicationStatus Status { get; set; }
        public int ApplicationStatusId { get; set; }

        /// <summary>
        /// Дата подачи заявки
        /// </summary>
        public DateTime DateTime { get; set; }

        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Соревнование, на которое была подана заявка
        /// </summary>
        public required Competition ParticipantCompetition { get; set; }
        public int ParticipantCompetitionId { get; set; }
    }
}
