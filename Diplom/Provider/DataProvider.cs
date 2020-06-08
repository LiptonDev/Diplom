using Diplom.DataBase.Interfaces;
using Diplom.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Diplom.Provider
{
    /// <summary>
    /// Провайдер данных.
    /// </summary>
    class DataProvider : IDataProvider
    {
        /// <summary>
        /// Машины.
        /// </summary>
        public ObservableCollection<Truck> Trucks { get; }

        /// <summary>
        /// Полуприцепы.
        /// </summary>
        public ObservableCollection<Semitrailer> Semitrailers { get; }

        /// <summary>
        /// Водители.
        /// </summary>
        public ObservableCollection<Driver> Drivers { get; }

        /// <summary>
        /// Сотрудники.
        /// </summary>
        public ObservableCollection<Staff> Staff { get; }

        /// <summary>
        /// Маршруты.
        /// </summary>
        public ObservableCollection<Route> Routes { get; }

        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly IContext context;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DataProvider(IContext context)
        {
            this.context = context;

            Trucks = new ObservableCollection<Truck>();
            Semitrailers = new ObservableCollection<Semitrailer>();
            Drivers = new ObservableCollection<Driver>();
            Staff = new ObservableCollection<Staff>();
            Routes = new ObservableCollection<Route>();
        }

        /// <summary>
        /// Загрузка данных.
        /// </summary>
        public async void LoadAsync()
        {
            Logger.Log.Info($"Загрузка данных");

            Clear();

            await LoadTable(Trucks, context.Trucks);
            await LoadTable(Semitrailers, context.Semitrailers);
            await LoadTable(Staff, context.Staff);
            await LoadTable(Drivers, context.Drivers);
            await LoadTable(Routes, context.Routes);
        }

        /// <summary>
        /// Загрузка данных таблицы.
        /// </summary>
        async Task LoadTable<T>(ObservableCollection<T> collection, ITable<T> table)
        {
            var res = await table.GetAllAsync();
            if (res)
                collection.AddRange(res.Result);
        }

        /// <summary>
        /// Очистка данных.
        /// </summary>
        public void Clear()
        {
            Trucks.Clear();
            Semitrailers.Clear();
            Drivers.Clear();
            Staff.Clear();
            Routes.Clear();
        }
    }
}
