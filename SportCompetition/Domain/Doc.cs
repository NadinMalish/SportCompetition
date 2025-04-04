using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportCompetition.Model;

namespace SportCompetition.Domain;

public partial class Doc: BaseEntityDelExt
{

    public string? name_doc { get; set; }

    public string? file_name { get; set; }

    public byte[]? docum { get; set; }

    public string? comment_doc { get; set; }

    public int? id_doc_type { get; set; }

    public int? id_event { get; set; }

    public int? id_event_competition { get; set; }

    //public bool Deleted { get; set; }

    public DocType? DocType { get; set; }
}
