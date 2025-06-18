using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventParticipantsController : ControllerBase
    {
        private readonly EventParticipantRepository _repository;
        private readonly StatusRepository _statusRepository;

        public EventParticipantsController(EventParticipantRepository repository, StatusRepository statusRepository)
        {
            _repository = repository;
            _statusRepository = statusRepository;
        }

        /// <summary>
        /// Получить данные всех заявках
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<EventParticipant>>> GetAllEventParticipantsAsync()
        {
            var eventParticipants = await _repository.GetParticipantAsync(true);
            var eventParticipantModelList = eventParticipants.Select(ep => new EventParticipantResponse
            {
                Id = ep.Id,
                DateTime = ep.DateTime,
                Status = new ApplicationStatusResponse()
                {
                    Id = ep.Status.Id,
                    Name = ep.Status.Name
                },
            }).ToList();

            return Ok(eventParticipantModelList);
        }

        /// <summary>
        /// Получить данные заявки/участника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<EventParticipant>>> GetEventParticipantByIdAsync(int id)
        {
            EventParticipant? eventParticipant = await _repository.GetEventParticipantById(id, true);
            
            if (eventParticipant is null)
                return NotFound();

            EventParticipantResponse response = new EventParticipantResponse()
            {
                Id = eventParticipant.Id,
                DateTime = eventParticipant.DateTime,
                Status = new ApplicationStatusResponse()
                {
                    Id = eventParticipant.Status.Id,
                    Name = eventParticipant.Status.Name
                }
            };

            return Ok(response);
        }

        /// <summary>
        /// Создать заявку
        /// </summary>
        /// <param name="createOrEditEventParticipant"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEventParticipantAsync(CreateEventParticipantRequest createEventParticipantRequest)
        {
            EventParticipant eventParticipant = new EventParticipant()
            {
                DateTime = DateTime.Now,
                Status = await _statusRepository.GetByIdAsync(createEventParticipantRequest.StatusId)
            };

            await _repository.AddEventParticipantAsync(eventParticipant);
            return Created();
        }

        /// <summary>
        /// Редактировать заявку
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditEventParticipantAsync(int id, [FromBody] EditEventParticipantRequest request)
        {
            EventParticipant? eventParticipant = await _repository.GetEventParticipantById(id);
            if (eventParticipant is null)
                return NotFound();
            if (!await _statusRepository.IsStatusExistsByIdAsync(request.StatusId))
                return BadRequest();
            eventParticipant.ApplicationStatusId = request.StatusId;
            await _repository.UpdateAsync(eventParticipant);

            return NoContent();
        }

        /// <summary>
        /// Удалить данные о заявке
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEventParticipantAsync(int id) 
        {
            EventParticipant eventParticipant = await _repository.GetByIdAsync(id);
            if (eventParticipant is null)
                return NotFound();

            eventParticipant.IsDeleted = true;
            await _repository.UpdateAsync(eventParticipant);

            return NoContent();
        }
    }
}
