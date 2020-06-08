using Diplom.Dialogs.Attributes;
using Diplom.Dialogs.Classes;
using Diplom.Models;
using DryIoc;

namespace Diplom.Dialogs
{
    /// <summary>
    /// Driver editor view model.
    /// </summary>
    [DialogName(nameof(DriverEditorView))]
    class DriverEditorViewModel : BaseEditModeViewModel<Driver>
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public DriverEditorViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DriverEditorViewModel(Driver driver, bool isEditMode, IContainer container) : base(driver, isEditMode, container)
        {

        }
    }
}
