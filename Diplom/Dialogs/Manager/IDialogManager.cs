using Diplom.Models;
using System.Threading.Tasks;

namespace Diplom.Dialogs.Manager
{
    /// <summary>
    /// Менеджер диалогов.
    /// </summary>
    interface IDialogManager
    {
        /// <summary>
        /// Окно выбора машины.
        /// </summary>
        /// <returns></returns>
        Task<Truck> SelectTruck(int currentId);

        /// <summary>
        /// Окно редактирования маршрута.
        /// </summary>
        /// <returns></returns>
        Task<Route> EditRoute(Route route, bool isEditMode);

        /// <summary>
        /// Окно выбора мед. работника, механика и диспетчера для путевого листа.
        /// </summary>
        /// <returns></returns>
        Task<StaffSelectorResult> SelectStaffForWaybill();

        /// <summary>
        /// Показать окно настроек.
        /// </summary>
        void ShowProgramSettings();

        /// <summary>
        /// Получить сохранения файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns></returns>
        string GetSaveFileName(string fileName);

        /// <summary>
        /// Окно редактирования сотрудника.
        /// </summary>
        /// <returns></returns>
        Task<Staff> EditStaff(Staff staff, bool isEditMode);

        /// <summary>
        /// Окно редактирования машины.
        /// </summary>
        /// <returns></returns>
        Task<Truck> EditTruck(Truck driver, bool isEditMode);

        /// <summary>
        /// Окно редактирования полуприцепа.
        /// </summary>
        /// <returns></returns>
        Task<Semitrailer> EditSemitrail(Semitrailer semitrail, bool isEditMode);

        /// <summary>
        /// Окно редактирования водителя.
        /// </summary>
        /// <returns></returns>
        Task<Driver> EditDriver(Driver driver, bool isEditMode);

        /// <summary>
        /// Окно выбора водителя.
        /// </summary>
        /// <returns></returns>
        Task<Driver> SelectDriver(int currentId);

        /// <summary>
        /// Окно выбора полуприцепа.
        /// </summary>
        /// <returns></returns>
        Task<Semitrailer> SelectSemitrail(int currentId);
    }
}
