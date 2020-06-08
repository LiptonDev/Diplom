using System.Windows;

namespace Diplom.Dialogs.Manager
{
    /// <summary>
    /// Менеджер диалоговых окон.
    /// </summary>
    interface IWindowsManager
    {
        /// <summary>
        /// Показать основную форму с данными.
        /// </summary>
        /// <param name="sender">Форма, отправившая запрос на открытие формы.</param>
        void ShowMainWindow(Window sender);
    }
}
