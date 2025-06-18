namespace WebApplication.Models
{
    public class EventInfoResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Дата и время начала мероприятия
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// Дата и время окончания мероприятия
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Когда создано
        /// </summary>
        public DateTime RegistryDate { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
