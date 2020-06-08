using Diplom.Models;

namespace Diplom.DataBase.Interfaces
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    interface IContext
    {
        /// <summary>
        /// Пользователи.
        /// </summary>
        IUsers Users { get; }

        /// <summary>
        /// Водители.
        /// </summary>
        ITable<Driver> Drivers { get; }

        /// <summary>
        /// Полуприцепы.
        /// </summary>
        ITable<Semitrailer> Semitrailers { get; }

        /// <summary>
        /// Машины.
        /// </summary>
        ITable<Truck> Trucks { get; }

        /// <summary>
        /// Сотрудники.
        /// </summary>
        ITable<Staff> Staff { get; }

        /// <summary>
        /// Маршруты.
        /// </summary>
        ITable<Route> Routes { get; }

        /// <summary>
        /// Запросы к базе данных.
        /// </summary>
        IInvoker Invoker { get; }
    }
}
