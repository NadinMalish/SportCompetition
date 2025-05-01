using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Документы
    /// </summary>
    public class Doc : BaseEntity
    {
        public string? NameDoc { get; set; }
        public string? FileName { get; set; }
        public byte[]? Docum { get; set; }
        public string? CommentDoc { get; set; }
        public int? IdDocType { get; set; }
        public int? IdEvent { get; set; }
        public int? IdCompetition { get; set; }
        public bool Deleted { get; set; }

        public virtual DocType? DocType { get; set; }
        public virtual EventInfo? EventInfo { get; set; }
        public virtual Competition? Competition { get; set; }
    }
}
