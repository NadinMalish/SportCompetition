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
        public required CompetitionTypes CompetitionType { get; set; }
        /// <summary>
        /// Дата и время начала состязания
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// Дата и время окончания состязания
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
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
    }
}
