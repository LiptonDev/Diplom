using Diplom.Models;
using System.Threading.Tasks;

namespace Diplom.DataBase.Interfaces
{
    /// <summary>
    /// Пользователи.
    /// </summary>
    interface IUsers
    {
        /// <summary>
        /// Авторизация.
        /// </summary>
        /// <param name="loginUser">Пользователь для авторизации.</param>
        /// <returns></returns>
        Task<DbQueryResponse<bool>> LoginAsync(LoginUser loginUser);
    }
}
