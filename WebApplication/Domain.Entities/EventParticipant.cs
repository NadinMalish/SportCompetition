using System.ComponentModel.DataAnnotations;

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
        /// Подтверждение от капитана команды
        /// </summary>
        public bool IsCaptainConfirmed { get; set; } //TODO: Мы не заменили это поле статусом заявки?

        /// <summary>
        /// Код статуса
        /// </summary>
        public int StatusId { get; set; }
        
        /// <summary>
        /// Код пользователя, менявший статус последним
        /// </summary>
        public int? SetStatusId { get; set; }
        
        /// <summary>
        /// Комментарий к заявке участника
        /// </summary>
        [MaxLength(4096)]
        public string? Comment { get; set; } 

        /// <summary>
        /// Участие подтверждено
        /// </summary>
        public bool IsActual { get; set; }

        /// <summary>
        /// Дата подачи заявки
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Код пользователя, подавшего заявку
        /// </summary>
        //public Guid PotentId { get; set; } 

        /// <summary>
        /// Код состязания участника
        /// </summary>
        //public Guid EventCompetitionId { get; set; }

        /// <summary>
        /// Код команды участника
        /// </summary>
        //public Guid TeamId { get; set; } //TODO: поле допускает null?
        public bool IsDeleted { get; set; } = false;
        
        /// <summary>
        /// Роль участника
        /// </summary>
        public Role Role { get; set; } 
        
        /// <summary>
        /// Статус заявки участника
        /// </summary>
        public ApplicationStatus Status { get; set; }
    }
}
