namespace Diplom.Dialogs.Manager
{
    /// <summary>
    /// Dialogs view factory.
    /// </summary>
    interface IDialogsFactoryView
    {
        /// <summary>
        /// Получить view.
        /// </summary>
        /// <returns></returns>
        object GetView<TViewModel>(TViewModel viewModel);
    }
}
