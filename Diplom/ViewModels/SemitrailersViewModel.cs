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
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diplom.ViewModels
{
    /// <summary>
    /// Semitrailers view model.
    /// </summary>
    class SemitrailersViewModel : NavigationViewModel<Semitrailer>, IExporterCommand
    {
        /// <summary>
        /// Экспортер.
        /// </summary>
        private readonly IExporter<IEnumerable<Semitrailer>> exporter;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public SemitrailersViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public SemitrailersViewModel(IContext context,
                                   IDialogManager dialogManager,
                                   ISnackbarMessageQueue messageQueue,
                                   IExporter<IEnumerable<Semitrailer>> exporter,
                                   IDataProvider dataProvider,
                                   IContainer container)
            : base(context, dialogManager, messageQueue, container)
        {
            this.exporter = exporter;

            ExportCommand = new DelegateCommand(Export);

            Items = dataProvider.Semitrailers;
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
            var msg = res ? "Полуприцепы экспортированы" : "Полуприцепы не экспортированы";

            messageQueue.Enqueue(msg);

            Logger.Log.Info(msg);
        }

        /// <summary>
        /// Редактирование полуприцепа.
        /// </summary>
        /// <param name="item">Полуприцеп.</param>
        /// <returns></returns>
        protected override async Task Edit(Semitrailer item)
        {
            var res = await dialogManager.EditSemitrail(item, true);

            if (res == null) //cancel
                return;

            var update = await context.Invoker.UpdateAsync(res);

            if (update && update.Result)
            {
                item.SetAllFields(res);

                messageQueue.Enqueue("Полуприцеп обновлен");

                Logger.Log.Info($"Полуприцеп обновлен (id: {item.Id})");
            }
        }

        /// <summary>
        /// Удаление полуприцепа.
        /// </summary>
        /// <param name="item">Полуприцеп.</param>
        /// <returns></returns>
        protected override async Task Delete(Semitrailer item)
        {
            var q = await dialogIdentifier.ShowMessageBoxAsync($"Удалить полуприцеп \"{item.Model}\" ({item})?", MaterialMessageBoxButtons.YesNo);

            if (q == MaterialMessageBoxButtons.No)
                return;

            var delete = await context.Invoker.DeleteAsync(item);

            if (delete && delete.Result)
            {
                Items.Remove(item);

                messageQueue.Enqueue("Полуприцеп удален");

                Logger.Log.Info($"Полуприцеп удален (id: {item.Id})");
            }
        }

        /// <summary>
        /// Добавление полуприцепа.
        /// </summary>
        /// <returns></returns>
        protected override async Task Add()
        {
            var res = await dialogManager.EditSemitrail(null, false);

            if (res == null) //cancel
                return;

            var insert = await context.Invoker.InsertAsync(res);

            if (insert && insert.Result)
            {
                Items.Add(res);

                messageQueue.Enqueue("Полуприцеп добавлен");

                Logger.Log.Info($"Полуприцеп обновлен (id: {res.Id})");
            }
        }
    }
}
