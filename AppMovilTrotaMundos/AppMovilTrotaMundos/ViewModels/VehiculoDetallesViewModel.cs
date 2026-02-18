using System.Threading.Tasks;
using Xamarin.Forms;
using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Services;
using System;

namespace AppMovilTrotaMundos.ViewModels
{
	public class VehiculoDetallesViewModel : BaseViewModel
	{
		private readonly ApiService _apiService;
		private Vehiculos _vehiculo;

		public Vehiculos Vehiculo
		{
			get => _vehiculo;
			set
			{
				_vehiculo = value;
				OnPropertyChanged();
			}
		}

		//public VehiculoDetallesViewModel(int vehiculoId)
		//{
		//	_apiService = new ApiService();
		//	LoadVehiculoAsync(vehiculoId);
		//}

		public VehiculoDetallesViewModel(int vehiculoId)
		{
			_apiService = new ApiService();
			Task.Run(async () => await LoadVehiculoAsync(vehiculoId));
		}


		private async Task LoadVehiculoAsync(int vehiculoId)
		{
			try
			{
				// Reemplaza la URL si es necesario
				Vehiculo = await _apiService.GetTVehiculo<Vehiculos>($"api/vehiculo?idVehiculo={vehiculoId}");
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
