using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Models
{
    public class EditingStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isEditing)
            {
                return isEditing ? "Edición" : "Creación";
            }
            return "Creación"; // Valor por defecto si el valor no es booleano
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == "Edición"; // Esto es opcional, dependiendo de tus necesidades
        }
    }
}
