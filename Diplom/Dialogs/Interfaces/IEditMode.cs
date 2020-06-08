using Diplom.ViewModels;
using System;

namespace Diplom.Dialogs.Interfaces
{
    /// <summary>
    /// Указывает, что ViewModel - модель редактирования объекта.
    /// </summary>
    interface IEditMode<T> where T : ValidateViewModel, ICloneable
    {
        /// <summary>
        /// True - режим редактирования, false - добавления.
        /// </summary>
        bool IsEditMode { get; }

        /// <summary>
        /// Объект для редактирования.
        /// </summary>
        T EditableObject { get; }
    }
}
