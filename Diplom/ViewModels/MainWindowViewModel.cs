using DevExpress.Mvvm;
using Diplom.Dialogs.Manager;
using Diplom.Provider;
using DryIoc;
using MaterialDesignThemes.Wpf;
using MaterialDesignXaml.DialogsHelper;
using Prism.Regions;
using System.Windows.Input;

namespace Diplom.ViewModels
{
    /// <summary>
    /// MainWindow view model.
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Главное меню (открыто/закрыто).
        /// </summary>
        public bool MenuIsOpen { get; set; }

        /// <summary>
        /// Идентификатор диалоговых окон.
        /// </summary>
        public IDialogIdentifier DialogIdentifier { get; }

        /// <summary>
        /// Очередь сообщений.
        /// </summary>
        public ISnackbarMessageQueue MessageQueue { get; }

        /// <summary>
        /// Менеджер регионов.
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Менеджер диалогов.
        /// </summary>
        private readonly IDialogManager dialogManager;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public MainWindowViewModel()
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MainWindowViewModel(ISnackbarMessageQueue messageQueue, IRegionManager regionManager, IDialogManager dialogManager, IDataProvider dataProvider, IContainer container)
        {
            DialogIdentifier = container.ResolveRootDialogIdentifier();
            MessageQueue = messageQueue;
            this.regionManager = regionManager;
            this.dialogManager = dialogManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
            ShowProgramSettingsCommand = new DelegateCommand(ShowProgramSettings);
            UpdateDataCommand = new DelegateCommand(dataProvider.LoadAsync);
        }

        /// <summary>
        /// Команда перехода на другую страницу.
        /// </summary>
        public ICommand<string> NavigateCommand { get; }

        /// <summary>
        /// Команда открытия окна настроек.
        /// </summary>
        public ICommand ShowProgramSettingsCommand { get; }

        /// <summary>
        /// Обновить данные.
        /// </summary>
        public ICommand UpdateDataCommand { get; }

        /// <summary>
        /// Открыть окно настроек.
        /// </summary>
        private void ShowProgramSettings()
        {
            dialogManager.ShowProgramSettings();
        }

        /// <summary>
        /// Переход на другую страницу.
        /// </summary>
        /// <param name="view">Страница.</param>
        private void Navigate(string view)
        {
            regionManager.RequestNavigateInRootRegion(view);

            MenuIsOpen = false;

            Logger.Log.Info($"Navigated to \"{view}\"");
        }
    }
}
