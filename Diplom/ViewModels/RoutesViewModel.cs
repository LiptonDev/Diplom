using DevExpress.Mvvm;
using Diplom.DataBase.Interfaces;
using Diplom.Dialogs.Manager;
using Diplom.Excel.Interfaces;
using Diplom.Models;
using Diplom.Provider;
using DryIoc;
using MaterialDesignThemes.Wpf;
using MaterialDesignXaml.DialogsHelper;
using MaterialDesignXaml.DialogsHelper.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diplom.ViewModels
{
    /// <summary>
    /// Routes view mode.
    /// </summary>
    class RoutesViewModel : NavigationViewModel<Route>, IExporterCommand
    {
        /// <summary>
        /// Экспорт.
        /// </summary>
        private readonly IExporter<IEnumerable<Route>> exporter;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public RoutesViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public RoutesViewModel(IContext context,
                               IDialogManager dialogManager,
                               ISnackbarMessageQueue messageQueue,
                               IDataProvider dataProvider,
                               IExporter<IEnumerable<Route>> exporter,
                               IContainer container)
            : base(context, dialogManager, messageQueue, container)
        {
            this.exporter = exporter;

            Items = dataProvider.Routes;

            ExportCommand = new DelegateCommand(Export);
            SetTruckCommand = new DelegateCommand<Route>(SetTruck, (route) => route != null);
            RemoveTruckCommand = new DelegateCommand<Route>(RemoveTruck, (route) => route?.TruckId.HasValue == true);
        }

        /// <summary>
        /// Удалить машину с маршрута.
        /// </summary>
        public ICommand<Route> RemoveTruckCommand { get; }

        /// <summary>
        /// Установить машину на маршрут.
        /// </summary>
        public ICommand<Route> SetTruckCommand { get; }

        /// <summary>
        /// Команда экспорта.
        /// </summary>
        public ICommand ExportCommand { get; }

        /// <summary>
        /// Экспорт.
        /// </summary>
        private void Export()
        {
            var res = exporter.Export(Items);
            var msg = res ? "Маршруты экспортированы" : "Маршруты не экспортированы";

            messageQueue.Enqueue(msg);

            Logger.Log.Info(msg);
        }

        /// <summary>
        /// Удалить машину с маршрута.
        /// </summary>
        /// <param name="route">Маршрут.</param>
        private async void RemoveTruck(Route route)
        {
            var clone = (Route)route.Clone();

            clone.TruckId = null;
            clone.Truck = null;

            var res = await context.Invoker.UpdateAsync(clone);

            if (res && res.Result)
            {
                route.SetAllFields(clone);

                messageQueue.Enqueue("Машина снята с маршрута");

                Logger.Log.Info($"Машина снята с маршрута (id: {route.Id})");
            }
        }

        /// <summary>
        /// Установить машину на маршрут.
        /// </summary>
        /// <param name="route">Маршрут.</param>
        private async void SetTruck(Route route)
        {
            var res = await dialogManager.SelectTruck(route.TruckId.HasValue ? route.TruckId.Value : -1);

            if (res == null)
                return; //cancel

            var clone = (Route)route.Clone();

            clone.Truck = res;
            clone.TruckId = res.Id;

            var update = await context.Invoker.UpdateAsync(clone);

            if (update && update.Result)
            {
                route.SetAllFields(clone);

                messageQueue.Enqueue("Машина посталена на маршрут");

                Logger.Log.Info($"Машина поставлена на маршрут (id: {route.Id})");
            }
        }

        /// <summary>
        /// Добавить маршрут.
        /// </summary>
        /// <param name="item">Маршрут.</param>
        /// <returns></returns>
        protected override async Task Add()
        {
            var res = await dialogManager.EditRoute(null, false);

            if (res == null) //cancel
                return;

            var insert = await context.Invoker.InsertAsync(res);

            if (insert && insert.Result)
            {
                Items.Add(res);

                messageQueue.Enqueue("Маршрут добавлен");

                Logger.Log.Info($"Маршрут добавлен (id: {res.Id})");
            }
        }

        /// <summary>
        /// Удалить маршрут.
        /// </summary>
        /// <param name="item">Маршрут.</param>
        /// <returns></returns>
        protected override async Task Delete(Route item)
        {
            var q = await dialogIdentifier.ShowMessageBoxAsync($"Удалить маршрут \"{item.From} - {item.To}\"", MaterialMessageBoxButtons.YesNo);

            if (q == MaterialMessageBoxButtons.No)
                return;

            var delete = await context.Invoker.DeleteAsync(item);

            if (delete && delete.Result)
            {
                Items.Remove(item);

                messageQueue.Enqueue("Маршрут удален");

                Logger.Log.Info($"Маршрут удален (id: {item.Id})");
            }
        }

        /// <summary>
        /// Редактировать маршрут.
        /// </summary>
        /// <param name="item">Маршрут.</param>
        /// <returns></returns>
        protected override async Task Edit(Route item)
        {
            var res = await dialogManager.EditRoute(item, true);

            if (res == null) //cancel
                return;

            var update = await context.Invoker.UpdateAsync(res);

            if (update && update.Result)
            {
                item.SetAllFields(res);

                messageQueue.Enqueue("Маршрут обновлен");

                Logger.Log.Info($"Маршрут обновлен (id: {item.Id})");
            }
        }
    }
}
