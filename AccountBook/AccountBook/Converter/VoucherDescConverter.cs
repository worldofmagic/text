using System;
using System.Windows.Data;
using System.Globalization;

namespace AccountBook.Converter
{
    public class VoucherDescConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().Length > 6)
            {
                return value.ToString().Substring(0, 5) + "…";
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
