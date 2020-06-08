using Diplom.Models;
using System.Threading.Tasks;

namespace Diplom.DataBase.Interfaces
{
    /// <summary>
    /// Удаление данных.
    /// </summary>
    interface IDeleter
    {
        /// <summary>
        /// Удаление данных.
        /// </summary>
        /// <param name="item">Объект для удаления.</param>
        /// <returns></returns>
        Task<DbQueryResponse<bool>> DeleteAsync<T>(T item) where T : class;
    }
}
