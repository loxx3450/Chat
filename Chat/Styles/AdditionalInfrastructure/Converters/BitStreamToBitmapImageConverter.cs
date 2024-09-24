using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Chat.Styles.AdditionalInfrastructure.Converters
{
    public class BitStreamToBitmapImageConverter : IMultiValueConverter
    {
        private string imagesPath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"/Images/";

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            MemoryStream stream;

            // If we got icon from server
            if (values[0] is not null)
            {
                stream = new MemoryStream((byte[])values[0]);
            }
            else
            {
                bool isGroup = (bool)values[1];

                // Setting default icon
                if (isGroup)
                    stream = new MemoryStream(File.ReadAllBytes(imagesPath + "group_default_light_icon.png"));                  //TODO: different themes
                else
                    stream = new MemoryStream(File.ReadAllBytes(imagesPath + "user_default_light_icon.png"));
            }

            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.EndInit();

            return bitmap;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
