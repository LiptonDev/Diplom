using Diplom.Dialogs.Attributes;
using Diplom.Dialogs.Classes;
using Diplom.Models;
using Diplom.Provider;
using MaterialDesignXaml.DialogsHelper;

namespace Diplom.Dialogs
{
    /// <summary>
    /// TruckSelector view model.
    /// </summary>
    [DialogName(nameof(TruckSelectorView))]
    class TruckSelectorViewModel : BaseSelectorViewModel<Truck>
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public TruckSelectorViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TruckSelectorViewModel(int currentId, IDialogIdentifier dialogIdentifier, IDataProvider dataProvider)
            : base(dialogIdentifier, dataProvider.Trucks, currentId)
        {
        }
    }
}
