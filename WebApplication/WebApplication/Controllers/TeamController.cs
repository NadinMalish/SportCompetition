using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using WebApplication.DataAccess.Repositories;
using Services.Repositories.Abstractions;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Команды мероприятия
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]

    public class TeamController : Controller
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<Competition> _competitionRepository;
        private readonly IRepository<Potent> _potentRepository;

        public TeamController(
            IRepository<Team> teamRepository,
            IRepository<Competition> competitionRepository,
            IRepository<Potent> potentRepository)
        {
            _teamRepository = teamRepository;
            _competitionRepository = competitionRepository;
            _potentRepository = potentRepository;
        }

        /// <summary>
        /// Получение полного списка команд
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<TeamResponse>> GetTeamsAsync()
        {
            var teamSet = await _teamRepository.GetAllAsync();
            var result = teamSet.Select(q => new TeamResponse
            {
                Id = q.Id,
                Name = q.Name,
                RegistryDate = q.RegistryDate,
                IsApproved = q.IsApproved,
                RejectNote = q.RejectNote,
                IsDeleted = q.IsDeleted
            }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Получить данные о команде
        /// </summary>
        /// <param name="id">Id команды</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamResponse>> GetTeamAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team == null)
                return NotFound($"Team with id={id} does not exists.");

            var teamModel = new TeamResponse()
            {
                Id = team.Id,
                Name = team.Name,
                RegistryDate = team.RegistryDate,
                IsApproved = team.IsApproved,
                RejectNote = team.RejectNote,
                IsDeleted = team.IsDeleted
            };

            return teamModel;
        }

        /// <summary>
        /// Создание новой команды мероприятия капитаном
        /// </summary>
        /// <param name="request">Атрибуты новой команды</param>
        [HttpPost]
        public async Task<IActionResult> CreateTeamByCaptainAsync([FromBody]CreateTeamByCaptainRequest request)
        {
            //Получаем состязание
            var competition = await _competitionRepository.GetByIdAsync(request.CompetitionId);
            if (competition == null)
                return NotFound($"Competition with id={request.CompetitionId} does not exists.");
            if (competition.CompetitionType == CompetitionTypes.Single)
                return BadRequest($"Competition with id={request.CompetitionId} only for single players.");
            //Получаем того, кто создал
            var editor = await _potentRepository.GetByIdAsync(request.EditorId);
            if (editor == null)
                return NotFound($"Potent with id={request.EditorId} does not exists.");

            var team = new Team()
            {
                Name = request.Name,
                RegistryDate = DateTime.Now,
                CompetitionOfEvent = competition,
                Captain = editor,
                CaptainId = request.EditorId
            };

            await _teamRepository.AddAsync(team);

            return Ok(team.Id);
        }

        /// <summary>
        /// Уничтожение команды
        /// </summary>
        /// <param name="id">Id команды</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTeam(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null)
                return NotFound($"Team with id={id} does not exists.");

            await _teamRepository.DeleteAsync(id);

            return Ok();
        }

    }
}
