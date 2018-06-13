using System;
using System.Globalization;

namespace XamarinNativeTemplate
{
    public static class CountConverter
    {
        public static string Convert(object value, object parameter = null) => string.Format("{0} clicks!", value);

        public static int ConvertBack(object value, object parameter = null) => (int)value;
    }
}