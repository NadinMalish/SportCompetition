using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories.Abstractions;
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;


namespace WebApplication.Controllers
{
    /// <summary>
    /// справочник Категории Документов
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DocTypeController : ControllerBase
    {
        //private readonly DocTypeRepository _doctypeRepository;

        //public DocTypeController(DocTypeRepository doctypeRepository)
        //{
        //    _doctypeRepository = doctypeRepository;
        //}

        private readonly IRepository<DocType> _doctypeRepository;

        public DocTypeController(IRepository<DocType> doctypeRepository)
        {
            _doctypeRepository = doctypeRepository;
        }


        /// <summary>
        /// Получение данных из справочника Категории Документов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<DocTypeShortResponse>>> GetSpisDocType()
        {
            try
            {
                var _doctype = (await _doctypeRepository.GetAllAsync()).ToList();
                var doctypeModelList = _doctype.Select(x => new DocTypeShortResponse()
                {
                    Id = x.Id,
                    Name_doc_type = x.NameDocType,
                    Comment_doc = x.CommentDoc
                }).ToList();

                return Ok(doctypeModelList);
                //return Ok(_doctype);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Получить данные из справочника Категори Документа по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DocType>> GetDocTypeById(int id)
        {
            DocType _doctype = await _doctypeRepository.GetByIdAsync(id);
            return Ok(_doctype);
        }


        /// <summary>
        /// Удаление записть из справочника Категори Документа по Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteDocType(int id)
        {
            bool fl = await _doctypeRepository.DeleteAsync(id);
            if (!fl) return BadRequest("Not Found");

            return Ok();
        }


        /// <summary>
        /// Добавить запись в справочник Категори Документа
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDocType(DocTypeShortResponse request)
        {
            try
            {
                DocType item = new DocType()
                {
                    NameDocType = request.Name_doc_type,
                    CommentDoc = request.Comment_doc
                };

                var _doctype = await _doctypeRepository.AddAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Обновить запись в справочник Категори Документа
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDocType(int id, DocTypeShortResponse request)
        {
            try
            {
                DocType doctype = await _doctypeRepository.GetByIdAsync(id);
                if (doctype == null) return NotFound();

                doctype.NameDocType = request.Name_doc_type;
                doctype.CommentDoc = request.Comment_doc;
                await _doctypeRepository.UpdateAsync(doctype);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
