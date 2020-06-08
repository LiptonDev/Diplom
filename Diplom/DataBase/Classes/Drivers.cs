using Dapper.Contrib.Extensions;
using Diplom.DataBase.Interfaces;
using Diplom.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.DataBase.Classes
{
    /// <summary>
    /// Водители.
    /// </summary>
    class Drivers : ITable<Driver>
    {
        /// <summary>
        /// Запросы к базе данных.
        /// </summary>
        private readonly IInvoker invoker;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Drivers(IInvoker invoker)
        {
            this.invoker = invoker;
        }

        /// <summary>
        /// Получение списка водителей.
        /// </summary>
        /// <returns></returns>
        public Task<DbQueryResponse<IEnumerable<Driver>>> GetAllAsync()
        {
            return invoker.QueryAsync(con => con.GetAllAsync<Driver>(), Enumerable.Empty<Driver>());
        }


        /// <summary>
        /// Получение водителя по его Id.
        /// </summary>
        /// <param name="id">Id водителя.</param>
        /// <returns></returns>
        public Task<DbQueryResponse<Driver>> GetItemByIdAsync(int id)
        {
            return invoker.QueryAsync(con => con.GetAsync<Driver>(id));
        }
    }
}
