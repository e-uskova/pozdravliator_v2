using Microsoft.AspNetCore.Mvc;
using pozdravliator.Application.AppServices.Contexts.Services;
using pozdravliator.Contracts.Photo;
using System.Net;

namespace pozdravliator.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с фото
    /// </summary>
    [ApiController]
    [Route("/photo")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        /// <summary>
        /// Инициализация экземпляра <see cref="PhotoController"/>
        /// </summary>
        /// <param name="photoService">Сервис работы с фото</param>
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Загрузка файла в систему
        /// </summary>
        /// <param name="photo">Файл</param>
        /// <param name="personId">Идентификатор персоны</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile photo, Guid personId, CancellationToken cancellationToken)
        {
            var bytes = await GetBytesAsync(photo, cancellationToken);
            var photoDto = new PhotoDto
            {
                Content = bytes,
                ContentType = photo.ContentType,
                Name = photo.FileName,
            };

            var result = await _photoService.UploadAsync(photoDto, personId, cancellationToken);

            return result == Guid.Empty ? BadRequest() : StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Скачивание файла
        /// </summary>
        /// <param name="id">Ижентификатор файла</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
        {
            var result = await _photoService.DownloadAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            Response.ContentLength = result.Content.Length;
            return File(result.Content, result.ContentType, result.Name);
        }

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="id">Идентификатор файла</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePhotoAsync(Guid id, CancellationToken cancellationToken)
        {
            var photo = await _photoService.GetInfoByIdAsync(id, cancellationToken);
            if (photo == null)
            {
                return BadRequest();
            }

            var result = await _photoService.DeleteAsync(id, cancellationToken);
            return result ? BadRequest() : Ok();
        }

        private async Task<byte[]> GetBytesAsync(IFormFile photo, CancellationToken cancellationToken)
        {
            var ms = new MemoryStream();
            await photo.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();
        }
    }
}

