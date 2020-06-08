using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    /// <summary>
    /// Маршрут.
    /// </summary>
    [Table("routes")]
    class Route : BaseId, ICloneable
    {
        /// <summary>
        /// Машина на маршруте.
        /// </summary>
        public int? TruckId { get; set; }

        /// <summary>
        /// Место отправления.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите место отправления")]
        public string From { get; set; }

        /// <summary>
        /// Место назначения.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите место назначения")]
        public string To { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Введите описание")]
        public string Description { get; set; }

        /// <summary>
        /// Статус (false - активный, true - завершен).
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Машина на маршруте.
        /// </summary>
        [Write(false)]
        public Truck Truck { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
