using System.Windows.Input;

namespace Diplom.ViewModels
{
    /// <summary>
    /// Указывает на то, что View Model может экспортировать данные.
    /// </summary>
    interface IExporterCommand
    {
        /// <summary>
        /// Команда экспорта.
        /// </summary>
        ICommand ExportCommand { get; }
    }
}
