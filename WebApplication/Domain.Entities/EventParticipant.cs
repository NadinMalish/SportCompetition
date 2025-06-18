namespace Domain.Entities
{
    /// <summary>
    /// Участник мероприятия|Заявка
    /// </summary>
    public class EventParticipant : BaseEntity
    {
        /// <summary>
        /// Код участника
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Код статуса
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Код состязания в котором хочет участвовать пользователь
        /// </summary>
        public int CompetitionId { get; set; }

        /// <summary>
        /// Дата подачи заявки
        /// </summary>
        public DateTime DateTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        
        /// <summary>
        /// Статус заявки участника
        /// </summary>
        public ApplicationStatus Status { get; set; }
        public Competition Competition { get; set; }
    }
}
