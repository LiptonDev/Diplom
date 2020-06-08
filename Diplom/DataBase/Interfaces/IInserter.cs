using Diplom.Models;
using System.Threading.Tasks;

namespace Diplom.DataBase.Interfaces
{
    /// <summary>
    /// Добавление данных.
    /// </summary>
    interface IInserter
    {
        /// <summary>
        /// Добавление данных.
        /// </summary>
        /// <param name="item">Объект для добавления.</param>
        /// <returns></returns>
        Task<DbQueryResponse<bool>> InsertAsync<T>(T item) where T : class;
    }
}
