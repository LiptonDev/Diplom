namespace Diplom.Models
{
    /// <summary>
    /// Выбор сотрудников для создания путевого листа.
    /// </summary>
    class StaffSelectorResult
    {
        /// <summary>
        /// Мед. работник.
        /// </summary>
        public Staff Medical { get; }

        /// <summary>
        /// Механик.
        /// </summary>
        public Staff Mechanic { get; }

        /// <summary>
        /// Диспетчер.
        /// </summary>
        public Staff Dispatcher { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public StaffSelectorResult(Staff medical, Staff mechanic, Staff dispatcher)
        {
            Medical = medical;
            Mechanic = mechanic;
            Dispatcher = dispatcher;
        }
    }
}
