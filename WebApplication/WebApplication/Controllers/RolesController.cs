using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;

        public RolesController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Получить все роли
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<RoleResponse>>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            var result = roles.Select(r => new RoleResponse
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Получить роль по Id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleResponse>> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role is null)
                return NotFound();

            return Ok(new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
            });
        }

        /// <summary>
        /// Создать новую роль
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateOrEditRoleRequest request)
        {
            if (await _roleRepository.IsRoleExistsByNameAsync(request.Name))
                return BadRequest($"Роль с названием \"{request.Name}\" уже существует");

            var role = new Role
            {
                Name = request.Name
            };

            await _roleRepository.AddAsync(role);

            return Created();
        }

        /// <summary>
        /// Удалить роль
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role is null)
                return NotFound();

            await _roleRepository.DeleteAsync(id);
            await _roleRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
