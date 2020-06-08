using Diplom.Dialogs.Attributes;
using Diplom.Dialogs.Classes;
using Diplom.Models;
using Diplom.Provider;
using MaterialDesignXaml.DialogsHelper;

namespace Diplom.Dialogs
{
    /// <summary>
    /// Driver selector view model.
    /// </summary>
    [DialogName(nameof(DriverSelectorView))]
    class DriverSelectorViewModel : BaseSelectorViewModel<Driver>
    { 
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public DriverSelectorViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DriverSelectorViewModel(int currentId, IDialogIdentifier dialogIdentifier, IDataProvider dataProvider) 
            : base(dialogIdentifier, dataProvider.Drivers, currentId)
        {
        }
    }
}
