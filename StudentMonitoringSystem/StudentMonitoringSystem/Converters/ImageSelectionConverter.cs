using System;
using System.Globalization;
using Xamarin.Forms;

namespace StudentMonitoringSystem.Converters
{
    public class ImageSelectionConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var result = System.Convert.ToBoolean(value);
                if (result)
                {
                    return "ic_check";
                }
                else
                {
                    return "ic_uncheck";
                }
            }
            return "ic_uncheck";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
}
}
