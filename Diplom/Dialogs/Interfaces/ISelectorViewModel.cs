
using System.Collections.ObjectModel;

namespace Diplom.Dialogs.Interfaces
{
    /// <summary>
    /// Указывает, что ViewModel явялется моделью выбора данных.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface ISelectorViewModel<T>
    {
        /// <summary>
        /// Все данные.
        /// </summary>
        ObservableCollection<T> Items { get; }

        /// <summary>
        /// Выбранные данные.
        /// </summary>
        T SelectedItem { get; set; }
    }
}
