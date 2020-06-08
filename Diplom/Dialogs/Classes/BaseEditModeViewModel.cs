using DevExpress.Mvvm;
using Diplom.Dialogs.Interfaces;
using Diplom.ViewModels;
using DryIoc;
using MaterialDesignXaml.DialogsHelper;
using System;
using System.Windows.Input;

namespace Diplom.Dialogs.Classes
{
    /// <summary>
    /// Base edit mode view model.
    /// </summary>
    class BaseEditModeViewModel<T> : ViewModelBase, IClosableDialog, IDialogIdentifier, IEditMode<T> where T : ValidateViewModel, ICloneable
    {
        /// <summary>
        /// True - Режим редактирования, False - добавление.
        /// </summary>
        public bool IsEditMode { get; }

        /// <summary>
        /// Объект для редактирования.
        /// </summary>
        public T EditableObject { get; }

        /// <summary>
        /// Identifier.
        /// </summary>
        public string Identifier => nameof(BaseEditModeViewModel<T>);

        /// <summary>
        /// Owner.
        /// </summary>
        public IDialogIdentifier OwnerIdentifier { get; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public BaseEditModeViewModel()
        {
            EditableObject = Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public BaseEditModeViewModel(T obj, bool isEditMode, IContainer container)
        {
            IsEditMode = isEditMode;
            OwnerIdentifier = container.ResolveRootDialogIdentifier();

            if (isEditMode)
                EditableObject = (T)obj.Clone();
            else EditableObject = container.Resolve<T>();

            CloseDialogCommand = new DelegateCommand(CloseDialog, IsObjectValid);
        }

        /// <summary>
        /// Указывает, валидный ли редактируемый объект или нет.
        /// </summary>
        /// <returns></returns>
        private bool IsObjectValid()
        {
            return EditableObject.IsValid;
        }

        /// <summary>
        /// Команда закрытия диалога.
        /// </summary>
        public ICommand CloseDialogCommand { get; }

        /// <summary>
        /// Закрытие диалога.
        /// </summary>
        private void CloseDialog()
        {
            OwnerIdentifier.Close(EditableObject);
        }
    }
}
