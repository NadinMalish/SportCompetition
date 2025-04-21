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
        public async Task<ActionResult<List<Doc>>> GetSpisDoc()
        {
            try
            {
                var _doc = (await _docRepository.GetSpisDoc()).ToList();
                return Ok(_doc);
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
                    name_doc = request.Name_doc,
                    file_name = request.File_name,
                    comment_doc = request.Comment_doc,
                    id_doc_type = (await _doctypeRepository.FlById(request.Id_doc_type)) ? request.Id_doc_type : null,
                    id_event = (await _eventRepository.FlById(request.Id_event)) ? request.Id_event : null,
                    id_competition = (await _competitionRepository.FlById(request.Id_competition)) ? request.Id_competition : null,
                    docum = null
                };

                await _docRepository.AddDoc(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


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

                doc.name_doc = request.Name_doc;
                doc.file_name = request.File_name;
                doc.comment_doc = request.Comment_doc;
                doc.id_doc_type = (request.Id_doc_type==0) ? null : (await _doctypeRepository.FlById(request.Id_doc_type)) ? request.Id_doc_type : doc.id_doc_type;
                doc.id_event = (request.Id_event == 0) ? null : (await _eventRepository.FlById(request.Id_event)) ? request.Id_event : doc.id_event;
                doc.id_competition = (request.Id_competition == 0) ? null : (await _competitionRepository.FlById(request.Id_competition)) ? request.Id_competition : doc.id_competition;
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


        [HttpPost]
        [Route("upload")]
        public void PostFile(IFormFile uploadedFile)
        {
            //TODO: Save file
        }



    }
}
