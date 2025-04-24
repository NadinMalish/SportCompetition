using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories.Abstractions;
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;


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
        private readonly IRepository<DocType> _doctypeRepository;
        private readonly IRepository<EventInfo> _eventRepository;
        private readonly IRepository<Competition> _competitionRepository;

        public DocController(DocRepository docRepository, IRepository<DocType> doctypeRepository, IRepository<EventInfo> eventRepository, IRepository<Competition> competitionRepository)
        { 
            _docRepository = docRepository;
            _doctypeRepository = doctypeRepository;
            _eventRepository = eventRepository;
            _competitionRepository = competitionRepository;
        }


        /// <summary>
        /// Получение данных из списка Документов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<DocShortResponse>>> GetSpisDoc()
        {
            try
            {
                var _doc = (await _docRepository.GetSpisDoc()).ToList();
                var docModelList = _doc.Select(x => new DocShortResponse()
                {
                    Id = x.Id,
                    Name_doc = x.NameDoc,
                    File_name = x.FileName,
                    Comment_doc = x.CommentDoc,
                    Id_doc_type = x.IdDocType,
                    Id_competition = x.IdCompetition,
                    Id_event = x.IdEvent
                }).ToList();

                return Ok(docModelList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Добавить запись в список Документов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDoc(DocShortResponse request)
        {
            try
            {
                Doc item = new Doc()
                {
                    NameDoc = request.Name_doc.Trim(),
                    FileName = null,
                    CommentDoc = request.Comment_doc,
                    IdDocType = (await _doctypeRepository.FlById(request.Id_doc_type)) ? request.Id_doc_type : null,
                    IdEvent = (await _eventRepository.FlById(request.Id_event)) ? request.Id_event : null,
                    IdCompetition = (await _competitionRepository.FlById(request.Id_competition)) ? request.Id_competition : null,
                    Docum = null
                };

                await _docRepository.AddDoc(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        ///// <summary>
        ///// Добавить запись в список Документов
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("upload")]
        //public async Task<IActionResult> CreateDoc(DocShortResponse request, IFormFile docFile)
        //{
        //    try
        //    {
        //        if (docFile.Length==0) return BadRequest("Not File Docum");

        //        byte[] fileData = null;
        //        using (var binaryReader = new BinaryReader(docFile.OpenReadStream()))
        //        {
        //            fileData = binaryReader.ReadBytes((int)docFile.Length);
        //        }
        //        Doc item = new Doc()
        //        {
        //            name_doc = (request.Name_doc.Trim().Length > 0) ? request.Name_doc : docFile.FileName,
        //            file_name = docFile.FileName,
        //            comment_doc = request.Comment_doc,
        //            id_doc_type = (await _doctypeRepository.FlById(request.Id_doc_type)) ? request.Id_doc_type : null,
        //            id_event = (await _eventRepository.FlById(request.Id_event)) ? request.Id_event : null,
        //            id_competition = (await _competitionRepository.FlById(request.Id_competition)) ? request.Id_competition : null,
        //            docum = fileData
        //        };

        //        await _docRepository.AddDoc(item);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        /// <summary>
        /// Получить данные из списка Документов по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Doc>> GetDocById(int id)
        {
            var _doc = await _docRepository.GetByIdAsync(id);
            return Ok(_doc);
        }


        /// <summary>
        /// Редактировать запись в списке Документов
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoc(int id, DocShortResponse request)
        {
            try
            {
                Doc doc = await _docRepository.GetByIdAsync(id);
                if (doc == null) return NotFound();

                doc.NameDoc = request.Name_doc;
                doc.CommentDoc = request.Comment_doc;
                doc.IdDocType = (request.Id_doc_type==0) ? null : (await _doctypeRepository.FlById(request.Id_doc_type)) ? request.Id_doc_type : doc.IdDocType;
                doc.IdEvent = (request.Id_event == 0) ? null : (await _eventRepository.FlById(request.Id_event)) ? request.Id_event : doc.IdEvent;
                doc.IdCompetition = (request.Id_competition == 0) ? null : (await _competitionRepository.FlById(request.Id_competition)) ? request.Id_competition : doc.IdCompetition;
                await _docRepository.Update(doc);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Ометить записть удаленной в списке Документов по Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> SetDeleteDoc(int id)
        {
            bool fl = await _docRepository.SetDelDoclById(id);
            if (!fl) return BadRequest("Not Found");

            return Ok();
        }


        /// <summary>
        /// Загрузить файл Документа по Id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> PostFile(int id, IFormFile fileDoc)
        {
            //TODO: Save file
            try
            {
                if (fileDoc.Length == 0) return BadRequest("Not File Docum");

                Doc doc = await _docRepository.GetByIdAsync(id);
                if (doc == null) return NotFound();

                string fileName = fileDoc.FileName;
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(fileDoc.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)fileDoc.Length);
                }

                doc.FileName = fileName;
                doc.Docum = fileData;
                await _docRepository.Update(doc);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
