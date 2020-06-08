using Diplom.Dialogs.Attributes;
using Diplom.Dialogs.Classes;
using Diplom.Models;
using DryIoc;

namespace Diplom.Dialogs
{
    /// <summary>
    /// Semitrail editor view model.
    /// </summary>
    [DialogName(nameof(SemitrailEditorView))]
    class SemitrailerEditorViewModel : BaseEditModeViewModel<Semitrailer>
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public SemitrailerEditorViewModel()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public SemitrailerEditorViewModel(Semitrailer semitrail, bool isEditMode, IContainer container) : base(semitrail, isEditMode, container)
        {

        }
    }
}
