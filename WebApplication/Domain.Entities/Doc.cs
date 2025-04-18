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
        public string? name_doc { get; set; }
        public string? file_name { get; set; }
        public byte[]? docum { get; set; }
        public string? comment_doc { get; set; }
        public int? id_doc_type { get; set; }
        public int? id_event { get; set; }
        public int? id_event_competition { get; set; }
        public bool deleted { get; set; }

        public DocType? DocType { get; set; }
    }
}
