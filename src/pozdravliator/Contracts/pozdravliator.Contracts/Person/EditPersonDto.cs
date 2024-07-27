using pozdravliator.Contracts.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pozdravliator.Contracts.Person
{
    public class EditPersonDto
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateOnly Birthday { get; set; }
    }
}
