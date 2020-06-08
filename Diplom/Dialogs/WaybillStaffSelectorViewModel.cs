using DevExpress.Mvvm;
using Diplom.Dialogs.Attributes;
using Diplom.Dialogs.Classes;
using Diplom.Models;
using Diplom.Properties;
using Diplom.Provider;
using MaterialDesignXaml.DialogsHelper;
using System.Windows.Input;

namespace Diplom.Dialogs
{
    /// <summary>
    /// WaybillStaffSelector view mode.
    /// </summary>
    [DialogName(nameof(WaybillStaffSelectorView))]
    class WaybillStaffSelectorViewModel : IClosableDialog
    {
        /// <summary>
        /// Выбор диспетчера.
        /// </summary>
        public BaseSelectorViewModel<Staff> DispatcherSelector { get; }

        /// <summary>
        /// Выбор мед. работника.
        /// </summary>
        public BaseSelectorViewModel<Staff> MedicalSelector { get; }

        /// <summary>
        /// Выбор механика.
        /// </summary>
        public BaseSelectorViewModel<Staff> MechanicSelector { get; }

        /// <summary>
        /// Owner;
        /// </summary>
        public IDialogIdentifier OwnerIdentifier { get; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public WaybillStaffSelectorViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public WaybillStaffSelectorViewModel(IDialogIdentifier dialogIdentifier, IDataProvider dataProvider)
        {
            OwnerIdentifier = dialogIdentifier;

            var settings = Settings.Default;

            DispatcherSelector = new BaseSelectorViewModel<Staff>(dialogIdentifier, dataProvider.Staff, settings.lastDispatcher);
            MedicalSelector = new BaseSelectorViewModel<Staff>(dialogIdentifier, dataProvider.Staff, settings.lastMed);
            MechanicSelector = new BaseSelectorViewModel<Staff>(dialogIdentifier, dataProvider.Staff, settings.lastMechanic);

            CloseDialogCommand = new DelegateCommand(CloseDialog, () => DispatcherSelector.CloseDialogCommand.CanExecute(null)
                                                                        && MedicalSelector.CloseDialogCommand.CanExecute(null)
                                                                        && MechanicSelector.CloseDialogCommand.CanExecute(null));
        }

        /// <summary>
        /// Команда закрытия окна.
        /// </summary>
        public ICommand CloseDialogCommand { get; }

        /// <summary>
        /// Закрыть окно.
        /// </summary>
        private void CloseDialog()
        {
            var settings = Settings.Default;
            settings.lastDispatcher = DispatcherSelector.SelectedItem.Id;
            settings.lastMed = MedicalSelector.SelectedItem.Id;
            settings.lastMechanic = MechanicSelector.SelectedItem.Id;

            settings.Save();

            OwnerIdentifier.Close(new StaffSelectorResult(MedicalSelector.SelectedItem, MechanicSelector.SelectedItem, DispatcherSelector.SelectedItem));
        }
    }
}
