using Dapper.Contrib.Extensions;
using Diplom.DataBase.Interfaces;
using Diplom.Models;
using Diplom.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Diplom.DataBase.Classes
{
    /// <summary>
    /// Запросы к базе данных.
    /// </summary>
    class Invoker : IInvoker
    {
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Invoker()
        {
            connectionString = $"server={Settings.Default.dbHost};port={Settings.Default.dbPort};userid={Settings.Default.dbUser};pwd={Settings.Default.dbPwd};database={Settings.Default.dbName};Convert Zero Datetime=True";
        }

        /// <summary>
        /// Удаление данных.
        /// </summary>
        /// <param name="item">Объект для удаления.</param>
        /// <returns></returns>
        public Task<DbQueryResponse<bool>> DeleteAsync<T>(T item) where T : class
        {
            return QueryAsync(con => con.DeleteAsync(item));
        }

        /// <summary>
        /// Добавление данных.
        /// </summary>
        /// <param name="item">Объект для добавления.</param>
        /// <returns></returns>
        public async Task<DbQueryResponse<bool>> InsertAsync<T>(T item) where T : class
        {
            var res = await QueryAsync(con => con.InsertAsync(item));

            return new DbQueryResponse<bool>(res ? res.Result > -1 : false, res.QueryResult);
        }

        /// <summary>
        /// Обновление данных.
        /// </summary>
        /// <param name="item">Объект для обновления.</param>
        /// <returns></returns>
        public Task<DbQueryResponse<bool>> UpdateAsync<T>(T item) where T : class
        {
            return QueryAsync(con => con.UpdateAsync(item));
        }

        /// <summary>
        /// Запрос к базе данных.
        /// </summary>
        /// <param name="func">Запрос.</param>
        /// <param name="defaultResult">Дефолтное значение результата.</param>
        /// <returns></returns>
        public async Task<DbQueryResponse<TResult>> QueryAsync<TResult>(Func<IDbConnection, Task<TResult>> func, TResult defaultResult = default)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var res = await func(connection);

                    return new DbQueryResponse<TResult>(res, DbQueryResult.Ok);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());

                    Logger.Log.Error("Ошибка запроса к базе данных", ex);

                    return new DbQueryResponse<TResult>(defaultResult, DbQueryResult.Error);
                }
            }
        }
    }
}
