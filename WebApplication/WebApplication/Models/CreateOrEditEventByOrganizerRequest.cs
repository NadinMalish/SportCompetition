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
<<<<<<< HEAD
=======
        /// <summary>
        /// Обратная связь
        /// </summary>
        public string? Feedback { get; set; }
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3

        /// <summary>
        /// Дата и время начала мероприятия
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// Дата и время окончания мероприятия
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
<<<<<<< HEAD
        /// Дата и время регистрации участников
        /// </summary>
        public DateTime RegistrationDate { get; set; }
=======
        /// Дата и время начала регистрации участников
        /// </summary>
        public DateTime StartRegistrationDate { get; set; }
        /// <summary>
        /// Дата и время окончания регистрации участников
        /// </summary>
        public DateTime FinishRegistrationDate { get; set; }
        /// <summary>
        /// Дата начала контроля актуальности
        /// </summary>
        public DateTime? StartActualControlDate { get; set; }
        /// <summary>
        /// Дата окончания контроля актуальности
        /// </summary>
        public DateTime? FinishActualControlDate { get; set; }
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3

        /// <summary>
        /// Кто зарегистрировал мероприятие (организатор)
        /// </summary>
        public required int OrganizerId { get; set; }
    }
}
