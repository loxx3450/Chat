using Chat.Styles.AdditionalInfrastructure.Converters.ConverterParameters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Chat.Styles.AdditionalInfrastructure.Converters
{
    public class WidthAndHeightToGridLengthConverter : IMultiValueConverter
    {
        private const double SQUARE_SCREEN_FACTOR = 1.6;
        private const double VERTICAL_SCREEN_FACTOR = 1.2;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var parameters = parameter as WidthAndHeightToGridLengthConverterParameters;

            //Default behaviour in case when something went wrong
            if (parameters is null || values.Length < 2)
                return new GridLength(parameters.HorizontalScreenValue, GridUnitType.Star);

            double currentWidth = (double)values[0];
            double currentHeight = (double)values[1];

            if (currentHeight * VERTICAL_SCREEN_FACTOR > currentWidth)
                return new GridLength(parameters.VerticalScreenValue, GridUnitType.Star);
            else if (currentHeight * SQUARE_SCREEN_FACTOR > currentWidth)
                return new GridLength(parameters.SquareScreenValue, GridUnitType.Star);
            else
                return new GridLength(parameters.HorizontalScreenValue, GridUnitType.Star);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
