using Dapper.Contrib.Extensions;
using Diplom.DataBase.Interfaces;
using Diplom.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.DataBase.Classes
{
    /// <summary>
    /// Полуприцепы.
    /// </summary>
    class Semitrailers : ITable<Semitrailer>
    {
        /// <summary>
        /// Запросы к базе данных.
        /// </summary>
        private readonly IInvoker invoker;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Semitrailers(IInvoker invoker)
        {
            this.invoker = invoker;
        }

        /// <summary>
        /// Получение списка полуприцепов.
        /// </summary>
        /// <returns></returns>
        public Task<DbQueryResponse<IEnumerable<Semitrailer>>> GetAllAsync()
        {
            return invoker.QueryAsync(con => con.GetAllAsync<Semitrailer>(), Enumerable.Empty<Semitrailer>());
        }

        /// <summary>
        /// Получение полуприцепа по его Id.
        /// </summary>
        /// <param name="id">Id полуприцепа.</param>
        /// <returns></returns>
        public Task<DbQueryResponse<Semitrailer>> GetItemByIdAsync(int id)
        {
            return invoker.QueryAsync(con => con.GetAsync<Semitrailer>(id));
        }
    }
}
