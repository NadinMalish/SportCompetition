using Domain.Entities;

namespace WebApplication.Models
{
    public class DocShortResponse
    {
        public int Id { get; set; }
        public string? Name_doc { get; set; }
        public string? File_name { get; set; }
        public string? Comment_doc { get; set; }
        public int? Id_doc_type { get; set; }
        public int? Id_event { get; set; }
        public int? Id_competition { get; set; }
    }
}
