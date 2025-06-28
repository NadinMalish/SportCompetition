using Domain.Entities;

namespace WebApplication.Models
{
    public class CreateCompetitionByOrganizerRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Тип состязания
        /// </summary>
<<<<<<< HEAD
        public required CompetitionTypes CompetitionType { get; set; } = CompetitionTypes.Single;
=======
        public required CompetitionTypes CompetitionType { get; set; }
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        /// <summary>
        /// Дата и время начала состязания
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// Дата и время окончания состязания
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
<<<<<<< HEAD
        /// Идентификатор мероприятия, в рамках которого проходит состязание
        /// </summary>
        public int EventId { get; set; }
=======
        /// Минимальное количество членов команды
        /// </summary>
        public int? MinComandSize { get; set; }
        /// <summary>
        /// Максимальное количество членов команды
        /// </summary>
        public int? MaxComandSize { get; set; }

        /// <summary>
        /// Идентификатор мероприятия, в рамках которого проходит состязание
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Идентификатор того, кто создал состязание
        /// </summary>
        public int EditorId { get; set; }
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
    }
}
