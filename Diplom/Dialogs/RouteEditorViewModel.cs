using Diplom.Dialogs.Attributes;
using Diplom.Dialogs.Classes;
using Diplom.Models;
using DryIoc;

namespace Diplom.Dialogs
{
    /// <summary>
    /// RouteEditor view model.
    /// </summary>
    [DialogName(nameof(RouteEditorView))]
    class RouteEditorViewModel : BaseEditModeViewModel<Route>
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public RouteEditorViewModel()
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public RouteEditorViewModel(Route route, bool isEditMode, IContainer container) : base(route, isEditMode, container)
        {

        }
    }
}
