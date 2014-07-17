using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace neihanshe.Core.Convert
{
    public class AvatarConvert : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(string.Format("http://avatar.neihanshe.cn/avatar/{0}.jpg", value)));
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
