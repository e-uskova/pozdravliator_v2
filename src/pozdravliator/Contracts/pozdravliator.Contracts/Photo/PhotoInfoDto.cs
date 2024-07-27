using pozdravliator.Contracts.Base;

namespace pozdravliator.Contracts.Photo
{
    public class PhotoInfoDto : BaseDto
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип контента
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Размер файла
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Время создания
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Персона
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
