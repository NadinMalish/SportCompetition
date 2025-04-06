namespace SportCompetition.WebApi.Models
{
    public class TeamResponse
    {
        public int Id { get; set; }
        /// <summary>
        /// Название команды
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Когда создана
        /// </summary>
        public DateTime RegistryDate { get; set; }

        /// <summary>
        /// Является ли команда согласованной администрацией мероприятия?
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// Комментарий (почему команда отклонена)
        /// </summary>
        public string? RejectNote { get; set; }

        ///// <summary>
        ///// Мероприятие
        ///// </summary>
        //public required EventInfo Event { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
