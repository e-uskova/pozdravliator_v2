using System.Net.Mail;

namespace pozdravliator.Domain
{
    /// <summary>
    /// День рождения
    /// </summary>
    public class Person : BaseEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateOnly Birthday { get; set; }

        public DateOnly NextBirthday
        {
            get
            {
                var thisYearBirthday = new DateOnly(DateTime.Today.Year, Birthday.Month, Birthday.Day);
                return thisYearBirthday >= DateOnly.FromDateTime(DateTime.Today) ? thisYearBirthday : new DateOnly(DateTime.Today.Year + 1, Birthday.Month, Birthday.Day);
            }
            set
            {
                
            }
        }

        /// <summary>
        /// Вложения
        /// </summary>
        public virtual Photo? Photo { get; set; }
    }
}
