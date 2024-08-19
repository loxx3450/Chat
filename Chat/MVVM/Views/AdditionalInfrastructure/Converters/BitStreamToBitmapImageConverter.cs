using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Chat.MVVM.Views.AdditionalInfrastructure.Converters
{
    public class BitStreamToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            BitmapImage bitmap = new BitmapImage();

            MemoryStream stream = new MemoryStream((byte[])value);

            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.EndInit();

            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
