using SportCompetition.Domain;

namespace SportCompetition.Model
{
    public class DocTypeShortResponse 
    {
        public int Id { get; set; }
        public string Name_doc_type { get; set; } = null!;
        public string? Comment_doc { get; set; }
    }
}
