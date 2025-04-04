using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportCompetition.Domain;

public partial class DocType: BaseEntity
{

    public string name_doc_type { get; set; } = null!;

    public string? comment_doc { get; set; }

    public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();
}
