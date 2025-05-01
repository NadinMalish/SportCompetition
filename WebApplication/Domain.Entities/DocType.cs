using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Справочник категории документов
    /// </summary>
    public class DocType : BaseEntity
    {
        public string NameDocType { get; set; } = null!;
        public string? CommentDoc { get; set; }
        
        public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();
    }
}
