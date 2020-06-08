using Diplom.Dialogs.Attributes;
using Diplom.Dialogs.Classes;
using Diplom.Models;
using DryIoc;

namespace Diplom.Dialogs
{
    /// <summary>
    /// Driver editor view model.
    /// </summary>
    [DialogName(nameof(TruckEditorView))]
    class TruckEditorViewModel : BaseEditModeViewModel<Truck>
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public TruckEditorViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TruckEditorViewModel(Truck truck, bool isEditMode, IContainer container) : base(truck, isEditMode, container)
        {

        }
    }
}
