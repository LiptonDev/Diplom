using Diplom.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Diplom.Provider
{
    /// <summary>
    /// Провайдер данных.
    /// </summary>
    interface IDataProvider
    {
        /// <summary>
        /// Машины.
        /// </summary>
        ObservableCollection<Truck> Trucks { get; }

        /// <summary>
        /// Полуприцепы.
        /// </summary>
        ObservableCollection<Semitrailer> Semitrailers { get; }

        /// <summary>
        /// Водители.
        /// </summary>
        ObservableCollection<Driver> Drivers { get; }

        /// <summary>
        /// Сотрудники.
        /// </summary>
        ObservableCollection<Staff> Staff { get; }

        /// <summary>
        /// Маршруты.
        /// </summary>
        ObservableCollection<Route> Routes { get; }

        /// <summary>
        /// Загрузка данных.
        /// </summary>
        void LoadAsync();

        /// <summary>
        /// Очистка данных.
        /// </summary>
        void Clear();
    }
}
