namespace Domain.Entities
{
    /// <summary>
    /// Потенциальный участник
    /// </summary>
    public class Potent : BaseEntity
    {
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string? Surname { get; set; }
        public DateOnly DateBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Login { get; set; } = null!;
        public DateTime DatReg { get; set; }

        public virtual ICollection<EventInfo> Events { get; set; } = new List<EventInfo>();
        public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();
    }
}
