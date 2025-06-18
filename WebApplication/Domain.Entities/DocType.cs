namespace Domain.Entities
{
    /// <summary>
    /// Справочник категории документов
    /// </summary>
    public class DocType : BaseEntity
    {
        public string NameDocType { get; set; } = null!;
        public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();
    }
}
