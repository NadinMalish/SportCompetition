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
<<<<<<< HEAD
        private readonly StatusRepository _statusRepository;
        private readonly CompetitionRepository _competitionRepository;

        public EventParticipantsController(EventParticipantRepository repository,
            StatusRepository statusRepository,
            CompetitionRepository competitionRepository)
        {
            _repository = repository;
            _statusRepository = statusRepository;
            _competitionRepository = competitionRepository;
=======
        private readonly RoleRepository _roleRepository;
        private readonly StatusRepository _statusRepository;

        public EventParticipantsController(EventParticipantRepository repository, RoleRepository roleRepository, StatusRepository statusRepository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _statusRepository = statusRepository;
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
=======
                Comment = ep.Comment,
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
                DateTime = ep.DateTime,
                Status = new ApplicationStatusResponse()
                {
                    Id = ep.Status.Id,
                    Name = ep.Status.Name
                },
<<<<<<< HEAD
=======
                Role = new RoleResponse()
                {
                    Id = ep.Role.Id,
                    Name = ep.Role.Name
                }
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
            var eventParticipant = await _repository.GetEventParticipantById(id, true);
=======
            EventParticipant? eventParticipant = await _repository.GetEventParticipantById(id, true);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            
            if (eventParticipant is null)
                return NotFound();

            EventParticipantResponse response = new EventParticipantResponse()
            {
                Id = eventParticipant.Id,
<<<<<<< HEAD
                DateTime = eventParticipant.DateTime,
=======
                Comment = eventParticipant.Comment,
                DateTime = eventParticipant.DateTime,
                Role = new RoleResponse()
                {
                    Id = eventParticipant.Role.Id,
                    Name = eventParticipant.Role.Name
                },
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
        /// <param name="createEventParticipantRequest"></param>
=======
        /// <param name="createOrEditEventParticipant"></param>
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEventParticipantAsync(CreateEventParticipantRequest createEventParticipantRequest)
        {
<<<<<<< HEAD
            var competition = await _competitionRepository.GetByIdAsync(createEventParticipantRequest.CompetitionId);
            if (competition is null)
                return NotFound();
=======
            if (!await _roleRepository.IsRoleExistsByIdAsync(createEventParticipantRequest.RoleId)) 
                return BadRequest("Роль не найдена");
            if (!await _statusRepository.IsStatusExistsByIdAsync(createEventParticipantRequest.StatusId))
                return BadRequest("Статус не найден");
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3

            EventParticipant eventParticipant = new EventParticipant()
            {
                DateTime = DateTime.Now,
<<<<<<< HEAD
                Status = await _statusRepository.GetByIdAsync(createEventParticipantRequest.StatusId),
                ParticipantCompetition = competition
=======
                Role = await _roleRepository.GetByIdAsync(createEventParticipantRequest.RoleId),
                Status = await _statusRepository.GetByIdAsync(createEventParticipantRequest.StatusId)
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
            var eventParticipant = await _repository.GetEventParticipantById(id);
=======
            EventParticipant? eventParticipant = await _repository.GetEventParticipantById(id);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            if (eventParticipant is null)
                return NotFound();
            if (!await _statusRepository.IsStatusExistsByIdAsync(request.StatusId))
                return BadRequest();
<<<<<<< HEAD
            eventParticipant.ApplicationStatusId = request.StatusId;
=======

            //TODO: ограничение на размер комментария

            eventParticipant.Comment = request.Comment;
            eventParticipant.StatusId = request.StatusId;
            eventParticipant.SetStatusId = request.SetStatusId;
            eventParticipant.IsActual = request.IsActual;
            eventParticipant.IsCaptainConfirmed = request.IsCaptainConfirmed;
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
            var eventParticipant = await _repository.GetByIdAsync(id);
=======
            EventParticipant eventParticipant = await _repository.GetByIdAsync(id);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            if (eventParticipant is null)
                return NotFound();

            eventParticipant.IsDeleted = true;
            await _repository.UpdateAsync(eventParticipant);

            return NoContent();
        }
    }
}
