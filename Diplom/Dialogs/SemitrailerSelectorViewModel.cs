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
    [DialogName(nameof(SemitrailerSelectorView))]
    class SemitrailerSelectorViewModel : BaseSelectorViewModel<Semitrailer>
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public SemitrailerSelectorViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public SemitrailerSelectorViewModel(int currentId, IDialogIdentifier dialogIdentifier, IDataProvider dataProvider)
            : base(dialogIdentifier, dataProvider.Semitrailers, currentId)
        {
        }
    }
}
