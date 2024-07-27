using System.ComponentModel.DataAnnotations;
using pozdravliator.Contracts.Base;

namespace pozdravliator.Contracts.Photo
{
    /// <summary>
    /// Фото
    /// </summary>
    public class PhotoDto : BaseDto
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Тип контента
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Персона
        /// </summary>
        [Required]
        public Guid PersonId { get; set; }
    }
}
