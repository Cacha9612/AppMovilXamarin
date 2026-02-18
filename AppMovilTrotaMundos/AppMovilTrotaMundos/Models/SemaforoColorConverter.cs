using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Models
{
        public class SemaforoColorConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string estado = value as string;
                string color = parameter as string;
                if (estado == color)
                {
                    switch (color)
                    {
                        case "Rojo": return Color.Red;
                        case "Amarillo": return Color.Yellow;
                        case "Verde": return Color.Green;
                    }
                }
                return Color.LightGray;
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
        }
}