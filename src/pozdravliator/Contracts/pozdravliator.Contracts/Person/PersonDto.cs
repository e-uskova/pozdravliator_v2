using pozdravliator.Contracts.Base;
using pozdravliator.Contracts.Photo;

namespace pozdravliator.Contracts.Person
{
    /// <summary>
    /// День рождения
    /// </summary>
    public class PersonDto : BaseDto
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateOnly Birthday { get; set; }

        public DateOnly NextBirthday { get; set; }

        /// <summary>
        /// Вложения.
        /// </summary>
        public virtual PhotoInfoDto? Photo { get; set; }
    }
}
