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
    /// Экспорт машин.
    /// </summary>
    class TrucksExporter : BaseExporter, IExporter<IEnumerable<Truck>>
    {
        /// <summary>
        /// Экспорт.
        /// </summary>
        /// <returns></returns>
        public bool Export(IEnumerable<Truck> trucks)
        {
            if (!SelectFile("Автомобили"))
                return false;

            if (!trucks.Any())
                return false;

            using (var excel = new ExcelPackage())
            {
                try
                {
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add($"Автомобили на {DateTime.Now.ToShortDateString()}");

                    worksheet.Cells["A1"].SetValue("Модель");
                    worksheet.Cells["B1"].SetValue("Номер");
                    worksheet.Cells["C1"].SetValue("П/П");
                    worksheet.Cells["D1"].SetValue("Номер П/П");

                    worksheet.Row(1).Height = 40;

                    int row = 2;

                    foreach (var item in trucks)
                    {
                        worksheet.Cells[row, 1].SetValue(item.CarModel);
                        worksheet.Cells[row, 2].SetValue($"{item.CarNumber} | {item.CarNumberRegion}");

                        string model = "Нет", number = "Нет";
                        if (item.SemitrailerId.HasValue)
                        {
                            model = item.Semitrailer.Model;
                            number = $"{item.Semitrailer.SemitrailerNumber} | {item.Semitrailer.SemitrailerNumberRegion}";
                        }

                        worksheet.Cells[row, 3].SetValue(model);
                        worksheet.Cells[row, 4].SetValue(number);

                        worksheet.Row(row).Height = 25;

                        row++;
                    }

                    worksheet.Cells[1, 1, row - 1, 4].SetVerticalHorizontalAligment();
                    worksheet.Cells[1, 1, row - 1, 4].SetTable();

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
