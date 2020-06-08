using Dapper.Contrib.Extensions;
using Diplom.ViewModels;

namespace Diplom.Models
{
    /// <summary>
    /// Base Id.
    /// </summary>
    class BaseId : ValidateViewModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        [ChangeIgnore]
        public int Id { get; set; }
    }
}
