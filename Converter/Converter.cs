using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BusinessManager.Converter
{
    public class Converter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Все проверки для краткости выкинул
            return (decimal?)value <= 0 ?
                new SolidColorBrush(Colors.OrangeRed)
                : new SolidColorBrush(Colors.Green);
        }
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
    

    }

