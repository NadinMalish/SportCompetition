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
        public string name_doc_type { get; set; } = null!;
        public string? comment_doc { get; set; }
        
        public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();
    }
}
