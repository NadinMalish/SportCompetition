using Microsoft.AspNetCore.Mvc;
using SportCompetition.Infrastructure;
using SportCompetition.Domain;
using SportCompetition.Model;


namespace SportCompetition.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PotentController : ControllerBase
    {
        private readonly IRepository<Potent> _Repository;
        private readonly IRepositoryExtD<Potent> _RepositoryExtD;
        private readonly IPotentRepository _potentRepository;

        public PotentController(IRepository<Potent> Repository, IRepositoryExtD<Potent> RepositoryExtD, IPotentRepository potentRepository)
        {
            _Repository = Repository;
            _RepositoryExtD = RepositoryExtD;
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
                var _potent = (await _RepositoryExtD.GetAllNotDell()).ToList();
                var potentModelList = _potent.Select(x => new PotentSpis()
                {
                    Id=x.id,
                    FullName = $"{x.lastname} {x.firstname} {x.surname}",
                    DatBirth = x.date_birth.ToString(),
                    //Gender = x.gender.ToString(),
                    Login=x.login,
                    Email=x.email,
                    DatReg=x.dat_reg.ToString()
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
            var _potent = await _Repository.GetByIdAsync(id);
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
            var _potent = await _potentRepository.GetPotentByLogin(login, pass);
            return Ok(_potent);
        }


        /// <summary>
        /// Ометить записть удаленной в списке Зарегистрированных пользователей по Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> SetDeletePotent(int id)
        {
            bool fl = await _RepositoryExtD.SetDelById(id);
            if (!fl) return BadRequest("Not Found");

            return Ok();
        }

        /// <summary>
        /// Добавить запись в список Зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreatePotent(Potent request)
        {
            try
            {
                await _potentRepository.AddPotent(request);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }


        /// <summary>
        /// Редактировать запись в списоке Зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePotent(PotentShortResponse request)
        {
            try
            {
                await _potentRepository.UpdPotent(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }


    }
}
