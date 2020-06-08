using Dapper;
using Diplom.DataBase.Interfaces;
using Diplom.Models;
using System.Threading.Tasks;

namespace Diplom.DataBase.Classes
{
    /// <summary>
    /// Пользователи.
    /// </summary>
    class Users : IUsers
    {
        /// <summary>
        /// Запросы к базе данных.
        /// </summary>
        private readonly IInvoker invoker;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Users(IInvoker invoker)
        {
            this.invoker = invoker;
        }

        /// <summary>
        /// Авторизация.
        /// </summary>
        /// <param name="loginUser">Пользователь для авторизации.</param>
        /// <returns></returns>
        public Task<DbQueryResponse<bool>> LoginAsync(LoginUser loginUser)
        {
            return invoker.QueryAsync(async con =>
            {
                var user = await con.QueryFirstOrDefaultAsync<LoginUser>("SELECT * FROM users WHERE login = @Login AND password = @Password", loginUser);
                return user != null;
            });
        }
    }
}
