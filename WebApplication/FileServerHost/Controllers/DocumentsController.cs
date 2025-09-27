using FileServerHost.Models;
using FileServerHost.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FileServerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly GridFsDocumentStore _store;

        public DocumentsController(GridFsDocumentStore store)
        {
            _store = store;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] UploadRequest req, CancellationToken ct)
        {
            if (req.File is null || req.File.Length == 0) return BadRequest("File is required");
            var (id, sha) = await _store.UploadAsync(req.File, req.Owner, req.Description, req.Tags, ct);
            return Ok(new { id = id.ToString(), checksum = sha });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Download(string id, CancellationToken ct)
        {
            if (!ObjectId.TryParse(id, out var oid)) return BadRequest("Invalid id");
            var res = await _store.DownloadAsync(oid, ct);
            if (res is null) return NotFound();
            var (stream, contentType, fileName) = res.Value;
            return File(stream, contentType, fileName);
        }

        [HttpGet("{id}/info")]
        public async Task<IActionResult> Info(string id, CancellationToken ct)
        {
            if (!ObjectId.TryParse(id, out var oid)) return BadRequest("Invalid id");
            var info = await _store.GetInfoAsync(oid, ct);
            return info is null ? NotFound() : Ok(info);
        }

        [HttpPut("{id}/metadata")]
        public async Task<IActionResult> UpdateMetadata(string id, [FromBody] UpdateMetadataRequest body, CancellationToken ct)
        {
            if (!ObjectId.TryParse(id, out var oid)) return BadRequest("Invalid id");
            var updated = await _store.UpdateMetadataAsync(oid, body.Owner, body.Description, body.Tags, ct);
            return updated ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string? owner, [FromQuery] string? tag, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 200);
            var items = await _store.ListAsync(owner, tag, page, pageSize, ct);
            return Ok(items);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken ct)
        {
            if (!ObjectId.TryParse(id, out var oid)) return BadRequest("Invalid id");
            try
            {
                await _store.DeleteAsync(oid, ct);
                return NoContent();
            }
            catch (MongoDB.Driver.GridFS.GridFSFileNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
