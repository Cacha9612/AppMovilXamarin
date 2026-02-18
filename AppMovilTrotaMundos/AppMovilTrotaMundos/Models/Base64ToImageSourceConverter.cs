using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Models
{
	public class Base64ToImageSourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is List<string> base64Strings && base64Strings.Count > 0)
			{
				// Retorna la primera imagen de la lista
				return HandleBase64Image(base64Strings[0]);
			}

			if (value is string singleBase64String)
			{
				return HandleBase64Image(singleBase64String); // Para el caso de una sola cadena Base64
			}

			return null; // Si no es válido
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		private ImageSource HandleBase64Image(string base64String)
		{
			if (string.IsNullOrWhiteSpace(base64String))
			{
				return null; // Maneja cadenas vacías
			}

			// Validar longitud y caracteres
			if (base64String.Length % 4 != 0 ||
				base64String.Contains(" ") ||
				base64String.Contains("\t") ||
				base64String.Contains("\r") ||
				base64String.Contains("\n"))
			{
				return null; // Cadena no válida
			}

			try
			{
				byte[] imageBytes = System.Convert.FromBase64String(base64String);
				return ImageSource.FromStream(() => new MemoryStream(imageBytes));
			}
			catch (FormatException)
			{
				Console.WriteLine($"Formato inválido para la cadena Base64: {base64String}");
				return null; // Error al convertir
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al convertir Base64 a imagen: {ex.Message}");
				return null; // Manejo de errores adicional
			}
		}
	}
}
