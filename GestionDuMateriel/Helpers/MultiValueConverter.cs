using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GestionDuMateriel.Helpers
{
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            string value = null;
            int len = values.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                value = "";
                if (!(string.IsNullOrEmpty(values[i].ToString()))) value = values[i].ToString();                 
                if (string.IsNullOrEmpty(result))
                {
                    // first one
                    if (!(string.IsNullOrEmpty(value))) result = value.Trim();
                }
                else
                {
                    if (!(string.IsNullOrEmpty(value)))
                    {
                        result += " " + value.Trim();
                        result = result.Trim();
                    }
                }
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
