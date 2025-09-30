namespace WebApplication.Models
{
    public sealed class DocResponse
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? CommentDoc { get; set; }
        public int? IdDocType { get; set; }
        public int? IdEvent { get; set; }
        public int? IdCompetition { get; set; }
    }
}
