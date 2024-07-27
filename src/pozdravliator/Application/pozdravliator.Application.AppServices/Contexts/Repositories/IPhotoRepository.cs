using pozdravliator.Contracts.Photo;

namespace pozdravliator.Application.AppServices.Contexts.Repositories
{
    /// <summary>
    /// Репозиторий для работы с фото
    /// </summary>
    public interface IPhotoRepository
    {
        /// <summary>
        /// Получение информации о фото без контента по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор вложения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Информация о файле</returns>
        Task<PhotoInfoDto?> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление фото по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор вложения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="attachment">Файл</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор файла</returns>
        Task<Guid> UploadAsync(PhotoDto attachment, Guid postId, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла
        /// </summary>
        /// <param name="id">Идентификатор файла</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Файл</returns>
        Task<PhotoDto?> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}