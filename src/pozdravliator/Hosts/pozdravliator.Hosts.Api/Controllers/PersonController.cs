using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pozdravliator.Application.AppServices.Contexts.Services;
using pozdravliator.Contracts.Person;
using System.Net;

namespace pozdravliator.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с персоной
    /// </summary>
    [ApiController]
    [Route("/person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// Инициализация экземпляра <see cref="PersonController"/>
        /// </summary>
        /// <param name="personService">Сервис работы с персонами</param>
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Создание персоны
        /// </summary>
        /// <param name="person">Модель для создания персоны</param>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <returns>Идентификатор созданной сущности/></returns>
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePersonAsync(CreatePersonDto person, CancellationToken cancellationToken)
        {
            var id = await _personService.AddAsync(person, cancellationToken);
            return id == Guid.Empty ? BadRequest() : CreatedAtAction(nameof(GetPersonAsync), new { id }, id);
        }

        /// <summary>
        /// Получение персон постранично
        /// </summary>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="pageIndex">Номер страницы</param>
        /// <returns>Коллекция персон <see cref="PersonDto"/></returns>
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("/persons")]
        [HttpGet]
        public async Task<ActionResult<PersonDto>> GetPersonsAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
        {
            var persons = await _personService.GetAllAsync(cancellationToken, pageSize, pageIndex);
            return persons == null ? BadRequest() : Ok(persons);
        }

        /// <summary>
        /// Получение сегодняшних и ближайших др
        /// </summary>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <param name="count">Количество</param>
        /// <returns>Коллекция персон <see cref="PersonDto"/></returns>
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("/")]
        [HttpGet]
        public async Task<ActionResult<PersonDto>> GetNearestBirthdaysAsync(CancellationToken cancellationToken, int count = 3)
        {
            var persons = await _personService.GetSortedN(e => e.NextBirthday, count, cancellationToken);
            return persons == null ? BadRequest() : Ok(persons);
        }

        /// <summary>
        /// Получение персоны по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор персоны</param>
        /// <param name="cancellationToken">Отмена операции</param>
        /// <returns>Модель персоны <see cref="PersonDto"/></returns>
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ActionName(nameof(GetPersonAsync))]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PersonDto>> GetPersonAsync(Guid id, CancellationToken cancellationToken)
        {
            var person = await _personService.GetByIdAsync(id, cancellationToken);
            return person == null ? BadRequest("Объявление не найдено.") : Ok(person);
        }

        /// <summary>
        /// Редактирование персоны
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person">Модель для редактирования персоны</param>
        /// <param name="cancellationToken">Отмена операции</param>
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PersonDto>> EditPersonAsync(Guid id, EditPersonDto person, CancellationToken cancellationToken)
        {
            var result = await _personService.UpdateAsync(id, person, cancellationToken);
            return result ? BadRequest() : Ok();
        }

        /// <summary>
        /// Удаление персоны по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор персоны</param>
        /// <param name="cancellationToken">Отмена операции</param>
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PersonDto>> DeletePersonAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _personService.DeleteAsync(id, cancellationToken);
            return result ? BadRequest() : Ok();
        }
    }
}
