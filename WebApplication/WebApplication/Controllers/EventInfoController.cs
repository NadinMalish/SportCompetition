using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using WebApplication.DataAccess.Repositories;
using Services.Repositories.Abstractions;
using WebApplication.Models;
using System.Linq.Expressions;
using System;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventInfoController : Controller
    {
        private readonly IRepository<EventInfo> _eventInfoRepository;
        private readonly IRepository<Potent> _potentRepository;

        public EventInfoController(
            IRepository<EventInfo> eventInfoRepository,
            IRepository<Potent> potentRepository
            )
        {
            _eventInfoRepository = eventInfoRepository;
            _potentRepository = potentRepository;
        }

        /// <summary>
        /// Получение полного списка мероприятий
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult>> GetEventsAsync([FromQuery] int page = 1,
            [FromQuery] int pageSize = 20, [FromQuery] string search = "", [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null, [FromQuery] bool? isOpen = null)
        {
            if (page < 1 || pageSize < 1)
                throw new Exception("Номер страницы и размер страницы должны быть больше нуля.");

            Expression<Func<EventInfo, bool>> expression = e =>
                (string.IsNullOrEmpty(search) || e.Name.ToLower().Contains(search.ToLower())) &&
                (!startDate.HasValue || e.BeginDate >= startDate.Value) &&
                (!endDate.HasValue || e.EndDate <= endDate.Value);

            var count = await _eventInfoRepository.CountAsync(expression);

            var eventInfoSet = await _eventInfoRepository.GetAllAsync(pageSize, pageSize * (page - 1), true, expression);
            
            var result = new PagedResult()
            {
                Events = eventInfoSet.Select(q => new EventInfoResponse
                {
                    Id = q.Id,
                    Name = q.Name,

                    BeginDate = q.BeginDate,
                    EndDate = q.EndDate,
                    RegistrationDate = q.RegistrationDate,

                    IsCompleted = q.IsCompleted,
                    RegistryDate = q.RegistryDate
                }).ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = count
            };

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

                BeginDate = eventInfo.BeginDate,
                EndDate = eventInfo.EndDate,
                RegistrationDate = eventInfo.RegistrationDate,

                IsCompleted = eventInfo.IsCompleted,
                RegistryDate = eventInfo.RegistryDate
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
            //Получаем того, кто создал
            var organizer = await _potentRepository.GetByIdAsync(request.OrganizerId);
            if (organizer == null)
                return NotFound($"Potent with id={request.OrganizerId} does not exists.");

            var eventInfo = new EventInfo()
            {
                Name = request.Name,

                BeginDate = request.BeginDate.ToUniversalTime(),
                EndDate = request.EndDate.ToUniversalTime(),
                RegistrationDate = request.RegistrationDate.ToUniversalTime(),

                IsCompleted = false,
                RegistryDate = DateTime.Now,

                Organizer = organizer,
                OrganizerId = request.OrganizerId
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

            eventInfo.BeginDate = request.BeginDate;
            eventInfo.EndDate = request.EndDate;
            eventInfo.RegistrationDate = request.RegistrationDate;

            eventInfo.IsCompleted = false;
            eventInfo.RegistryDate = DateTime.Now;

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

            await _eventInfoRepository.DeleteAsync(id);

            return Ok();
        }

    }
}