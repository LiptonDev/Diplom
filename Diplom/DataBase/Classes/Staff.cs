using Dapper.Contrib.Extensions;
using Diplom.DataBase.Interfaces;
using Diplom.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diplom.DataBase.Classes
{
    class Staff : ITable<Models.Staff>
    {
        /// <summary>
        /// Запросы к базе данных.
        /// </summary>
        private readonly IInvoker invoker;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Staff(IInvoker invoker)
        {
            this.invoker = invoker;
        }

        /// <summary>
        /// Получение списка всех сотрудников.
        /// </summary>
        /// <returns></returns>
        public Task<DbQueryResponse<IEnumerable<Models.Staff>>> GetAllAsync()
        {
            return invoker.QueryAsync(con => con.GetAllAsync<Models.Staff>());
        }

        /// <summary>
        /// Получение сотрудника по Id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns></returns>
        public Task<DbQueryResponse<Models.Staff>> GetItemByIdAsync(int id)
        {
            return invoker.QueryAsync(con => con.GetAsync<Models.Staff>(id));
        }
    }
}
