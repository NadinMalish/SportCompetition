using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class DocumentGateway
    {
        private readonly HttpClient _http;
        private static readonly JsonSerializerOptions _json = new(JsonSerializerDefaults.Web);

        public DocumentGateway(HttpClient http)
        {
            _http = http;
        }

        public async Task<(string id, string checksum)> UploadAsync(IFormFile file, string? owner, string? description, CancellationToken ct = default)
        {
            using var content = new MultipartFormDataContent();
            var stream = file.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType ?? "application/octet-stream");
            content.Add(fileContent, "File", file.FileName);
            if (!string.IsNullOrWhiteSpace(owner)) content.Add(new StringContent(owner), "Owner");
            if (!string.IsNullOrWhiteSpace(description)) content.Add(new StringContent(description), "Description");

            using var resp = await _http.PostAsync("/documents", content, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                throw new InvalidOperationException($"Upload failed: {(int)resp.StatusCode} {resp.ReasonPhrase} — {body}");
            }
            var json = await resp.Content.ReadAsStringAsync(ct);
            var doc = JsonSerializer.Deserialize<UploadResponse>(json, _json)!;
            return (doc.Id!, doc.Checksum!);
        }

        public async Task<Stream> DownloadAsync(string id, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"/documents/{id}", HttpCompletionOption.ResponseHeadersRead, ct);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                throw new FileNotFoundException($"File not found or error: {(int)resp.StatusCode} {resp.ReasonPhrase} — {body}");
            }
            return await resp.Content.ReadAsStreamAsync(ct);
        }

        public async Task<DocumentInfoDto?> GetInfoAsync(string id, CancellationToken ct = default)
        {
            var resp = await _http.GetAsync($"/documents/{id}/info", ct);
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync(ct);
            return JsonSerializer.Deserialize<DocumentInfoDto>(json, _json);
        }

        public async Task<bool> UpdateMetadataAsync(string id, string? owner, string? description, IEnumerable<string>? tags, CancellationToken ct = default)
        {
            var payload = new { Owner = owner, Description = description, Tags = tags?.ToArray() };
            var resp = await _http.PutAsync($"/documents/{id}/metadata",
            new StringContent(JsonSerializer.Serialize(payload, _json), Encoding.UTF8, "application/json"), ct);
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return false;
            resp.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"/documents/{id}", ct);
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return false;
            resp.EnsureSuccessStatusCode();
            return true;
        }

        private sealed class UploadResponse
        {
            public string? Id { get; set; }
            public string? Checksum { get; set; }
        }
    }
}
