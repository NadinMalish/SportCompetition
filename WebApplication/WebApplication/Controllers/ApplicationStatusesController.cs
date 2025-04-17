using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApplicationStatusesController : ControllerBase
    {
        private readonly StatusRepository _statusRepository;

        public ApplicationStatusesController(StatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        /// <summary>
        /// Получить все статусы
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ApplicationStatusResponse>>> GetAllStatusesAsync()
        {
            var statuses = await _statusRepository.GetAllAsync();
            var result = statuses.Select(s => new ApplicationStatusResponse
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Получить статус по Id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApplicationStatusResponse>> GetStatusByIdAsync(int id)
        {
            var status = await _statusRepository.GetByIdAsync(id);
            if (status is null)
                return NotFound();

            return Ok(new ApplicationStatusResponse
            {
                Id = status.Id,
                Name = status.Name
            });
        }

        /// <summary>
        /// Создать новый статус
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateStatusAsync([FromBody] CreateOrEditApplicationStatusRequest request)
        {

            if (await _statusRepository.IsStatusExistsByNameAsync(request.Name))
                return BadRequest($"Заявка с названием \"{request.Name}\" уже существует.");

            var status = new ApplicationStatus
            {
                Name = request.Name
            };

            await _statusRepository.AddAsync(status);

            return Created();
        }

        /// <summary>
        /// Удалить статус
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStatusAsync(int id)
        {
            var status = await _statusRepository.GetByIdAsync(id);
            if (status is null)
                return NotFound();

            await _statusRepository.DeleteAsync(id);

            return NoContent();
        }
    }

}
