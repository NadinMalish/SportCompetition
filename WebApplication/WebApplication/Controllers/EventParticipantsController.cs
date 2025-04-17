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
        private readonly RoleRepository _roleRepository;
        private readonly StatusRepository _statusRepository;

        public EventParticipantsController(EventParticipantRepository repository, RoleRepository roleRepository, StatusRepository statusRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _statusRepository = statusRepository;
        }

        /// <summary>
        /// Получить данные всех заявках
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<EventParticipant>>> GetEventParticipantsAsync()
        {
            var eventParticipants = await _repository.GetParticipantAsync(true);
            var eventParticipantModelList = eventParticipants.Select(ep => new EventParticipantResponse
            {
                Id = ep.Id,
                Comment = ep.Comment,
                DateTime = ep.DateTime,
                Status = new ApplicationStatusResponse()
                {
                    Id = ep.Status.Id,
                    Name = ep.Status.Name
                },
                Role = new RoleResponse()
                {
                    Id = ep.Role.Id,
                    Name = ep.Role.Name
                }
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
                Comment = eventParticipant.Comment,
                DateTime = eventParticipant.DateTime,
                Role = new RoleResponse()
                {
                    Id = eventParticipant.Role.Id,
                    Name = eventParticipant.Role.Name
                },
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
        public async Task<IActionResult> CreateEventParticipantAsync(CreateOrEditEventParticipant createOrEditEventParticipant)
        {
            if (!await _roleRepository.IsRoleExistsByIdAsync(createOrEditEventParticipant.RoleId)) 
                return BadRequest("Роль не найдена");
            if (!await _statusRepository.IsStatusExistsByIdAsync(createOrEditEventParticipant.StatusId))
                return BadRequest("Статус не найден");

            //TODO: ограничение на длину комментария?

            EventParticipant eventParticipant = new EventParticipant()
            {
                Comment = createOrEditEventParticipant.Comment,
                DateTime = createOrEditEventParticipant.DateTime,
                Role = await _roleRepository.GetByIdAsync(createOrEditEventParticipant.RoleId),
                Status = await _statusRepository.GetByIdAsync(createOrEditEventParticipant.StatusId)
            };

            await _repository.AddEventParticipantAsync(eventParticipant);
            return Created();
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

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
