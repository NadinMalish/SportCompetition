using System.ComponentModel.DataAnnotations;

namespace WebApplication.Core.Domain
{
    public class EventParticipant : BaseEntity
    {
        public Guid RoleId { get; set; } // id участника
        public Guid StatusId { get; set; } //id статуса
        public Guid SetStatusId { get; set; } //id того кто последний выставил статус
        [MaxLength(4096)] //?
        public string Comment { get; set; } 
        public DateTime DateTime { get; set; } //Дата подачи заявки
        public Guid PotentId { get; set; } //Чья заявка
        public Guid EventCompletitionId { get; set; } //id мероприятия
        public Guid TeamId { get; set; } //id команды
        public bool IsDeleted { get; set; } = false;
        public Role Role { get; set; } 
        public ApplicationStatus Status { get; set; }
    }
}
