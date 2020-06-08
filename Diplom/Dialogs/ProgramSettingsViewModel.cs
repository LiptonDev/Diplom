using DevExpress.Mvvm;
using Diplom.Dialogs.Attributes;
using Diplom.Properties;
using DryIoc;
using MaterialDesignXaml.DialogsHelper;
using System.Windows.Input;

namespace Diplom.Dialogs
{
    /// <summary>
    /// ProgramSettings view model.
    /// </summary>
    [DialogName(nameof(ProgramSettingsView))]
    class ProgramSettingsViewModel : IClosableDialog
    {
        /// <summary>
        /// Настройки.
        /// </summary>
        public Settings Settings { get; }

        /// <summary>
        /// Владелец окна.
        /// </summary>
        public IDialogIdentifier OwnerIdentifier { get; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public ProgramSettingsViewModel()
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ProgramSettingsViewModel(IContainer container)
        {
            OwnerIdentifier = container.ResolveRootDialogIdentifier();

            Settings = Settings.Default;

            CloseDialogCommand = new DelegateCommand(CloseDialog);
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
            Settings.Default.Save();

            OwnerIdentifier.Close();
        }
    }
}
