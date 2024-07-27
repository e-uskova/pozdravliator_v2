using pozdravliator.Application.AppServices.Contexts.Repositories;
using pozdravliator.Contracts.Person;
using pozdravliator.Domain;
using System.Linq.Expressions;

namespace pozdravliator.Application.AppServices.Contexts.Services
{
    /// <inheritdoc/>
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _postRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="PersonService"
        /// </summary>
        /// <param name="postRepository">Репозиторий для работы с объявлениями.</param>
        public PersonService(IPersonRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Task<IEnumerable<PersonDto>?> GetAllAsync(CancellationToken cancellationToken, int pageSize, int pageIndex)
        {
            return _postRepository.GetAllAsync(cancellationToken, pageSize, pageIndex);
        }

        public Task<PersonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _postRepository.GetByIdAsync(id, cancellationToken);
        }

        /*        public Task<PersonDto?> GetFirstWhere(Expression<Func<Domain.Person, bool>> predicate, CancellationToken cancellationToken)
                {
                    return _postRepository.GetFirstWhere(predicate, cancellationToken);
                }

                public Task<IEnumerable<PersonDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
                {
                    return _postRepository.GetRangeByIDAsync(ids, cancellationToken);
                }*/

        public Task<IEnumerable<PersonDto>?> GetWhere(Expression<Func<Domain.Person, bool>> predicate, CancellationToken cancellationToken)
        {
            return _postRepository.GetWhere(predicate, cancellationToken);
        }

        public Task<IEnumerable<PersonDto>?> GetSortedN(Expression<Func<Person, object>> predicate, int count, CancellationToken cancellationToken)
        {
            return _postRepository.GetSortedN(predicate, count, cancellationToken);
        }

        public async Task<Guid> AddAsync(CreatePersonDto post, CancellationToken cancellationToken)
        {
            return await _postRepository.AddAsync(post, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Guid id, EditPersonDto post, CancellationToken cancellationToken)
        {
            var existedPerson = await _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPerson == null)
            {
                return true;
            }

            return await _postRepository.UpdateAsync(id, post, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedPerson = await _postRepository.GetByIdAsync(id, cancellationToken);
            if (existedPerson == null)
            {
                return true;
            }

            return await _postRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
