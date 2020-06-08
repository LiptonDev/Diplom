using DevExpress.Mvvm;
using Diplom.Dialogs.Interfaces;
using Diplom.Models;
using MaterialDesignXaml.DialogsHelper;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Diplom.Dialogs.Classes
{
    /// <summary>
    /// Selector view model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BaseSelectorViewModel<T> : ViewModelBase, IClosableDialog, ISelectorViewModel<T> where T : BaseId
    {
        /// <summary>
        /// Владелец диалогового окна.
        /// </summary>
        public IDialogIdentifier OwnerIdentifier { get; }

        /// <summary>
        /// Все данные.
        /// </summary>
        public ObservableCollection<T> Items { get; }

        /// <summary>
        /// Выбранные данные.
        /// </summary>
        public T SelectedItem { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public BaseSelectorViewModel()
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public BaseSelectorViewModel(IDialogIdentifier owner, ObservableCollection<T> items, int currentId)
        {
            OwnerIdentifier = owner;

            Items = items;

            SelectedItem = Items.FirstOrDefault(x => x.Id == currentId);

            CloseDialogCommand = new DelegateCommand(CloseDialog, () => SelectedItem != null);
        }

        /// <summary>
        /// Команда закрытия диалогового окна.
        /// </summary>
        public ICommand CloseDialogCommand { get; }

        /// <summary>
        /// Закрытие диалогового окна.
        /// </summary>
        private void CloseDialog()
        {
            this.Close(SelectedItem);
        }
    }
}
