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
    /// Экспорт полуприцепов.
    /// </summary>
    class SemitrailersExporter : BaseExporter, IExporter<IEnumerable<Semitrailer>>
    {
        /// <summary>
        /// Экспорт.
        /// </summary>
        /// <returns></returns>
        public bool Export(IEnumerable<Semitrailer> semitrailers)
        {
            if (!SelectFile("Полуприцепы"))
                return false;

            if (!semitrailers.Any())
                return false;

            using (var excel = new ExcelPackage())
            {
                try
                {
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add($"Полуприцепы на {DateTime.Now.ToShortDateString()}");

                    worksheet.Cells["A1"].SetValue("Модель");
                    worksheet.Cells["B1"].SetValue("Номер");

                    worksheet.Row(1).Height = 40;

                    int row = 2;

                    foreach (var item in semitrailers)
                    {
                        worksheet.Cells[row, 1].SetValue(item.Model);
                        worksheet.Cells[row, 2].SetValue(item);

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
