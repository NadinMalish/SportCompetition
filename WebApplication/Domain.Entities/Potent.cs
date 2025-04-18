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
        public string lastname { get; set; } = null!;
        public string firstname { get; set; } = null!;
        public string? surname { get; set; }
        public DateOnly date_birth { get; set; }
        public int gender { get; set; }
        public string email { get; set; } = null!;
        public string login { get; set; } = null!;
        public string pass { get; set; } = null!;
        public DateTime dat_reg { get; set; }
        public bool deleted { get; set; }
    }
}
