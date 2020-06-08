using DevExpress.Mvvm;
using Diplom.DataBase.Interfaces;
using Diplom.Dialogs.Manager;
using Diplom.Models;
using Diplom.Properties;
using MaterialDesignXaml.DialogsHelper;
using MaterialDesignXaml.DialogsHelper.Enums;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diplom.ViewModels
{
    /// <summary>
    /// LoginWindow view model.
    /// </summary>
    class LoginViewModel : IDialogIdentifier
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Identifier => nameof(LoginViewModel);

        /// <summary>
        /// Пользователь для авторизации.
        /// </summary>
        public LoginUser LoginUser { get; }

        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly IContext context;

        /// <summary>
        /// Менеджер окон.
        /// </summary>
        private readonly IWindowsManager windowsManager;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public LoginViewModel()
        {
            LoginUser = new LoginUser();
#if DEBUG
            LoginUser.Login = "root";
            LoginUser.Password = "root";
#else
            LoginUser.Login = Settings.Default.lastLogin;
            LoginUser.Password = Settings.Default.lastPassword;
#endif
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public LoginViewModel(IContext context, IWindowsManager windowsManager) : this()
        {
            this.context = context;
            this.windowsManager = windowsManager;

            TryLoginCommand = new AsyncCommand(TryLogin, () => LoginUser.IsValid);
        }

        /// <summary>
        /// Команда авторизации.
        /// </summary>
        public ICommand TryLoginCommand { get; }

        /// <summary>
        /// Авторизация.
        /// </summary>
        /// <returns></returns>
        private async Task TryLogin()
        {
            var res = await context.Users.LoginAsync(LoginUser);

            if (!res)
                return;

            if (!res.Result)
            {
                await this.ShowMessageBoxAsync($"Проверьте логин или пароль", MaterialMessageBoxButtons.Ok);
                return;
            }
            else
            {
                Settings.Default.lastLogin = LoginUser.Login;
                Settings.Default.lastPassword = LoginUser.Password;
                Settings.Default.Save();

                windowsManager.ShowMainWindow(App.Current.MainWindow);

                Logger.Log.Info("Успешный вход");
            }
        }
    }
}
