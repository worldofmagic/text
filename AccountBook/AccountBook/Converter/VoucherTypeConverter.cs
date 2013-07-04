using System;
using System.Windows.Data;
using System.Globalization;

namespace AccountBook.Converter
{
    public class VoucherTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (short.Parse(value.ToString()) == 0)
            {
                return "收入";
            }
            else
            {
                return "支出";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
