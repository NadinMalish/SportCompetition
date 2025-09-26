using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Security.Cryptography;

namespace FileServerHost.Services
{
    public class GridFsDocumentStore
    {
        private readonly GridFSBucket _bucket;

        public GridFsDocumentStore(GridFSBucket bucket)
        {
            _bucket = bucket;
        }

        public async Task<(ObjectId id, string sha256)> UploadAsync(IFormFile file, string? owner, string? description, List<string>? tags, CancellationToken ct)
        {
            if (file == null || file.Length == 0) throw new ArgumentException("File is required");


            var meta = new BsonDocument
            {
            {"owner", owner ?? string.Empty},
            {"description", description ?? string.Empty},
            {"tags", new BsonArray((tags ?? new List<string>()).Where(t => !string.IsNullOrWhiteSpace(t)))},
            {"contentType", file.ContentType ?? "application/octet-stream"},
            {"originalName", file.FileName ?? string.Empty},
            {"uploadedAt", DateTime.UtcNow},
            };


            ObjectId fileId;
            string sha256Hex;


            await using (var uploadStream = await _bucket.OpenUploadStreamAsync(file.FileName ?? "file", new GridFSUploadOptions { Metadata = meta }, ct))
            await using (var fileStream = file.OpenReadStream())
            using (var sha = SHA256.Create())
            {
                var buffer = new byte[64 * 1024];
                int read;
                while ((read = await fileStream.ReadAsync(buffer.AsMemory(0, buffer.Length), ct)) > 0)
                {
                    await uploadStream.WriteAsync(buffer.AsMemory(0, read), ct);
                    sha.TransformBlock(buffer, 0, read, null, 0);
                }
                sha.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
                sha256Hex = Convert.ToHexString(sha.Hash!);
                await uploadStream.FlushAsync(ct);
                fileId = uploadStream.Id;
            }


            // Patch checksum + size
            var filesColl = _bucket.Database.GetCollection<BsonDocument>($"{_bucket.Options.BucketName}.files");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", fileId);
            var update = Builders<BsonDocument>.Update
            .Set("metadata.sha256", sha256Hex)
            .Set("metadata.size", file.Length);
            await filesColl.UpdateOneAsync(filter, update, cancellationToken: ct);


            return (fileId, sha256Hex);
        }

        public async Task<(GridFSDownloadStream stream, string contentType, string fileName)?> DownloadAsync(ObjectId id, CancellationToken ct)
        {
            try
            {
                var stream = await _bucket.OpenDownloadStreamAsync(id, cancellationToken: ct);
                var info = stream.FileInfo;
                var contentType = info.Metadata?.GetValue("contentType", BsonNull.Value)?.AsString ?? "application/octet-stream";
                var fileName = info.Filename ?? id.ToString();
                return (stream, contentType, fileName);
            }
            catch (GridFSFileNotFoundException)
            {
                return null;
            }
        }

        public async Task<object?> GetInfoAsync(ObjectId id, CancellationToken ct)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Id, id);
            using var cursor = await _bucket.FindAsync(filter, cancellationToken: ct);
            var file = await cursor.FirstOrDefaultAsync(ct);
            if (file is null) return null;
            return new
            {
                id = file.Id.ToString(),
                filename = file.Filename,
                length = file.Length,
                chunkSize = file.ChunkSizeBytes,
                uploadDate = file.UploadDateTime,
                metadata = file.Metadata?.ToDictionary() ?? new Dictionary<string, object?>()
            };
        }

        public async Task<bool> UpdateMetadataAsync(ObjectId id, string? owner, string? description, List<string>? tags, CancellationToken ct)
        {
            var files = _bucket.Database.GetCollection<BsonDocument>($"{_bucket.Options.BucketName}.files");
            var updates = new List<UpdateDefinition<BsonDocument>>();
            if (owner is not null) updates.Add(Builders<BsonDocument>.Update.Set("metadata.owner", owner));
            if (description is not null) updates.Add(Builders<BsonDocument>.Update.Set("metadata.description", description));
            if (tags is not null) updates.Add(Builders<BsonDocument>.Update.Set("metadata.tags", new BsonArray(tags)));
            if (updates.Count == 0) return false;
            var result = await files.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", id), Builders<BsonDocument>.Update.Combine(updates), cancellationToken: ct);
            return result.MatchedCount > 0;
        }

        public async Task<IEnumerable<object>> ListAsync(string? owner, string? tag, int page, int pageSize, CancellationToken ct)
        {
            var builderF = Builders<GridFSFileInfo>.Filter;
            var filter = builderF.Empty;
            if (!string.IsNullOrWhiteSpace(owner))
                filter &= builderF.Eq("metadata.owner", owner);

            if (!string.IsNullOrWhiteSpace(tag))
                filter &= builderF.AnyEq("metadata.tags", tag); // tags — это массив строк


            var options = new GridFSFindOptions
            {
                Skip = (page - 1) * pageSize,
                Limit = pageSize,
                Sort = Builders<GridFSFileInfo>.Sort.Descending(x => x.UploadDateTime)
            };


            using var cursor = await _bucket.FindAsync(filter, options, ct);
            var list = await cursor.ToListAsync(ct);
            return list.Select(f => new
            {
                id = f.Id.ToString(),
                filename = f.Filename,
                length = f.Length,
                uploadDate = f.UploadDateTime,
                metadata = f.Metadata?.ToDictionary() ?? new Dictionary<string, object?>()
            });
        }

        public Task DeleteAsync(ObjectId id, CancellationToken ct) => _bucket.DeleteAsync(id, ct);
    }
}
