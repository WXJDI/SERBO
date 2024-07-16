using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfApp1.Tools
{
    public class WindowStateToMarginSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState windowState)
            {
                if (windowState == WindowState.Maximized)
                {
                    return new Thickness(0);
                }
                else
                {
                    // Adjust these values to your desired margins for the normal state
                    return new Thickness(20);
                }
            }
            return new Thickness(20); // Default margin
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
