using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories.Abstractions;
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;
using WebApplication.Services;


namespace WebApplication.Controllers
{
    /// <summary>
    /// список Документов
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DocController: ControllerBase
    {
        private readonly DocRepository _docRepository;
        private readonly DocumentGateway _documentGateway;

        public DocController(DocRepository docRepository, DocumentGateway documentGateway)
        { 
            _docRepository = docRepository;
            _documentGateway = documentGateway;
        }

        /// <summary>
        /// Загружает файл в микросервис и создаёт запись Doc в Postgres.
        /// </summary>
        [HttpPost("upload")]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<ActionResult<DocResponse>> Upload([FromForm] CreateDocRequest request, CancellationToken ct)
        {
            if (request.File is null || request.File.Length == 0) return BadRequest("File is required");


            var (docId, checksum) = await _documentGateway.UploadAsync(request.File, request.Owner, request.Description, ct);


            var entity = new Doc
            {
                DocId = docId,
                FileName = request.File.FileName,
                CommentDoc = request.CommentDoc,
                IdDocType = request.IdDocType,
                IdEvent = request.IdEvent,
                IdCompetition = request.IdCompetition
            };


            await _docRepository.AddAsync(entity);

            return NoContent();
        }

        /// <summary>
        /// Возвращает Doc по Id (Postgres запись).
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DocResponse>> Get(int id)
        {
            var doc = await _docRepository.GetByIdAsync(id);
            if (doc == null) return NotFound();
            return new DocResponse
            {
                Id = doc.Id,
                FileName = doc.FileName,
                CommentDoc = doc.CommentDoc,
                IdDocType = doc.IdDocType,
                IdEvent = doc.IdEvent,
                IdCompetition = doc.IdCompetition
            };
        }

        /// <summary>
        /// Скачивание файла из микросервиса
        /// </summary>
        [HttpGet("{id:int}/download")]
        public async Task<IActionResult> Download(int id, CancellationToken ct)
        {
            var doc = await _docRepository.GetByIdAsync(id);
            if (doc == null) return NotFound();


            var stream = await _documentGateway.DownloadAsync(doc.DocId, ct);
            var fileName = doc.FileName ?? doc.DocId;
            return File(stream, "application/octet-stream", fileName);
        }

        /// <summary>
        /// Удаление
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var doc = await _docRepository.GetByIdAsync(id);
            if (doc == null) return NotFound();


            var ok = await _documentGateway.DeleteAsync(doc.DocId, ct);
            if (!ok) return NotFound("Remote document not found");


            await _docRepository.DeleteAsync(doc);
            return NoContent();
        }

        /// <summary>
        /// Список документов по мероприятию.
        /// </summary>
        [HttpGet("by-event")]
        public async Task<ActionResult<IEnumerable<DocResponse>>> EventDocumentsList([FromQuery] int eventId)
        {
            var docs = (await _docRepository.GetDocsByEventId(eventId)).Select(d => new DocResponse
            {
                Id = d.Id,
                FileName = d.FileName,
                CommentDoc = d.CommentDoc,
                IdDocType = d.IdDocType,
                IdEvent = d.IdEvent,
                IdCompetition = d.IdCompetition
            })
            .ToList();

            return docs;
        }

        /// <summary>
        /// Список документов по состязанию.
        /// </summary>
        [HttpGet("by-competition")]
        public async Task<ActionResult<IEnumerable<DocResponse>>> CompetitionDocumentsList([FromQuery] int competitionId)
        {
            var docs = (await _docRepository.GetDocsByCompetitionId(competitionId)).Select(d => new DocResponse
            {
                Id = d.Id,
                FileName = d.FileName,
                CommentDoc = d.CommentDoc,
                IdDocType = d.IdDocType,
                IdEvent = d.IdEvent,
                IdCompetition = d.IdCompetition
            })
            .ToList();

            return docs;
        }
    }
}
