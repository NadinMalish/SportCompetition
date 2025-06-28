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
=======
        /// Минимальное количество членов команды
        /// </summary>
        public int? MinComandSize { get; set; }
        /// <summary>
        /// Максимальное количество членов команды
        /// </summary>
        public int? MaxComandSize { get; set; }

        /// <summary>
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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

        /// <summary>
<<<<<<< HEAD
=======
        /// Кто зарегистрировал состязание
        /// </summary>
        public required Potent Editor { get; set; }
        public required int EditorId { get; set; }

        /// <summary>
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        /// Признак удаления
        /// </summary>
        public bool IsDeleted { get; set; }

        public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();
    }
}
