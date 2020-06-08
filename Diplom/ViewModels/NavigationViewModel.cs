using DevExpress.Mvvm;
using Diplom.DataBase.Interfaces;
using Diplom.Dialogs.Manager;
using DryIoc;
using MaterialDesignThemes.Wpf;
using MaterialDesignXaml.DialogsHelper;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diplom.ViewModels
{
    /// <summary>
    /// ViewModel for navigation ViewModels.
    /// </summary>
    abstract class NavigationViewModel<T> : ViewModelBase, INavigationAware
    {
        /// <summary>
        /// Объекты.
        /// </summary>
        public ObservableCollection<T> Items { get; protected set; }

        /// <summary>
        /// Выбранный объект.
        /// </summary>
        public T SelectedItem { get; set; }

        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        protected readonly IContext context;

        /// <summary>
        /// Идентификатор диалогов.
        /// </summary>
        protected readonly IDialogIdentifier dialogIdentifier;

        /// <summary>
        /// Очередь сообщений.
        /// </summary>
        protected readonly ISnackbarMessageQueue messageQueue;

        /// <summary>
        /// Менеджер диалоговых окон.
        /// </summary>
        protected readonly IDialogManager dialogManager;

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public NavigationViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public NavigationViewModel(IContext context, IDialogManager dialogManager, ISnackbarMessageQueue messageQueue, IContainer container)
        {
            this.context = context;
            this.messageQueue = messageQueue;
            this.dialogManager = dialogManager;
            dialogIdentifier = container.ResolveRootDialogIdentifier();

            EditCommand = new AsyncCommand<T>(Edit, x => x != null);
            DeleteCommand = new AsyncCommand<T>(Delete, x => x != null);
            AddCommand = new AsyncCommand(Add);
        }

        /// <summary>
        /// Метод редактирования объекта.
        /// </summary>
        /// <returns></returns>
        protected abstract Task Edit(T item);

        /// <summary>
        /// Метод удаления объекта.
        /// </summary>
        /// <returns></returns>
        protected abstract Task Delete(T item);

        /// <summary>
        /// Метод добавления объекта.
        /// </summary>
        /// <returns></returns>
        protected abstract Task Add();

        /// <summary>
        /// Команда редактирования.
        /// </summary>
        public ICommand<T> EditCommand { get; }

        /// <summary>
        /// Команда удаления.
        /// </summary>
        public ICommand<T> DeleteCommand { get; }

        /// <summary>
        /// Команда добавления.
        /// </summary>
        public ICommand AddCommand { get; }

        /// <summary>
        /// Вызывается при переходе на страницу.
        /// </summary>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        #region INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        #endregion
    }
}
