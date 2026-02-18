using System.Threading.Tasks;
using Xamarin.Forms;
using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Services;
using System;

namespace AppMovilTrotaMundos.ViewModels
{
	public class ClientesDetallesViewModel : BaseViewModel
	{
		private readonly ApiService _apiService;
		private Clientes _cliente;

		public Clientes Cliente
		{
			get => _cliente;
			set
			{
				_cliente = value;
				OnPropertyChanged();
			}
		}

		public ClientesDetallesViewModel(int clienteId)
		{
			_apiService = new ApiService();
			LoadClienteAsync(clienteId);
		}

		private async Task LoadClienteAsync(int clienteId)
		{
			try
			{
				// Reemplaza la URL si es necesario
				Cliente = await _apiService.GetAsync<Clientes>($"api/cliente?idCliente={clienteId}");
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
