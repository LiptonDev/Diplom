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
    /// Машины.
    /// </summary>
    class Trucks : ITable<Truck>
    {
        /// <summary>
        /// Запросы к базе данных.
        /// </summary>
        private readonly IInvoker invoker;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Trucks(IInvoker invoker)
        {
            this.invoker = invoker;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Obsolete("Not implemented.", true)]
        public Task<DbQueryResponse<Truck>> GetItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение списка машин.
        /// </summary>
        /// <returns></returns>
        public Task<DbQueryResponse<IEnumerable<Truck>>> GetAllAsync()
        {
            return invoker.QueryAsync(con => con.QueryAsync<Truck, Driver, Semitrailer, Truck>("SELECT trucks.*,drivers.*,semitrails.* FROM trucks LEFT JOIN drivers ON trucks.driverId = drivers.id LEFT JOIN semitrails ON trucks.semitrailerId = semitrails.id ",
                (t, d, s) =>
                {
                    t.Driver = d;
                    t.Semitrailer = s;

                    return t;
                }));
        }
    }
}
