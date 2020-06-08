using Diplom.Excel.Classes;
using Diplom.Excel.Interfaces;
using Diplom.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diplom.Excel
{
    /// <summary>
    /// Экспорт сотрудников.
    /// </summary>
    class StaffExporter : BaseExporter, IExporter<IEnumerable<Staff>>
    {
        /// <summary>
        /// Экспорт.
        /// </summary>
        /// <returns></returns>
        public bool Export(IEnumerable<Staff> staff)
        {
            if (!SelectFile("Сотрудники"))
                return false;

            if (!staff.Any())
                return false;

            using (var excel = new ExcelPackage())
            {
                try
                {
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add($"Сотрудники на {DateTime.Now.ToShortDateString()}");

                    worksheet.Cells["A1"].SetValue("ФИО");
                    worksheet.Cells["B1"].SetValue("Должность");

                    worksheet.Row(1).Height = 40;

                    int row = 2;

                    foreach (var item in staff)
                    {
                        worksheet.Cells[row, 1].SetValue(item);
                        worksheet.Cells[row, 2].SetValue(item.Position);

                        worksheet.Row(row).Height = 25;

                        row++;
                    }

                    worksheet.Cells[1, 1, row - 1, 2].SetVerticalHorizontalAligment();
                    worksheet.Cells[1, 1, row - 1, 2].SetTable();

                    worksheet.Cells.SetFontName("Arial").SetFontSize(12);

                    worksheet.Cells.AutoFitColumns(1);

                    base.Save(excel);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
