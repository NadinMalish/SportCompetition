namespace FileServerHost.Models
{
    public record UploadRequest(
    IFormFile File,
    string? Owner,
    string? Description,
    List<string>? Tags
);
}
