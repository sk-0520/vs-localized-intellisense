using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace VsLocalizedIntellisense.Models.Mvvm.Views.Converter
{
    [ValueConversion(typeof(string), typeof(string))]
    public class LocalizeEnumConverter : IValueConverter
    {
        #region IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueType = value.GetType();
            if(valueType.IsEnum)
            {
                var field = valueType.GetField(value.ToString());
                var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
                if(displayAttribute != null)
                {
                    var property = displayAttribute.ResourceType.GetProperty(displayAttribute.Description);
                    var v = property.GetValue(value, null);
                    if (v is string s && !string.IsNullOrEmpty(s))
                    {
                        return s;
                    }
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
