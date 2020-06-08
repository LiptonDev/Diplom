using Diplom.DataBase.Interfaces;
using Diplom.Models;

namespace Diplom.DataBase.Classes
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    class Context : IContext
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Context(IUsers users,
                       ITable<Driver> drivers,
                       ITable<Semitrailer> semitrailers,
                       ITable<Truck> trucks,
                       ITable<Models.Staff> staff,
                       ITable<Route> routes,
                       IInvoker invoker)
        {
            Users = users;
            Drivers = drivers;
            Semitrailers = semitrailers;
            Trucks = trucks;
            Staff = staff;
            Routes = routes;
            Invoker = invoker;
        }

        /// <summary>
        /// Пользователи.
        /// </summary>
        public IUsers Users { get; }

        /// <summary>
        /// Водители.
        /// </summary>
        public ITable<Driver> Drivers { get; }

        /// <summary>
        /// Полуприцепы.
        /// </summary>
        public ITable<Semitrailer> Semitrailers { get; }

        /// <summary>
        /// Машины.
        /// </summary>
        public ITable<Truck> Trucks { get; }

        /// <summary>
        /// Сотрудники.
        /// </summary>
        public ITable<Models.Staff> Staff { get; }

        /// <summary>
        /// Маршруты.
        /// </summary>
        public ITable<Route> Routes { get; }

        /// <summary>
        /// Запросы к базе данных.
        /// </summary>
        public IInvoker Invoker { get; }
    }
}
