using Diplom.Converters;
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
    /// Экспорт маршрутов.
    /// </summary>
    class RoutesExporter : BaseExporter, IExporter<IEnumerable<Route>>
    {
        static RouteStatusConverter routeStatusConverter = new RouteStatusConverter();

        /// <summary>
        /// Экспорт.
        /// </summary>
        /// <returns></returns>
        public bool Export(IEnumerable<Route> routes)
        {
            if (!SelectFile("Маршруты"))
                return false;

            if (!routes.Any())
                return false;

            using (var excel = new ExcelPackage())
            {
                try
                {
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add($"Маршруты на {DateTime.Now.ToShortDateString()}");

                    worksheet.Cells["A1"].SetValue("Место отправления");
                    worksheet.Cells["B1"].SetValue("Место назначения");
                    worksheet.Cells["C1"].SetValue("Описание");
                    worksheet.Cells["D1"].SetValue("Статус");
                    worksheet.Cells["E1"].SetValue("Модель машины");
                    worksheet.Cells["F1"].SetValue("Номер машины");

                    worksheet.Row(1).Height = 40;

                    int row = 2;

                    foreach (var item in routes)
                    {
                        worksheet.Cells[row, 1].SetValue(item.From);
                        worksheet.Cells[row, 2].SetValue(item.To);
                        worksheet.Cells[row, 3].SetValue(item.Description);
                        worksheet.Cells[row, 4].SetValue(routeStatusConverter.Convert(item.Status, null, null, null));
                        if (item.TruckId.HasValue)
                        {
                            worksheet.Cells[row, 5].SetValue(item.Truck.CarModel);
                            worksheet.Cells[row, 6].SetValue($"{item.Truck.CarNumber} | {item.Truck.CarNumberRegion}");
                        }

                        worksheet.Row(row).Height = 25;

                        row++;
                    }

                    worksheet.Cells[1, 1, row - 1, 6].SetVerticalHorizontalAligment();
                    worksheet.Cells[1, 1, row - 1, 6].SetTable();

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
