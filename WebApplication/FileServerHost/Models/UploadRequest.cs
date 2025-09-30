namespace FileServerHost.Models
{
    public record UploadRequest(IFormFile File, string? Owner);
}
