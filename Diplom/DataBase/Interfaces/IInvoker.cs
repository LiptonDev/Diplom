using Diplom.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Diplom.DataBase.Interfaces
{
    /// <summary>
    /// Запросы к базе данных.
    /// </summary>
    interface IInvoker : IInserter, IDeleter, IUpdater
    {
        /// <summary>
        /// Запрос к базе данных.
        /// </summary>
        /// <param name="func">Запрос.</param>
        /// <param name="defaultResult">Дефолтное значение результата.</param>
        /// <returns></returns>
        Task<DbQueryResponse<TResult>> QueryAsync<TResult>(Func<IDbConnection, Task<TResult>> func, TResult defaultResult = default);
    }
}
