using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    /// <summary>
    /// Работник компании.
    /// </summary>
    [Table("staff")]
    class Staff : BaseId, ICloneable
    {
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

        /// <summary>
        /// Должность.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите должность")]
        public string Position { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
