using Microsoft.AspNetCore.Mvc;
using SportCompetition.Domain.Entities;
using SportCompetition.Infrastructure;
using SportCompetition.WebApi.Models;

namespace SportCompetition.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventInfoController : Controller
    {
        private readonly IRepository<EventInfo> _eventInfoRepository;

        public EventInfoController(IRepository<EventInfo> eventInfoRepository)
        {
            _eventInfoRepository = eventInfoRepository;
        }

        /// <summary>
        /// Получение полного списка мероприятий
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<EventInfoResponse>> GetEventsAsync()
        {
            var eventInfoSet = await _eventInfoRepository.GetAllAsync();
            var result = eventInfoSet.Select(q => new EventInfoResponse
            {
                Id = q.Id,
                Name = q.Name,
                Feedback = q.Feedback,

                BeginDate = q.BeginDate,
                EndDate = q.EndDate,
                StartRegistrationDate = q.StartRegistrationDate,
                FinishRegistrationDate = q.FinishRegistrationDate,
                StartActualControlDate = q.StartActualControlDate,
                FinishActualControlDate = q.FinishActualControlDate,

                IsCompleted = q.IsCompleted,
                RegistryDate = q.RegistryDate,

                IsDeleted = q.IsDeleted
            }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Получить данные о мероприятии
        /// </summary>
        /// <param name="id">Id мероприятия</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<EventInfoResponse>> GetEventAsync(int id)
        {
            var eventInfo = await _eventInfoRepository.GetByIdAsync(id);

            if (eventInfo == null)
                return NotFound($"Event with id={id} does not exists.");

            var eventInfoModel = new EventInfoResponse()
            {
                Id = eventInfo.Id,
                Name = eventInfo.Name,
                Feedback = eventInfo.Feedback,

                BeginDate = eventInfo.BeginDate,
                EndDate = eventInfo.EndDate,
                StartRegistrationDate = eventInfo.StartRegistrationDate,
                FinishRegistrationDate = eventInfo.FinishRegistrationDate,
                StartActualControlDate = eventInfo.StartActualControlDate,
                FinishActualControlDate = eventInfo.FinishActualControlDate,

                IsCompleted = eventInfo.IsCompleted,
                RegistryDate = eventInfo.RegistryDate,

                IsDeleted = eventInfo.IsDeleted
            };

            return eventInfoModel;
        }

        /// <summary>
        /// Создание нового мероприятия организатором
        /// </summary>
        /// <param name="request">Атрибуты нового мероприятия</param>
        [HttpPost]
        public async Task<IActionResult> CreateEventInfoByOrganizerAsync([FromBody] CreateOrEditEventByOrganizerRequest request)
        {
            var eventInfo = new EventInfo()
            {
                Name = request.Name,
                Feedback = request.Feedback,

                BeginDate = request.BeginDate,
                EndDate = request.EndDate,
                StartRegistrationDate = request.StartRegistrationDate,
                FinishRegistrationDate = request.FinishRegistrationDate,
                StartActualControlDate = request.StartActualControlDate,
                FinishActualControlDate = request.FinishActualControlDate,

                IsCompleted = false,
                RegistryDate = DateTime.Now,

                IsDeleted = false
            };

            await _eventInfoRepository.AddAsync(eventInfo);

            return Ok(eventInfo.Id);
        }

        /// <summary>
        /// Обновление данных о мероприятии организатором
        /// </summary>
        /// <param name="id">Id мероприятия</param>
        /// <param name="request">Новые атрибуты мероприятия</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEventInfoByOrganizerAsync(int id, [FromBody] CreateOrEditEventByOrganizerRequest request)
        {
            var eventInfo = await _eventInfoRepository.GetByIdAsync(id);
            if (eventInfo == null)
                return NotFound($"Event with id={id} does not exists.");

            eventInfo.Name = request.Name;
            eventInfo.Feedback = request.Feedback;

            eventInfo.BeginDate = request.BeginDate;
            eventInfo.EndDate = request.EndDate;
            eventInfo.StartRegistrationDate = request.StartRegistrationDate;
            eventInfo.FinishRegistrationDate = request.FinishRegistrationDate;
            eventInfo.StartActualControlDate = request.StartActualControlDate;
            eventInfo.FinishActualControlDate = request.FinishActualControlDate;

            eventInfo.IsCompleted = false;
            eventInfo.RegistryDate = DateTime.Now;

            eventInfo.IsDeleted = false;

            await _eventInfoRepository.UpdateAsync(eventInfo);

            return Ok();
        }

        /// <summary>
        /// Уничтожение мероприятия
        /// </summary>
        /// <param name="id">Id мероприятия</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEventInfo(int id)
        {
            var eventInfo = await _eventInfoRepository.GetByIdAsync(id);
            if (eventInfo == null)
                return NotFound($"Event with id={id} does not exists.");

            await _eventInfoRepository.DeleteAsync(eventInfo);

            return Ok();
        }

    }
}