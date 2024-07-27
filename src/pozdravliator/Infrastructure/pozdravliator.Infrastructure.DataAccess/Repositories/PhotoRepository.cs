using Microsoft.EntityFrameworkCore;
using pozdravliator.Application.AppServices.Contexts.Repositories;
using pozdravliator.Contracts.Photo;
using pozdravliator.Domain;
using pozdravliator.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace pozdravliator.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IRepository<Photo> _photoRepository;
        private readonly IRepository<Person> _personRepository;

        public PhotoRepository(IRepository<Photo> photoRepository, IRepository<Person> personRepository)
        {
            _photoRepository = photoRepository;
            _personRepository = personRepository;
        }

        ///<inheritdoc/>
        public async Task<PhotoInfoDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _photoRepository.GetAll().Where(a => a.Id == id).Select(a => new PhotoInfoDto
            {
                Id = a.Id,
                Name = a.Name,
                ContentType = a.ContentType,
                Length = a.Length,
                Created = a.Created,
                PersonId = a.Person.Id,
            }).FirstOrDefaultAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _photoRepository.GetByIdAsync(id, cancellationToken);

            await _photoRepository.DeleteAsync(entity, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<Guid> UploadAsync(PhotoDto photo, Guid personId, CancellationToken cancellationToken)
        {
            var entity = new Photo
            {
                Name = photo.Name,
                Content = photo.Content,
                ContentType = photo.ContentType,
                Created = DateTime.UtcNow,
                Length = photo.Content.Length,
                Person = await _personRepository.GetByIdAsync(personId, cancellationToken)
            };

            await _photoRepository.AddAsync(entity, cancellationToken);
            return entity.Id;
        }

        ///<inheritdoc/>
        public Task<PhotoDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return _photoRepository.GetAll().Where(s => s.Id == id).Select(s => new PhotoDto
            {
                Content = s.Content,
                ContentType = s.ContentType,
                Name = s.Name,
                PersonId = s.Person.Id,
            }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
