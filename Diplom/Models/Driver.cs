using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    /// <summary>
    /// Водитель.
    /// </summary>
    [Table("drivers")]
    class Driver : BaseId, ICloneable
    {

        /// <summary>
        /// Номер В/У.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Введите номер В/У")]
        public int DriverLicense { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите отчество")]
        public string MiddleName { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }

        public override bool Equals(object obj)
        {
            return obj is Driver driver && driver.Id == Id;
        }
    }
}
