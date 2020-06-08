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
    /// Экспорт водителей.
    /// </summary>
    class DriversExporter : BaseExporter, IExporter<IEnumerable<Driver>>
    {
        /// <summary>
        /// Экспорт.
        /// </summary>
        /// <returns></returns>
        public bool Export(IEnumerable<Driver> drivers)
        {
            if (!SelectFile("Водители"))
                return false;

            if (!drivers.Any())
                return false;

            using (var excel = new ExcelPackage())
            {
                try
                {
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add($"Водители на {DateTime.Now.ToShortDateString()}");

                    worksheet.Cells["A1"].SetValue("ФИО");
                    worksheet.Cells["B1"].SetValue("Номер телефона");
                    worksheet.Cells["C1"].SetValue("Номер В/У");
                    worksheet.Cells["D1"].SetValue("Домашний адрес");

                    worksheet.Row(1).Height = 40;

                    worksheet.Column(2).Style.Numberformat.Format = "#(###)-###-##-##";
                    worksheet.Column(3).Style.Numberformat.Format = "## ## ######";

                    int row = 2;

                    foreach (var item in drivers)
                    {
                        worksheet.Cells[row, 1].SetValue(item);
                        worksheet.Cells[row, 2].SetValue(item.DriverLicense);

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
