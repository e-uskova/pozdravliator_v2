﻿namespace pozdravliator.Domain
{
    /// <summary>
    /// Фото
    /// </summary>
    public class Photo : BaseEntity
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
        public virtual Person Person { get; set; }
    }
}
