﻿namespace SportCompetition.WebApi.Models
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
        /// Обратная связь
        /// </summary>
        public string? Feedback { get; set; }

        /// <summary>
        /// Дата и время начала мероприятия
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// Дата и время окончания мероприятия
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
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

        ///// <summary>
        ///// Кто зарегистрировал мероприятие (организатор)
        ///// </summary>
        //public required Potent Organizer { get; set; }
    }
}
