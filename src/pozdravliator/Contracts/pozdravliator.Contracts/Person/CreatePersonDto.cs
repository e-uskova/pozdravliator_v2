using pozdravliator.Contracts.Photo;
using System.ComponentModel.DataAnnotations;

namespace pozdravliator.Contracts.Person
{
    public class CreatePersonDto
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Required]
        public DateOnly Birthday { get; set; }
    }
}
