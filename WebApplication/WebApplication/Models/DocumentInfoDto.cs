namespace WebApplication.Models
{
    public sealed class DocumentInfoDto
    {
        public string Id { get; set; } = default!;
        public string? Filename { get; set; }
        public long Length { get; set; }
        public DateTime UploadDate { get; set; }
        public Dictionary<string, object?> Metadata { get; set; } = new();
    }
}
