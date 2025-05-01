namespace Domain.Entities
{
    /// <summary>
    /// Команда мероприятия
    /// </summary>
    public class Team : BaseEntity
    {
        /// <summary>
        /// Название команды
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Когда создана
        /// </summary>
        public DateTime RegistryDate { get; set; }
        /// <summary>
        /// Кто создал команду (капитан)
        /// </summary>
        public required Potent Captain { get; set; }
        public required int CaptainId { get; set; }

        /// <summary>
        /// Является ли команда согласованной администрацией мероприятия?
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// Комментарий (почему команда отклонена)
        /// </summary>
        public string? RejectNote { get; set; }

        /// <summary>
        /// Кто проверил команду
        /// </summary>
        public Potent? Considerer { get; set; }
        public int? ConsidererId { get; set; }

        /// <summary>
        /// Мероприятие
        /// </summary>
        public required Competition CompetitionOfEvent { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
