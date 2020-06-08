using Diplom.Dialogs.Attributes;
using Diplom.Dialogs.Classes;
using Diplom.Models;
using DryIoc;

namespace Diplom.Dialogs
{
    /// <summary>
    /// StaffEditor view model.
    /// </summary>
    [DialogName(nameof(StaffEditorView))]
    class StaffEditorViewModel : BaseEditModeViewModel<Staff>
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public StaffEditorViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public StaffEditorViewModel(Staff staff, bool isEditMode, IContainer container) : base(staff, isEditMode, container)
        {

        }
    }
}
