namespace WebApplication.Models
{
    public class CreateDocRequest
    {
        public int? IdDocType { get; set; }
        public int? IdEvent { get; set; }
        public int? IdCompetition { get; set; }
        public string? CommentDoc { get; set; }
        public string? Owner { get; set; }
        public string? Description { get; set; }
        public IFormFile File { get; set; }
    }
}
