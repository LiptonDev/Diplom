using Diplom.Models;
using System.Threading.Tasks;

namespace Diplom.DataBase.Interfaces
{
    /// <summary>
    /// Обновление данных.
    /// </summary>
    interface IUpdater
    {
        /// <summary>
        /// Обновление данных.
        /// </summary>
        /// <param name="item">Объект для обновления.</param>
        /// <returns></returns>
        Task<DbQueryResponse<bool>> UpdateAsync<T>(T item) where T : class;
    }
}
