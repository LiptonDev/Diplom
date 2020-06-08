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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diplom.ViewModels
{
    /// <summary>
    /// Drivers view model.
    /// </summary>
    class DriversViewModel : NavigationViewModel<Driver>, IExporterCommand
    {
        /// <summary>
        /// Экспортер.
        /// </summary>
        private readonly IExporter<IEnumerable<Driver>> exporter;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public DriversViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DriversViewModel(IContext context,
                                ISnackbarMessageQueue messageQueue,
                                IDialogManager dialogManager,
                                IExporter<IEnumerable<Driver>> exporter,
                                IDataProvider dataProvider,
                                IContainer container)
            : base(context, dialogManager, messageQueue, container)
        {
            this.exporter = exporter;

            ExportCommand = new DelegateCommand(Export);

            Items = dataProvider.Drivers;
        }

        /// <summary>
        /// Команда экспорта.
        /// </summary>
        public ICommand ExportCommand { get; }

        /// <summary>
        /// Экспорт.
        /// </summary>
        void Export()
        {
            var res = exporter.Export(Items);
            var msg = res ? "Водители экспортированы" : "Водители не экспортированы";

            messageQueue.Enqueue(msg);

            Logger.Log.Info(msg);
        }

        /// <summary>
        /// Редактирование водителя.
        /// </summary>
        /// <param name="item">Водитель.</param>
        /// <returns></returns>
        protected override async Task Edit(Driver item)
        {
            var res = await dialogManager.EditDriver(item, true);

            if (res == null) //cancel
                return;

            var update = await context.Invoker.UpdateAsync(res);

            if (update && update.Result)
            {
                item.SetAllFields(res);

                messageQueue.Enqueue("Водитель обновлен");

                Logger.Log.Info($"Водитель обновлен (id: {res.Id})");
            }
        }

        /// <summary>
        /// Удаление водителя.
        /// </summary>
        /// <param name="item">Водитель.</param>
        /// <returns></returns>
        protected override async Task Delete(Driver item)
        {
            var q = await dialogIdentifier.ShowMessageBoxAsync($"Удалить водителя {item}?", MaterialMessageBoxButtons.YesNo);

            if (q == MaterialMessageBoxButtons.No)
                return;

            var delete = await context.Invoker.DeleteAsync(item);

            if (delete)
            {
                Items.Remove(item);

                messageQueue.Enqueue("Водитель удален");

                Logger.Log.Info($"Водитель удален (id: {item.Id})");
            }
        }

        /// <summary>
        /// Добавление водителя.
        /// </summary>
        /// <param name="item">Водитель.</param>
        /// <returns></returns>
        protected override async Task Add()
        {
            var res = await dialogManager.EditDriver(null, false);

            if (res == null) //cancel
                return;

            var insert = await context.Invoker.InsertAsync(res);

            if (insert && insert.Result)
            {
                Items.Add(res);

                messageQueue.Enqueue("Водитель добавлен");

                Logger.Log.Info($"Водитель удален (id: {res.Id})");
            }
        }
    }
}
