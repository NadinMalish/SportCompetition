using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories.Abstractions;
using System.Security.Cryptography;
using System.Text;
using WebApplication.DataAccess.Repositories;
using WebApplication.Models;


namespace WebApplication.Controllers
{
    /// <summary>
    /// список Зарегистрированных пользователей
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PotentController : ControllerBase
    {
        private readonly PotentRepository _potentRepository;

        public PotentController(PotentRepository potentRepository)
        {
            _potentRepository = potentRepository;
        }


        /// <summary>
        /// Получение актуального списка Зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PotentSpis>>> GetSpisPotent()
        {
            try
            {
                var _potent = (await _potentRepository.GetSpisPotent()).ToList();
                var potentModelList = _potent.Select(x => new PotentSpis()
                {
                    Id = x.Id,
                    FullName = $"{x.Lastname} {x.Firstname} {x.Surname}",
                    DateBirth = x.DateBirth.ToString(),
                    Gender = (x.Gender.ToLower() == "w") ? "жен." : "муж.",
                    Login = x.Login,
                    Email = x.Email,
                    DatReg = x.DatReg
                }).ToList();

                return Ok(potentModelList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Получить данные о Зарегистрированном пользователе по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Potent>> GetPotentById(int id)
        {
            var _potent = await _potentRepository.GetByIdAsync(id);
            return Ok(_potent);
        }

        /// <summary>
        /// Получить данные о Зарегистрированном пользователе по E-mail
        /// </summary>
        /// <returns></returns>
        [HttpGet("email")]
        public async Task<ActionResult<Potent>> GetEmployeeByEmail(string email)
        {
            var _potent = await _potentRepository.GetPotentByEmail(email);
            return Ok(_potent);
        }

        /// <summary>
        /// Получить данные о Зарегистрированном пользователе по Login_Passord
        /// </summary>
        /// <returns></returns>
        [HttpGet("{login},{pass}")]
        public async Task<ActionResult<Potent>> GetEmployeeByLogin(string login, string pass)
        {
            var _potent = await _potentRepository.GetPotentByLogin(login, HashPass(pass));
            return Ok(_potent);
        }


        /// <summary>
        /// Добавить запись в список Зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreatePotent(PotentShortResponse request)
        {
            try
            {
                Potent item = new Potent()
                {
                    Lastname = request.Lastname,
                    Firstname = request.Firstname,
                    Surname = request.Surname,
                    DateBirth = request.date_birth,
                    Email = request.Email,
                    Login = request.Login,
                    Pass = HashPass(request.Pass),
                    Gender = (request.gender.ToUpper() == "M") ? "m" : (request.gender.ToUpper() == "М") ? "m" : "w"
                };

                await _potentRepository.AddPotent(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }


        /// <summary>
        /// Редактировать запись в списке Зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePotent(int id, PotentShortResponse request)
        {
            try
            {
                Potent item = await _potentRepository.GetByIdAsync(id);
                if (item == null) return NotFound();

                item.Lastname = request.Lastname;
                item.Firstname = request.Firstname;
                item.Surname = request.Surname;
                item.DateBirth = request.date_birth;
                item.Gender = (request.gender.ToUpper() == "M") ? "m" : (request.gender.ToUpper() == "М") ? "m" : "w";
                item.Login = request.Login;
                item.Pass = HashPass(request.Pass);

                await _potentRepository.UpdPotent(id, item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }


        /// <summary>
        /// Ометить записть удаленной в списке Зарегистрированных пользователей по Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> SetDeletePotent(int id)
        {
            bool fl = await _potentRepository.SetDelPotent(id);
            if (!fl) return BadRequest("Not Found");

            return Ok();
        }

<<<<<<< HEAD
=======





>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        // -- Шифрование пароля --
        string HashPass(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(hash);
        }


    }
}
