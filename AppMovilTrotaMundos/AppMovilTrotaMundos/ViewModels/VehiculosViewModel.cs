using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Services;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using AppMovilTrotaMundos.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Net.Http;

namespace AppMovilTrotaMundos.ViewModels
{
	public class VehiculosViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Models.Vehiculos> Vehiculo { get; set; }
		public ObservableCollection<Models.Vehiculos> FiltradosVehiculos { get; set; }
		public ICommand AgregarVehiculoCommand { get; }
        public ICommand ModificarVehiculoCommand { get; }
        private readonly ApiService _apiService;

		private string _searchText;
		public string SearchText
		{
			get => _searchText;
			set
			{
				if (_searchText != value)
				{
					_searchText = value;
					OnPropertyChanged();
					FiltrarVehiculos();
				}
			}
		}

		public VehiculosViewModel()
		{
			Vehiculo = new ObservableCollection<Models.Vehiculos>();
			FiltradosVehiculos = new ObservableCollection<Models.Vehiculos>();
			_apiService = new ApiService();
			LoadVehiculosAsync();
			AgregarVehiculoCommand = new Command(async () => await OnAgregarVehiculo()); // Se usa un Action sin parámetros
            ModificarVehiculoCommand = new Command<Models.Vehiculos>(async (vehiculo) => await OnModificarVehiculo(vehiculo));
        }

		public async Task LoadVehiculosAsync()
		{
			try
			{
				// Obtén la lista de vehículos desde la API
				var response = await _apiService.GetRawAsync("api/vehiculos");
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Para depuración
				Console.WriteLine("Respuesta de la API: " + jsonResponse);

				// Deserializa la respuesta con el conversor personalizado
				var vehiculos = JsonConvert.DeserializeObject<List<Models.Vehiculos>>(jsonResponse);

				// Verifica si se recibieron datos
				if (vehiculos == null || vehiculos.Count == 0)
				{
					throw new Exception("No se recibieron datos de la API.");
				}

				// Limpia la lista actual de vehículos
				Vehiculo.Clear();

				// Agrega los vehículos recibidos a la lista
				foreach (var vehiculo in vehiculos)
				{
					Vehiculo.Add(vehiculo);
				}

				// Filtra los vehículos después de cargarlos
				FiltrarVehiculos();
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}




		private void FiltrarVehiculos()
		{
			if (string.IsNullOrWhiteSpace(SearchText))
			{
				FiltradosVehiculos = new ObservableCollection<Models.Vehiculos>(Vehiculo);
			}
			else
			{
				FiltradosVehiculos = new ObservableCollection<Models.Vehiculos>(
					Vehiculo.Where(c => c.Marca?.ToLower().Contains(SearchText.ToLower()) == true ||
									   c.Modelo?.ToLower().Contains(SearchText.ToLower()) == true ||
									   c.Placa?.ToLower().Contains(SearchText.ToLower()) == true)
				);
			}
			OnPropertyChanged(nameof(FiltradosVehiculos));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        private async Task OnAgregarVehiculo()
        {
            try
            {
                // Crear el ViewModel en modo de creación
                var viewModel = new FormularioVehiculosViewModel
                {
                    IsEditing = false  // Se indica que estamos en modo creación
                };

                // Crear una instancia de la página FormularioVehiculos y asignar el ViewModel
                var pagina = new FormularioVehiculos
                {
                    BindingContext = viewModel
                };

                // Navegar a la página
                await Application.Current.MainPage.Navigation.PushAsync(pagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task OnModificarVehiculo(Models.Vehiculos vehiculo)
        {
            if (vehiculo == null)
                return;

            try
            {
                // Crear el ViewModel en modo de edición
                var viewModel = new FormularioVehiculosViewModel
                {
                    IsEditing = true,  // Se indica que estamos en modo edición
                    Vehiculo = vehiculo // Pasar el vehículo a editar
                };

                // Crear una instancia de la página FormularioVehiculos y asignar el ViewModel
                var pagina = new FormularioVehiculos
                {
                    BindingContext = viewModel
                };

                // Navegar a la página
                await Application.Current.MainPage.Navigation.PushAsync(pagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }





    }
}
