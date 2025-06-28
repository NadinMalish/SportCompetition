using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories.Abstractions;
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;


namespace WebApplication.Controllers
{
    /// <summary>
<<<<<<< HEAD
    /// Справочник "Категории Документов"
=======
    /// справочник Категории Документов
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DocTypeController : ControllerBase
    {
        private readonly DocTypeRepository _doctypeRepository;
     
        public DocTypeController(DocTypeRepository doctypeRepository)
        {
            _doctypeRepository = doctypeRepository;
        }

        /// <summary>
<<<<<<< HEAD
        /// Получение данных из справочника "Категории Документов"
=======
        /// Получение данных из справочника Категории Документов
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
        /// Получить данные из справочника "Категории Документов" по Id
=======
        /// Получить данные из справочника Категори Документа по Id
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DocType>> GetDocTypeById(int id)
        {
            DocType _doctype = await _doctypeRepository.GetByIdAsync(id);
            return Ok(_doctype);
        }


        /// <summary>
<<<<<<< HEAD
        /// Удаление записть из справочника "Категории Документов" по Id
=======
        /// Удаление записть из справочника Категори Документа по Id
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
        /// Добавить запись в справочник "Категории Документов"
=======
        /// Добавить запись в справочник Категори Документа
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
