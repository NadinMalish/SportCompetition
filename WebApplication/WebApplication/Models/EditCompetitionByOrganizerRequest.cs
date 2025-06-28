using Domain.Entities;

namespace WebApplication.Models
{
    public class EditCompetitionByOrganizerRequest
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
<<<<<<< HEAD
=======

        /// <summary>
        /// Минимальное количество членов команды
        /// </summary>
        public int? MinComandSize { get; set; }
        /// <summary>
        /// Максимальное количество членов команды
        /// </summary>
        public int? MaxComandSize { get; set; }
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
    }
}
