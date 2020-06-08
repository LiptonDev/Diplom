using Diplom.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diplom.DataBase.Interfaces
{
    /// <summary>
    /// Таблица базы данных.
    /// </summary>
    /// <typeparam name="T">Данные таблицы.</typeparam>
    interface ITable<T>
    {
        /// <summary>
        /// Получение всех данных из таблицы.
        /// </summary>
        /// <returns></returns>
        Task<DbQueryResponse<IEnumerable<T>>> GetAllAsync();

        /// <summary>
        /// Получение объекта по Id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns></returns>
        Task<DbQueryResponse<T>> GetItemByIdAsync(int id);
    }
}
