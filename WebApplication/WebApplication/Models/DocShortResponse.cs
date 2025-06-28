using Domain.Entities;

namespace WebApplication.Models
{
    public class DocShortResponse
    {
        public int Id { get; set; }
        public string? Name_doc { get; set; }
        public string? File_name { get; set; }
        public string? Comment_doc { get; set; }
<<<<<<< HEAD
=======

        //public DocType? Doc_type { get; set; }
        //public EventInfo? Event { get; set; }
        //public Competition? Competition { get; set; }
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        public int? Id_doc_type { get; set; }
        public int? Id_event { get; set; }
        public int? Id_competition { get; set; }
    }
}
