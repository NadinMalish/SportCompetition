namespace WebApplication.Models
{
    /// <summary>
    /// Запрос на создание мероприятия
    /// </summary>
    public class CreateOrEditEventByOrganizerRequest
    {
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
        /// Дата и время регистрации участников
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Кто зарегистрировал мероприятие (организатор)
        /// </summary>
        public required int OrganizerId { get; set; }
    }
}
