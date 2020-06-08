using Diplom.Provider;
using DryIoc;
using System.Windows;

namespace Diplom.Dialogs.Manager
{
    /// <summary>
    /// Менеджер диалоговых окон.
    /// </summary>
    class WindowsManager : IWindowsManager
    {
        /// <summary>
        /// Контейнер.
        /// </summary>
        private readonly IContainer container;

        /// <summary>
        /// Провайдер данных.
        /// </summary>
        private readonly IDataProvider dataProvider;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public WindowsManager(IDataProvider dataProvider, IContainer container)
        {
            this.container = container;
            this.dataProvider = dataProvider;
        }

        /// <summary>
        /// Показать основную форму с данными.
        /// </summary>
        /// <param name="sender">Форма, отправившая запрос на открытие формы.</param>
        public void ShowMainWindow(Window sender)
        {
            sender.Hide();

            dataProvider.LoadAsync();

            Window main = container.Resolve<MainWindow>();
            main.ShowDialog();

            sender.Close();
        }
    }
}
