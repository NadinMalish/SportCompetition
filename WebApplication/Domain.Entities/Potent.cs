using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Pass { get; set; } = null!;
        public DateTime DatReg { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<EventInfo> Events { get; set; } = new List<EventInfo>();
        public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();
    }
}
