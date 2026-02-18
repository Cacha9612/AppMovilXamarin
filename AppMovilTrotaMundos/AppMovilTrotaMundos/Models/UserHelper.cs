using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppMovilTrotaMundos.Models
{
	public static class UserHelper
	{
		public static async Task<int?> ObtenerIdUsuarioAsync()
		{
			try
			{
				var idUsuarioString = await SecureStorage.GetAsync("idUsuario");
				if (!string.IsNullOrEmpty(idUsuarioString) && int.TryParse(idUsuarioString, out int idUsuario))
				{
					return idUsuario;
				}
			}
			catch (Exception ex)
			{
				
				Console.WriteLine(ex.Message);
			}

			return null;
		}
	}

}
