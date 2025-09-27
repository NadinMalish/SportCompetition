namespace FileServerHost.Models
{
    public record UpdateMetadataRequest(
    string? Owner,
    string? Description,
    List<string>? Tags
);
}
