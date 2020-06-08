using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    /// <summary>
    /// Машина.
    /// </summary>
    [Table("trucks")]
    class Truck : BaseId, ICloneable
    {
        /// <summary>
        /// Серия.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Введите серию автомобиля")]
        public int Series { get; set; }

        /// <summary>
        /// Модель машины.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите модель машины")]
        public string CarModel { get; set; }

        /// <summary>
        /// Номер машины.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите номер машины")]
        [RegularExpression("[АВЕКМНОРСТУХ]{1}[0-9]{3}[АВЕКМНОРСТУХ]{2}", ErrorMessage = "Формат номера: А000АА")]
        public string CarNumber { get; set; }

        /// <summary>
        /// Регион на номере.
        /// </summary>
        [Range(1, 1000, ErrorMessage = "Введите регион машины")]
        public int CarNumberRegion { get; set; }

        /// <summary>
        /// Id водителя.
        /// </summary>
        public int? DriverId { get; set; }

        /// <summary>
        /// Id полуприцепа.
        /// </summary>
        public int? SemitrailerId { get; set; }

        /// <summary>
        /// Водитель.
        /// </summary>
        [Write(false)]
        [ChangeIgnore]
        public Driver Driver { get; set; }

        /// <summary>
        /// Полуприцеп.
        /// </summary>
        /// <returns></returns>
        [Write(false)]
        [ChangeIgnore]
        public Semitrailer Semitrailer { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return CarModel;
        }

        public override bool Equals(object obj)
        {
            return obj is Truck truck && truck.Id == Id;
        }
    }
}
