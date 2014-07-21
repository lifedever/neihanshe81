using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace neihanshe.Core
{
    public class SettingUtils
    {

        public static void Save(string key, Object value)
        {
            ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
            settings.Values[key] = value;
        }

        public static Object Get(string key)
        {
            ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
            if (settings.Values.ContainsKey(key))
            {
                return settings.Values[key];
            }
            return null;
        }
    }
}
