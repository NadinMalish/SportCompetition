namespace Domain.Entities
{
    /// <summary>
    /// Документы
    /// </summary>
    public class Doc : BaseEntity
    {
        public string DocId { get; set; }
        public string? FileName { get; set; }
        public string? CommentDoc { get; set; }
        public int? IdDocType { get; set; }
        public int? IdEvent { get; set; }
        public int? IdCompetition { get; set; }

        public virtual DocType? DocType { get; set; }
        public virtual EventInfo? EventInfo { get; set; }
        public virtual Competition? Competition { get; set; }
    }
}
