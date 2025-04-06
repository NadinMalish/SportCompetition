using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportCompetition.Domain.Entities;
using SportCompetition.Infrastructure;
using SportCompetition.WebApi.Models;
using System.Xml.Linq;

namespace SportCompetition.WebApi.Controllers
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

        public CompetitionController(IRepository<Competition> competitionRepository, IRepository<EventInfo> eventRepository)
        {
            _competitionRepository = competitionRepository;
            _eventRepository = eventRepository;
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
                MinComandSize = q.MinComandSize,
                MaxComandSize = q.MaxComandSize,
                Event = q.Event,
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
                MinComandSize = competition.MinComandSize,
                MaxComandSize = competition.MaxComandSize,
                Event = competition.Event,
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

            var competition = new Competition()
            {
                Name = request.Name,
                CompetitionType = request.CompetitionType,
                BeginDate = request.BeginDate,
                EndDate = request.EndDate,
                MinComandSize = request.MinComandSize,
                MaxComandSize = request.MaxComandSize,
                IsCompleted = false,
                RegistryDate = DateTime.Now,
                Event = eventIfo
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
            competition.MinComandSize = request.MinComandSize;
            competition.MaxComandSize = request.MaxComandSize;
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

            await _competitionRepository.DeleteAsync(competition);

            return Ok();
        }
    }
}
