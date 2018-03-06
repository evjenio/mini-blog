using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MiniBlog.UI.WPF.Converters
{
    /// <summary>
    /// Converts Int To Visibility
    /// </summary>
    public class IntToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value != 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
