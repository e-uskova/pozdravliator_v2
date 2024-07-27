using pozdravliator.Contracts.Person;
using pozdravliator.Domain;
using System.Linq.Expressions;

namespace pozdravliator.Application.AppServices.Contexts.Services
{
    /// <summary>
    /// Сервис работы с персонами
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Получение всех элементов
        /// </summary>
        /// <returns>Коллекция элементов типа <see cref="PersonDto"/></returns>
        Task<IEnumerable<PersonDto>?> GetAllAsync(CancellationToken cancellationToken, int pageSize, int pageIndex);

        /// <summary>
        /// Получение элемента по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <returns>Элемент типа <see cref="PersonDto"/></returns>
        Task<PersonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /*/// <summary>
        /// Получение элементов по списку идентификаторов
        /// </summary>
        /// <param name="ids">Список идентификаторов</param>
        /// <returns>Коллекция элементов типа <see cref="PersonDto"/></returns>
        Task<IEnumerable<PersonDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получение первого элемента из удовлетворяющих условию
        /// </summary>
        /// <param name="predicate">Условие отбора</param>
        /// <returns>Элемент типа <see cref="PersonDto"/></returns>
        Task<PersonDto?> GetFirstWhere(Expression<Func<Domain.Person, bool>> predicate, CancellationToken cancellationToken);*/

        /// <summary>
        /// Получение всех элементов, удовлетворяющих условию
        /// </summary>
        /// <param name="predicate">Условие отбора</param>
        /// <returns>Коллекция элементов типа <see cref="PersonDto"/></returns>
        Task<IEnumerable<PersonDto>?> GetWhere(Expression<Func<Domain.Person, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Получение N первых отсортированных элементов
        /// </summary>
        /// <param name="predicate">Условие сортировки</param>
        /// <param name="count">Количество</param>
        /// <returns>Коллекция элементов типа <see cref="PersonDto"</returns>
        Task<IEnumerable<PersonDto>?> GetSortedN(Expression<Func<Person, object>> predicate, int count, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="entity">Персона</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreatePersonDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Изменение элемента
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, EditPersonDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
