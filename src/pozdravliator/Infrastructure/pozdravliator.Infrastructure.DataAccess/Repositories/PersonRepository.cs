using Microsoft.EntityFrameworkCore;
using pozdravliator.Application.AppServices.Contexts.Repositories;
using pozdravliator.Contracts.Person;
using pozdravliator.Domain;
using pozdravliator.Infrastructure.DataAccess.Mapping;
using pozdravliator.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace pozdravliator.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class PersonRepository : IPersonRepository
    {
        private readonly IRepository<Person> _personRepository;

        public PersonRepository(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonDto>?> GetAllAsync(CancellationToken cancellationToken, int pageSize, int pageIndex)
        {
            var persons = await _personRepository.GetAll().Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            if (persons == null)
            {
                return null;
            }
            var result = (from person in persons
                          select Mapper.ToPersonDto(person)).AsEnumerable();
            return result;
        }

        public async Task<PersonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(id, cancellationToken);
            return Mapper.ToPersonDto(person);
        }

        public async Task<PersonDto?> GetFirstWhere(Expression<Func<Person, bool>> predicate, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetFirstWhere(predicate, cancellationToken);
            return Mapper.ToPersonDto(person);
        }

        public async Task<IEnumerable<PersonDto>?> GetRangeByIDAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetRangeByIDAsync(ids, cancellationToken);
            if (persons == null)
            {
                return null;
            }
            var result = (from person in persons
                          select Mapper.ToPersonDto(person)).AsEnumerable();
            return result;
        }

        public async Task<IEnumerable<PersonDto>?> GetWhere(Expression<Func<Person, bool>> predicate, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetWhere(predicate).ToListAsync(cancellationToken);
            if (persons == null)
            {
                return null;
            }
            var result = (from person in persons
                          select Mapper.ToPersonDto(person)).AsEnumerable();
            return result;
        }

        public async Task<IEnumerable<PersonDto>?> GetSortedN(Expression<Func<Person, object>> predicate, int count, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetAll().OrderBy(predicate).Take(count).ToListAsync(cancellationToken);
            if (persons == null)
            {
                return null;
            }
            var result = (from person in persons
                          select Mapper.ToPersonDto(person)).AsEnumerable();
            return result;
        }

        public async Task<Guid> AddAsync(CreatePersonDto person, CancellationToken cancellationToken)
        {
            Person entity = new()
            {
                Id = Guid.NewGuid(),
                Name = person.Name,
                Birthday = person.Birthday
            };

            return await _personRepository.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Guid id, EditPersonDto person, CancellationToken cancellationToken)
        {
            var entity = await _personRepository.GetByIdAsync(id, cancellationToken);

            if (person.Name != null)
            {
                entity.Name = person.Name;
            }
            if (person.Birthday != null)
            {
                entity.Birthday = person.Birthday;
            }

            await _personRepository.UpdateAsync(entity, cancellationToken);
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedPerson = await _personRepository.GetByIdAsync(id, cancellationToken);

            await _personRepository.DeleteAsync(existedPerson, cancellationToken);
            return false;
        }

        public async Task ModifyAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _personRepository.GetByIdAsync(id, cancellationToken);

            await _personRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
