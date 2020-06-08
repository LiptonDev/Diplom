using System;
using System.Globalization;
using System.Windows.Data;

namespace Diplom.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    class RouteStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool status = (bool)value;

            return status ? "Завершен" : "Активный";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
