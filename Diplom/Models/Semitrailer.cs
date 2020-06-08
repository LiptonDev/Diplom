using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    /// <summary>
    /// Полуприцеп.
    /// </summary>
    [Table("semitrails")]
    class Semitrailer : BaseId, ICloneable
    {
        /// <summary>
        /// Модель полуприцепа.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите модель полуприцепа")]
        public string Model { get; set; }

        /// <summary>
        /// Номер полуприцепа.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите номер полуприцепа")]
        [RegularExpression("[АВЕКМНОРСТУХ]{2}[0-9]{4}", ErrorMessage = "Формат номера: АА0000")]
        public string SemitrailerNumber { get; set; }

        /// <summary>
        /// Регион на номере.
        /// </summary>
        [Range(1, 1000, ErrorMessage = "Введите регион полуприцепа")]
        public int SemitrailerNumberRegion { get; set; }

        public override string ToString()
        {
            return $"{SemitrailerNumber} | {SemitrailerNumberRegion}";
        }

        public override bool Equals(object obj)
        {
            return obj is Semitrailer semitrail && semitrail.Id == Id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
