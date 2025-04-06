using System.Security.Cryptography;

namespace SportCompetition.Domain.Entities
{
    /// <summary>
    /// Сущность с идентификатором
    /// </summary>
    public class BaseEntity
    {
        public int Id { get; set; }
    }
}
