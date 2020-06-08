using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.IO;

namespace Diplom.Excel.Classes
{
    /// <summary>
    /// Базовые функции экспортера.
    /// </summary>
    class BaseExporter
    {
        /// <summary>
        /// Название файла для экспорта.
        /// </summary>
        string fileName;

        /// <summary>
        /// Сохранить документ.
        /// </summary>
        /// <param name="excel">Документ.</param>
        protected void Save(ExcelPackage excel)
        {
            try
            {
                excel.SaveAs(new FileInfo(fileName));

                Logger.Log.Info($"Сохранен Excel документ \"{fileName}\"");
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"Сохранение Excel документа \"{fileName}\"", ex);
            }
        }

        /// <summary>
        /// Выбор файла для экспорта.
        /// </summary>
        /// <param name="defName">Стандартное название файла.</param>
        /// <returns></returns>
        public bool SelectFile(string defName)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Экспорт данных",
                Filter = "Excel files|*.xlsx;*xls",
                FileName = defName
            };

            if (sfd.ShowDialog() == true)
            {
                fileName = sfd.FileName;
                return true;
            }
            else return false;
        }
    }
}
