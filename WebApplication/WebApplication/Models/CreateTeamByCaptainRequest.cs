namespace WebApplication.Models
{
    public class CreateTeamByCaptainRequest
    {
        /// <summary>
        /// Название команды
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Кто создал команду (капитан?)
        /// </summary>
        public required int EditorId { get; set; }

        /// <summary>
        /// Состязание
        /// </summary>
        public required int CompetitionId { get; set; }
    }
}
