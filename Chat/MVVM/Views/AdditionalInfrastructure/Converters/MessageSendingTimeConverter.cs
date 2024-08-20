using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chat.MVVM.Views.AdditionalInfrastructure.Converters
{
    internal class MessageSendingTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime input = (DateTime)value;

            DateTime now = DateTime.Now;
            DateTime dayAgo = now.AddDays(-1);
            DateTime weekAgo = now.AddDays(-7);

            if (input > dayAgo)
                return $"{input.Hour}:{input.Minute}";
            else if (input > weekAgo)
                return input.DayOfWeek.ToString().Substring(0, 3);
            else
                return input.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
