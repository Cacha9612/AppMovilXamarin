using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Services;
using AppMovilTrotaMundos.Views;

namespace AppMovilTrotaMundos.ViewModels
{
	public class FormularioFlotillasViewModel : INotifyPropertyChanged
	{
		
        private readonly ApiService _apiService;
        private Models.Flotillas _flotilla;
		public Models.Flotillas Flotilla
		{
			get => _flotilla;
			set
			{
				_flotilla = value;
				OnPropertyChanged(nameof(Flotilla));
			}
		}

		public Command GuardarFlotillaCommand { get; }

		public FormularioFlotillasViewModel()
		{
            // Inicializa un cliente vacío o con valores predeterminados si es necesario
            _apiService = new ApiService();

            Flotilla = new Models.Flotillas();

			GuardarFlotillaCommand = new Command(async () => await GuardarFlotillaAsync());
		}

        private async Task GuardarFlotillaAsync()
        {
            try
            {
                // Si el encargado no está definido, asignar un valor predeterminado
                if (string.IsNullOrWhiteSpace(Flotilla.Encargado))
                {
                    Flotilla.Encargado = "Sin encargado";
                }

                // Llamada al servicio para guardar la flotilla
                await _apiService.AddFlotillaAsync(Flotilla);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Flotilla guardada exitosamente", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo guardar la flotilla: {ex.Message}", "OK");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	
	
}
