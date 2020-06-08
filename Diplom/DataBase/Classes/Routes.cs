using Dapper;
using Dapper.Contrib.Extensions;
using Diplom.DataBase.Interfaces;
using Diplom.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diplom.DataBase.Classes
{
    /// <summary>
    /// Маршруты.
    /// </summary>
    class Routes : ITable<Route>
    {
        /// <summary>
        /// Запросы к базе данных.
        /// </summary>
        private readonly IInvoker invoker;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Routes(IInvoker invoker)
        {
            this.invoker = invoker;
        }

        /// <summary>
        /// Получение всех маршрутов.
        /// </summary>
        /// <returns></returns>
        public Task<DbQueryResponse<IEnumerable<Route>>> GetAllAsync()
        {
            return invoker.QueryAsync(con => con.QueryAsync<Route, Truck, Route>("SELECT routes.*, trucks.* FROM routes LEFT JOIN trucks ON routes.truckId = trucks.id",
                (r, t) =>
                {
                    r.Truck = t;
                    return r;
                }));
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Obsolete("Not implemented.", true)]
        public Task<DbQueryResponse<Route>> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
