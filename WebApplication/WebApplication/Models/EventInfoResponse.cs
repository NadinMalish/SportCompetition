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
        /// Дата и время регистрации участников
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Завершён ли ввод данных о мероприятии
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Когда создано
        /// </summary>
        public DateTime RegistryDate { get; set; }
        ///// <summary>
        ///// Кто зарегистрировал мероприятие (организатор)
        ///// </summary>
        //public required Potent Organizer { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
