using DevExpress.Mvvm;
using Diplom.DataBase.Interfaces;
using Diplom.Dialogs.Manager;
using Diplom.Excel;
using Diplom.Excel.Interfaces;
using Diplom.Models;
using Diplom.Properties;
using Diplom.Provider;
using DryIoc;
using MaterialDesignThemes.Wpf;
using MaterialDesignXaml.DialogsHelper;
using MaterialDesignXaml.DialogsHelper.Enums;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diplom.ViewModels
{
    /// <summary>
    /// Trucks view model.
    /// </summary>
    class TrucksViewModel : NavigationViewModel<Truck>, IExporterCommand
    {
        /// <summary>
        /// Экспортер.
        /// </summary>
        private readonly IExporter<IEnumerable<Truck>> exporter;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public TrucksViewModel()
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TrucksViewModel(IContext context,
                               IDialogManager dialogManager,
                               ISnackbarMessageQueue messageQueue,
                               IExporter<IEnumerable<Truck>> exporter,
                               IDataProvider dataProvider,
                               IContainer container)
            : base(context, dialogManager, messageQueue, container)
        {
            this.exporter = exporter;

            SetDriverCommand = new AsyncCommand(SetDriver, TruckIsSelected);
            RemoveDriverCommand = new AsyncCommand(RemoveDriver, () => TruckIsSelected() && SelectedItem.DriverId.HasValue);

            SetSemitrailCommand = new AsyncCommand(SetSemitrail, TruckIsSelected);
            RemoveSemitrailCommand = new AsyncCommand(RemoveSemitrail, () => TruckIsSelected() && SelectedItem.SemitrailerId.HasValue);

            ExportCommand = new DelegateCommand(Export);

            CreateWaybillCommand = new DelegateCommand<Truck>(CreateWaybill, (x) => TruckIsSelected());

            Items = dataProvider.Trucks;
        }

        bool TruckIsSelected() => SelectedItem != null;

        /// <summary>
        /// Команда установки водителя на машину.
        /// </summary>
        public ICommand SetDriverCommand { get; }

        /// <summary>
        /// Команда удаления водителя с машины.
        /// </summary>
        public ICommand RemoveDriverCommand { get; }

        /// <summary>
        /// Команда установки полуприцепа на машину.
        /// </summary>
        public ICommand SetSemitrailCommand { get; }

        /// <summary>
        /// Команда удаления полуприцепа с машины.
        /// </summary>
        public ICommand RemoveSemitrailCommand { get; }

        /// <summary>
        /// Команда экспорта.
        /// </summary>
        public ICommand ExportCommand { get; }

        /// <summary>
        /// Команда создания путевого листа.
        /// </summary>
        public ICommand<Truck> CreateWaybillCommand { get; }

        /// <summary>
        /// Создание путевого листа.
        /// </summary>
        private async void CreateWaybill(Truck truck)
        {
            var staff = await dialogManager.SelectStaffForWaybill();

            if (staff == null)
                return; //cancel

            string name = $"Путевой лист_{DateTime.Now.ToShortDateString()}_{truck}_{truck.CarNumber}_{truck.CarNumberRegion}";

            string path = dialogManager.GetSaveFileName(name);
            if (path == string.Empty)
                return;

            File.WriteAllBytes(path, Resources.Путевой_лист);

            using (ExcelPackage excel = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = excel.Workbook.Worksheets[0];

                var now = DateTime.Now;

                worksheet.Cells["CC3"].SetValue(truck.Series.ToString("00")); //СЕРИЯ
                worksheet.Cells["CW3"].SetValue(now.Month); //СЕРИЯ №

                worksheet.Cells["BG5"].SetValue(now.Day.ToString()); //День
                worksheet.Cells["BP5"].SetValue(now.ToString("MMMM")); //Месяц
                worksheet.Cells["CL5"].SetValue(now.Year.ToString()); //Год

                worksheet.Cells["Q6"].SetValue(Settings.Default.organization); //Организация

                worksheet.Cells["Q13"].SetValue(truck.CarModel); //Марка автомобиля
                worksheet.Cells["AB14"].SetValue($"{truck.CarNumber}/{truck.CarNumberRegion}"); //Гос. номерной знак
                if (truck.DriverId.HasValue)
                {
                    worksheet.Cells["I15"].SetValue(truck.Driver); //ФИО водителя
                    worksheet.Cells["CO46"].SetValue(truck.Driver);//ФИО водителя (АВТО ПРИНЯЛ, ПОДПИСЬ)
                    worksheet.Cells["P17"].SetValue(truck.Driver.DriverLicense.ToString("0000 000000")); //Номер В/У
                }

                if (truck.SemitrailerId.HasValue)
                {
                    worksheet.Cells["I21"].SetValue(truck.Semitrailer.Model); //Модель п/п
                    worksheet.Cells["AR21"].SetValue($"{truck.Semitrailer.SemitrailerNumber}/{truck.Semitrailer.SemitrailerNumberRegion}"); //Гос. номер знак п/п
                }

                worksheet.Cells["V46"].SetValue(staff.Dispatcher); //Диспетчер
                worksheet.Cells["CO44"].SetValue(staff.Mechanic); //Механик
                worksheet.Cells["AI50"].SetValue(staff.Medical); //Мед. работник

                excel.Save();

                messageQueue.Enqueue("Путевой лист создан", "Открыть", () => Process.Start(path));

                Logger.Log.Info($"Создан путевой лист \"{path}\"");
            }
        }

        /// <summary>
        /// Экспорт.
        /// </summary>
        void Export()
        {
            var res = exporter.Export(Items);
            var msg = res ? "Автомобили экспортированы" : "Автомобили не экспортированы";

            messageQueue.Enqueue(msg);

            Logger.Log.Info(msg);
        }

        /// <summary>
        /// Метод редактирования объекта.
        /// </summary>
        /// <returns></returns>
        protected override async Task Edit(Truck item)
        {
            var res = await dialogManager.EditTruck(item, true);

            if (res == null) //cancel
                return;

            var update = await context.Invoker.UpdateAsync(res);

            if (update && update.Result)
            {
                item.SetAllFields(res);

                messageQueue.Enqueue("Машина обновлена");

                Logger.Log.Info($"Машина обновлена (id: {item.Id})");
            }
        }

        /// <summary>
        /// Метод удаления объекта.
        /// </summary>
        /// <returns></returns>
        protected override async Task Delete(Truck item)
        {
            var q = await dialogIdentifier.ShowMessageBoxAsync($"Удалить машину \"{item.CarModel}\" ({item.CarNumber} | {item.CarNumberRegion})?", MaterialMessageBoxButtons.YesNo);

            if (q == MaterialMessageBoxButtons.No)
                return;

            var delete = await context.Invoker.DeleteAsync(item);

            if (delete && delete.Result)
            {
                Items.Remove(item);

                messageQueue.Enqueue("Машина удалена");

                Logger.Log.Info($"Машина удалена (id: {item.Id})");
            }
        }

        /// <summary>
        /// Метод добавления объекта.
        /// </summary>
        /// <returns></returns>
        protected override async Task Add()
        {
            var res = await dialogManager.EditTruck(null, false);

            if (res == null) //cancel
                return;

            var insert = await context.Invoker.InsertAsync(res);

            if (insert && insert.Result)
            {
                Items.Add(res);

                messageQueue.Enqueue("Машина добавлена");

                Logger.Log.Info($"Машина добавлена (id: {res.Id})");
            }
        }

        /// <summary>
        /// Удаление водителя с машины.
        /// </summary>
        /// <returns></returns>
        private Task RemoveDriver()
        {
            return Remove(true);
        }

        /// <summary>
        /// Удаление полуприцепа с машины.
        /// </summary>
        /// <returns></returns>
        private Task RemoveSemitrail()
        {
            return Remove(false);
        }

        /// <summary>
        /// Установка водителя на машину.
        /// </summary>
        /// <returns></returns>
        private Task SetDriver()
        {
            return Set(true);
        }

        /// <summary>
        /// Установка полуприцепа на машину.
        /// </summary>
        /// <returns></returns>
        private Task SetSemitrail()
        {
            return Set(false);
        }

        /// <summary>
        /// Удаление водителя или полуприцепа из машины.
        /// </summary>
        /// <param name="removeDriver">True - удаление водителя. False - удаление полуприцепа.</param>
        /// <returns></returns>
        async Task Remove(bool removeDriver)
        {
            var copy = (Truck)SelectedItem.Clone();

            if (removeDriver)
            {
                copy.Driver = null;
                copy.DriverId = null;
            }
            else
            {
                copy.Semitrailer = null;
                copy.SemitrailerId = null;
            }

            var res = await context.Invoker.UpdateAsync(copy);

            if (res)
            {
                if (removeDriver)
                {
                    SelectedItem.Driver = null;
                    SelectedItem.DriverId = null;

                    messageQueue.Enqueue("Водитель снят");

                    Logger.Log.Info($"Водитель снят с машины (id: {SelectedItem.Id})");
                }
                else
                {
                    SelectedItem.Semitrailer = null;
                    SelectedItem.SemitrailerId = null;

                    messageQueue.Enqueue("Полуприцеп снят");

                    Logger.Log.Info($"Полуприцеп снят с машины (id: {SelectedItem.Id})");
                }

                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        /// <summary>
        /// Установить полуприцеп или водителя.
        /// </summary>
        /// <param name="setDriver">True - установить водителя. False - установить полуприцеп.</param>
        /// <returns></returns>
        async Task Set(bool setDriver)
        {
            int current = 0;
            BaseId res;

            if (setDriver)
            {
                current = SelectedItem.DriverId.HasValue ? SelectedItem.DriverId.Value : -1;
                res = await dialogManager.SelectDriver(current);
            }
            else
            {
                current = SelectedItem.SemitrailerId.HasValue ? SelectedItem.SemitrailerId.Value : -1;
                res = await dialogManager.SelectSemitrail(current);
            }

            if (res == null)
                return;

            var copy = (Truck)SelectedItem.Clone();

            if (setDriver)
                copy.DriverId = res.Id;
            else copy.SemitrailerId = res.Id;

            var update = await context.Invoker.UpdateAsync(copy);

            if (update)
            {
                if (setDriver)
                {
                    SelectedItem.Driver = (Driver)res;
                    SelectedItem.DriverId = res.Id;

                    messageQueue.Enqueue("Водитель установлен");

                    Logger.Log.Info($"Водитель поставлен на машину (id: {SelectedItem.Id})");
                }
                else
                {
                    SelectedItem.Semitrailer = (Semitrailer)res;
                    SelectedItem.SemitrailerId = res.Id;

                    Logger.Log.Info($"Полуприцеп поставлен на машину (id: {SelectedItem.Id})");
                }

                RaisePropertyChanged(nameof(SelectedItem));
            }
        }
    }
}
