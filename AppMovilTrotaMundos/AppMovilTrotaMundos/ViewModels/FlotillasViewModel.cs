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
using Xamarin.Essentials;
using System.IO;

namespace AppMovilTrotaMundos.ViewModels
{
    public class FlotillasViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Flotillas> Flotilla{ get; set; }
        public ObservableCollection<Models.Flotillas> FiltradosFlotillas { get; set; }
        public ICommand AgregarFlotillaCommand { get; }
        public ICommand DescargarPdfCommand { get; }

        
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
                    FiltrarFlotillas();
                }
            }
        }

        public FlotillasViewModel()
        {
            Flotilla = new ObservableCollection<Models.Flotillas>();
            _apiService = new ApiService();
            LoadFlotillasAsync();
            AgregarFlotillaCommand = new Command(async () => await OnAgregarFlotilla());
            //DescargarPdfCommand = new Command<int>(async (idCliente) => await OnDescargarPdf(idCliente));
            
        }

        public async Task LoadFlotillasAsync()
        {
            try
            {
                var flotillas = await _apiService.GetAsync<List<Models.Flotillas>>("api/obtenerflotillas");
                if (flotillas == null)
                {
                    throw new Exception("No se recibieron datos de la API.");
                }
                Flotilla.Clear();
                foreach (var flotilla in flotillas)
                {
                    Flotilla.Add(flotilla);
                }
                FiltrarFlotillas();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void FiltrarFlotillas()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FiltradosFlotillas = new ObservableCollection<Models.Flotillas>(Flotilla);
            }
            else
            {
                FiltradosFlotillas = new ObservableCollection<Models.Flotillas>(
                    Flotilla.Where(c => c.NamesFlotillas?.ToLower().Contains(SearchText.ToLower()) == true ||
                                       c.Encargado?.ToLower().Contains(SearchText.ToLower()) == true)
                );
            }
            OnPropertyChanged(nameof(FiltradosFlotillas));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task OnAgregarFlotilla()
        {
            try
            {
                // Navegar a la página de FormularioClientes
                await Application.Current.MainPage.Navigation.PushAsync(new FormularioFlotillas());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
