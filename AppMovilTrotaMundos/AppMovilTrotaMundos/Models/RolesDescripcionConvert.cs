//using System;
//using System.Globalization;
//using Xamarin.Forms;
//using AppMovilTrotaMundos.ViewModels;

//namespace AppMovilTrotaMundos.Models
//{
//	public class RolDescripcionConverter : IValueConverter
//	{
//		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//		{
//			if (value is int rolId && EmpleadosViewModel.Roles.TryGetValue(rolId, out var rol))
//			{
//				return rol.Descripcion;
//			}
//			return "Desconocido";
//		}

//		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//		{
//			throw new NotImplementedException();
//		}
//	}

//	public class StatusDescripcionConverter : IValueConverter
//	{
//		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//		{
//			if (value is int estatusId && EmpleadosViewModel.Estados.TryGetValue(estatusId, out var estatus))
//			{
//				return estatus.Descripcion;
//			}
//			return "Desconocido";
//		}

//		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
