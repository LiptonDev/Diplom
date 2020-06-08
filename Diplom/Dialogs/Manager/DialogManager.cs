using Diplom.Models;
using DryIoc;
using MaterialDesignXaml.DialogsHelper;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace Diplom.Dialogs.Manager
{
    /// <summary>
    /// Менеджер диалогов.
    /// </summary>
    class DialogManager : IDialogManager
    {
        /// <summary>
        /// Идентификатор диалоговых окон.
        /// </summary>
        readonly IDialogIdentifier dialogIdentifier;

        /// <summary>
        /// Контейнер.
        /// </summary>
        readonly IContainer container;

        /// <summary>
        /// View factory.
        /// </summary>
        readonly IDialogsFactoryView viewFactory;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DialogManager(IContainer container, IDialogsFactoryView viewFactory)
        {
            this.dialogIdentifier = container.ResolveRootDialogIdentifier();
            this.container = container;
            this.viewFactory = viewFactory;
        }

        /// <summary>
        /// Открыть диалог.
        /// </summary>
        /// <param name="args">Аргументы для VM.</param>
        /// <param name="dialogIdentifier">Идентификатор, где будет показан диалог.</param>
        /// <returns></returns>
        async Task<T> show<T, VM>(object[] args = null, IDialogIdentifier dialogIdentifier = null)
        {
            var vm = container.Resolve<VM>(args: args);
            var view = viewFactory.GetView(vm);

            dialogIdentifier = dialogIdentifier ?? this.dialogIdentifier;

            Logger.Log.Info($"Попытка показать диалог: {{view: {view}, viewmodel: {typeof(VM)}}}");

            var res = await dialogIdentifier.ShowAsync<T>(view);

            return res;
        }

        /// <summary>
        /// Окно выбора машины.
        /// </summary>
        /// <returns></returns>
        public Task<Truck> SelectTruck(int currentId)
        {
            return show<Truck, TruckSelectorViewModel>(new object[] { currentId, dialogIdentifier });
        }

        /// <summary>
        /// Окно редактирования маршрута.
        /// </summary>
        /// <returns></returns>
        public Task<Route> EditRoute(Route route, bool isEditMode)
        {
            return show<Route, RouteEditorViewModel>(new object[] { route, isEditMode });
        }

        /// <summary>
        /// Окно выбора мед. работника, механика и диспетчера для путевого листа.
        /// </summary>
        /// <returns></returns>
        public Task<StaffSelectorResult> SelectStaffForWaybill()
        {
            return show<StaffSelectorResult, WaybillStaffSelectorViewModel>(new object[] { dialogIdentifier });
        }

        /// <summary>
        /// Показать окно настроек.
        /// </summary>
        public async void ShowProgramSettings()
        {
            var vm = container.Resolve<ProgramSettingsViewModel>();
            var view = viewFactory.GetView(vm);

            await dialogIdentifier.ShowAsync(view);
        }

        /// <summary>
        /// Получить сохранения файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns></returns>
        public string GetSaveFileName(string fileName)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Сохранение путевого листа",
                Filter = "Excel files|*.xlsx",
                FileName = fileName,
            };

            if (sfd.ShowDialog() == true)
            {
                return sfd.FileName;
            }
            else return string.Empty;
        }

        /// <summary>
        /// Окно редактирования сотрудника.
        /// </summary>
        /// <returns></returns>
        public Task<Staff> EditStaff(Staff staff, bool isEditMode)
        {
            return show<Staff, StaffEditorViewModel>(new object[] { staff, isEditMode });
        }

        /// <summary>
        /// Окно редактирования водителя.
        /// </summary>
        /// <returns></returns>
        public Task<Truck> EditTruck(Truck truck, bool isEditMode)
        {
            return show<Truck, TruckEditorViewModel>(new object[] { truck, isEditMode });
        }

        /// <summary>
        /// Окно редактирования полуприцепа.
        /// </summary>
        /// <returns></returns>
        public Task<Semitrailer> EditSemitrail(Semitrailer semitrail, bool isEditMode)
        {
            return show<Semitrailer, SemitrailerEditorViewModel>(new object[] { semitrail, isEditMode });
        }

        /// <summary>
        /// Окно редактирования водителя.
        /// </summary>
        /// <returns></returns>
        public Task<Driver> EditDriver(Driver driver, bool isEditMode)
        {
            return show<Driver, DriverEditorViewModel>(new object[] { driver, isEditMode });
        }

        /// <summary>
        /// Окно выбора полуприцепа.
        /// </summary>
        /// <returns></returns>
        public Task<Semitrailer> SelectSemitrail(int currentId)
        {
            return show<Semitrailer, SemitrailerSelectorViewModel>(new object[] { currentId, dialogIdentifier });
        }

        /// <summary>
        /// Окно выбора водителя.
        /// </summary>
        /// <returns></returns>
        public Task<Driver> SelectDriver(int currentId)
        {
            return show<Driver, DriverSelectorViewModel>(new object[] { currentId, dialogIdentifier });
        }
    }
}
