namespace Diplom.Excel.Interfaces
{
    /// <summary>
    /// Интерфейс экспортера.
    /// </summary>
    interface IExporter<T>
    {
        /// <summary>
        /// Экспорт данных.
        /// </summary>
        /// <param name="value">Данные для экспорта.</param>
        /// <returns></returns>
        bool Export(T value);
    }
}
