using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using WebApplication.DataAccess.Repositories;
using Services.Repositories.Abstractions;
using WebApplication.Models;
using System.Xml.Linq;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Состязания мероприятия
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompetitionController : Controller
    {
        private readonly IRepository<Competition> _competitionRepository;
        private readonly IRepository<EventInfo> _eventRepository;
        private readonly IRepository<Potent> _potentRepository;

        public CompetitionController(
            IRepository<Competition> competitionRepository,
            IRepository<EventInfo> eventRepository,
            IRepository<Potent> potentRepository
            )
        {
            _competitionRepository = competitionRepository;
            _eventRepository = eventRepository;
            _potentRepository = potentRepository;
        }

        /// <summary>
        /// Получение полного списка состязаний
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<CompetitionResponse>> GetCompetitionsAsync()
        {
            var competitionSet = await _competitionRepository.GetAllAsync();
            var result = competitionSet.Select(q => new CompetitionResponse
            {
                Id = q.Id,
                Name = q.Name,
                CompetitionType = q.CompetitionType,
                BeginDate = q.BeginDate,
                EndDate = q.EndDate,
<<<<<<< HEAD
                EventInfo = q.Event,
=======
                MinComandSize = q.MinComandSize,
                MaxComandSize = q.MaxComandSize,
                Event = q.Event,
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
                IsCompleted = q.IsCompleted,
                RegistryDate = q.RegistryDate,
                IsDeleted = q.IsDeleted
            }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Получить данные о состязании
        /// </summary>
        /// <param name="id">Id состязания</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CompetitionResponse>> GetCompetitionAsync(int id)
        {
            var competition = await _competitionRepository.GetByIdAsync(id);
            if (competition == null)
                return NotFound($"Competition with id={id} does not exists.");

            var competitionModel = new CompetitionResponse()
            {
                Id = competition.Id,
                Name = competition.Name,
                CompetitionType = competition.CompetitionType,
                BeginDate = competition.BeginDate,
                EndDate = competition.EndDate,
<<<<<<< HEAD
                EventInfo = competition.Event,
=======
                MinComandSize = competition.MinComandSize,
                MaxComandSize = competition.MaxComandSize,
                Event = competition.Event,
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
                IsCompleted = competition.IsCompleted,
                RegistryDate = competition.RegistryDate,
                IsDeleted = competition.IsDeleted
            };

            return competitionModel;
        }

        /// <summary>
        /// Создание нового состязания мероприятия организатором
        /// </summary>
        /// <param name="request">Атрибуты нового состязания</param>
        [HttpPost]
        public async Task<IActionResult> CreateCompetitionByOrganizerAsync([FromBody] CreateCompetitionByOrganizerRequest request)
        {
            //Получаем мероприятие
            var eventIfo = await _eventRepository.GetByIdAsync(request.EventId);
            if (eventIfo == null)
                return NotFound($"Event with id={request.EventId} does not exists.");
<<<<<<< HEAD
=======
            //Получаем того, кто создал
            var editor = await _potentRepository.GetByIdAsync(request.EditorId);
            if (editor == null)
                return NotFound($"Potent with id={request.EditorId} does not exists.");
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3

            var competition = new Competition()
            {
                Name = request.Name,
                CompetitionType = request.CompetitionType,
                BeginDate = request.BeginDate,
                EndDate = request.EndDate,
<<<<<<< HEAD
                IsCompleted = false,
                RegistryDate = DateTime.Now,
                Event = eventIfo,
                EventId = request.EventId
=======
                MinComandSize = request.MinComandSize,
                MaxComandSize = request.MaxComandSize,
                IsCompleted = false,
                RegistryDate = DateTime.Now,
                Event = eventIfo,
                EventId = request.EventId,
                Editor = editor,
                EditorId = request.EditorId
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            };

            await _competitionRepository.AddAsync(competition);

            return Ok(competition.Id);
        }

        /// <summary>
        /// Обновление данных о состязании мероприятия организатором
        /// </summary>
        /// <param name="id">Id состязания</param>
        /// <param name="request">Новые атрибуты состязания</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCompetitionByOrganizerAsync(int id, [FromBody] EditCompetitionByOrganizerRequest request)
        {
            var competition = await _competitionRepository.GetByIdAsync(id);
            if (competition == null)
                return NotFound($"Competition with id={id} does not exists.");

            competition.Name = request.Name;
            competition.CompetitionType = request.CompetitionType;
            competition.BeginDate = request.BeginDate;
            competition.EndDate = request.EndDate;
<<<<<<< HEAD
=======
            competition.MinComandSize = request.MinComandSize;
            competition.MaxComandSize = request.MaxComandSize;
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            competition.IsCompleted = false;
            competition.RegistryDate = DateTime.Now;

            await _competitionRepository.UpdateAsync(competition);

            return Ok();
        }

        /// <summary>
        /// Уничтожение состязания
        /// </summary>
        /// <param name="id">Id состязания</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCompetition(int id)
        {
            var competition = await _competitionRepository.GetByIdAsync(id);
            if (competition == null)
                return NotFound($"Competition with id={id} does not exists.");

            await _competitionRepository.DeleteAsync(id);

            return Ok();
        }
    }
}
