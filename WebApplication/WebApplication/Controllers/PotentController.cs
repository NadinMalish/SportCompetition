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
                    FullName = $"{x.lastname} {x.firstname} {x.surname}",
                    DateBirth = x.date_birth.ToString(),
                    Gender = (x.gender.ToLower() == "w") ? "жен." : "муж.",
                    Login = x.login,
                    Email = x.email,
                    DatReg = x.dat_reg
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
                    lastname = request.Lastname,
                    firstname = request.Firstname,
                    surname = request.Surname,
                    date_birth = request.date_birth,
                    email = request.Email,
                    login = request.Login,
                    pass = HashPass(request.Pass),
                    gender = (request.gender.ToUpper() == "M") ? "m" : (request.gender.ToUpper() == "М") ? "m" : "w"
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

                item.lastname = request.Lastname;
                item.firstname = request.Firstname;
                item.surname = request.Surname;
                item.date_birth = request.date_birth;
                item.gender = (request.gender.ToUpper() == "M") ? "m" : (request.gender.ToUpper() == "М") ? "m" : "w";
                item.login = request.Login;
                item.pass = HashPass(request.Pass);

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






        // -- Шифрование пароля --
        string HashPass(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(hash);
        }


    }
}
