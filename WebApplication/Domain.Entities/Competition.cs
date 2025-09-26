namespace Domain.Entities
{
    /// <summary>
    /// Состязание мероприятия
    /// </summary>
    public class Competition : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Описание состязания
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Тип состязания
        /// </summary>
        public required CompetitionTypes CompetitionType { get; set; } = CompetitionTypes.Single;
        /// <summary>
        /// Дата и время начала состязания
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// Дата и время окончания состязания
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Завершён ли ввод данных о состязании
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Когда создано
        /// </summary>
        public DateTime RegistryDate { get; set; }

        
        /// <summary>
        /// Мероприятие
        /// </summary>
        public required EventInfo Event { get; set; }
        public required int EventId { get; set; }

        public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();
    }
}
