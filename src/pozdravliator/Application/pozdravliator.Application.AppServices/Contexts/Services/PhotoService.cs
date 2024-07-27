using pozdravliator.Application.AppServices.Contexts.Repositories;
using pozdravliator.Contracts.Photo;

namespace pozdravliator.Application.AppServices.Contexts.Services
{
    /// <inheritdoc/>
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="PhotoService"/>
        /// </summary>
        /// <param name="photoRepository">Репозиторий для работы с фото</param>
        public PhotoService(IPhotoRepository photoRepository, IPersonRepository personRepository)
        {
            _photoRepository = photoRepository;
            _personRepository = personRepository;
        }

        /// <inheritdoc/>
        public Task<PhotoInfoDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _photoRepository.GetInfoByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedEntity = await _photoRepository.GetInfoByIdAsync(id, cancellationToken);
            if (existedEntity == null)
            {
                return true;
            }

            await _photoRepository.DeleteAsync(id, cancellationToken);

            return false;
        }

        /// <inheritdoc/>
        public async Task<Guid> UploadAsync(PhotoDto photo, Guid personId, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(personId, cancellationToken);
            if (person == null)
            {
                return Guid.Empty;
            }

            var result = await _photoRepository.UploadAsync(photo, personId, cancellationToken);

            return result;
        }

        /// <inheritdoc/>
        public async Task<PhotoDto?> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedEntity = await _photoRepository.GetInfoByIdAsync(id, cancellationToken);
            if (existedEntity == null)
            {
                return null;
            }

            return await _photoRepository.DownloadAsync(id, cancellationToken);
        }
    }
}
