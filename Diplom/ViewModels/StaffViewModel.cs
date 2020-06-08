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
    /// Staff view model.
    /// </summary>
    class StaffViewModel : NavigationViewModel<Staff>, IExporterCommand
    {
        /// <summary>
        /// Экспортер.
        /// </summary>
        private readonly IExporter<IEnumerable<Staff>> exporter;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public StaffViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public StaffViewModel(IContext context,
                              IDialogManager dialogManager,
                              ISnackbarMessageQueue messageQueue,
                              IDataProvider dataProvider,
                              IExporter<IEnumerable<Staff>> exporter,
                              IContainer container)
            : base(context, dialogManager, messageQueue, container)
        {
            this.exporter = exporter;

            Items = dataProvider.Staff;

            ExportCommand = new DelegateCommand(Export);
        }

        /// <summary>
        /// Команда экспорта.
        /// </summary>
        public ICommand ExportCommand { get; }

        /// <summary>
        /// Экспорт сотрудников.
        /// </summary>
        private void Export()
        {
            var res = exporter.Export(Items);
            var msg = res ? "Сотрудники экспортированы" : "Сотрудники не экспортированы";

            messageQueue.Enqueue(msg);

            Logger.Log.Info(msg);
        }

        /// <summary>
        /// Добавление сотрудника.
        /// </summary>
        /// <param name="item">Сотрудник.</param>
        /// <returns></returns>
        protected override async Task Add()
        {
            var res = await dialogManager.EditStaff(null, false);

            if (res == null) //cancel
                return;

            var insert = await context.Invoker.InsertAsync(res);

            if (insert && insert.Result)
            {
                Items.Add(res);

                messageQueue.Enqueue("Сотрудник добавлен");

                Logger.Log.Info($"Cотрудник добавлен (id: {res.Id})");
            }
        }

        /// <summary>
        /// Удаление сотрудника.
        /// </summary>
        /// <param name="item">Сотрудник.</param>
        /// <returns></returns>
        protected override async Task Delete(Staff item)
        {
            var q = await dialogIdentifier.ShowMessageBoxAsync($"Удалить сотрудника \"{item}\"?", MaterialMessageBoxButtons.YesNo);

            if (q == MaterialMessageBoxButtons.No)
                return;

            var delete = await context.Invoker.DeleteAsync(item);

            if (delete && delete.Result)
            {
                Items.Remove(item);

                messageQueue.Enqueue("Сотрудник удален");

                Logger.Log.Info($"Cотрудник удален (id: {item.Id})");
            }
        }

        /// <summary>
        /// Редактирование сотрудника.
        /// </summary>
        /// <param name="item">Сотрудник.</param>
        /// <returns></returns>
        protected override async Task Edit(Staff item)
        {
            var res = await dialogManager.EditStaff(item, true);

            if (res == null) //cancel
                return;

            var update = await context.Invoker.UpdateAsync(res);

            if (update && update.Result)
            {
                item.SetAllFields(res);

                messageQueue.Enqueue("Сотрудник обновлен");

                Logger.Log.Info($"Cотрудник обновлен (id: {item.Id})");
            }
        }
    }
}
