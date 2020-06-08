using Diplom.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Diplom.Converters
{
    class TruckSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var truck = value as Truck;

            var truckSelected = truck != null;
            var semitrail = truck?.SemitrailerId.HasValue;

            return truckSelected && semitrail.HasValue && semitrail.Value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
