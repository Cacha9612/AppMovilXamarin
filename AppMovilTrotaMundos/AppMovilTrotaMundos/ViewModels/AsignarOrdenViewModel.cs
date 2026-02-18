//using AppMovilTrotaMundos.Models;
//using AppMovilTrotaMundos.Services;
//using System.Collections.Generic;
//using System;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Threading.Tasks;
//using Xamarin.Forms;
//using System.Windows.Input;
//using AppMovilTrotaMundos.Views;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using Xamarin.Essentials;
//using System.IO;

//namespace AppMovilTrotaMundos.ViewModels
//{
//    public class ClientesViewModel : INotifyPropertyChanged
//    {
//        public ObservableCollection<Models.Clientes> Cliente { get; set; }
//        public ObservableCollection<Models.Clientes> FiltradosClientes { get; set; }
//        public ICommand AgregarClienteCommand { get; }
//        public ICommand DescargarPdfCommand { get; }
        
//        private readonly ApiService _apiService;

//        private string _searchText;
//        public string SearchText
//        {
//            get => _searchText;
//            set
//            {
//                if (_searchText != value)
//                {
//                    _searchText = value;
//                    OnPropertyChanged();
//                    FiltrarClientes();
//                }
//            }
//        }

//        public ClientesViewModel()
//        {
//            Cliente = new ObservableCollection<Models.Clientes>();
//            FiltradosClientes = new ObservableCollection<Models.Clientes>();
//            _apiService = new ApiService();
//            LoadClientesAsync();
//            AgregarClienteCommand = new Command(async () => await OnAgregarCliente());
//            DescargarPdfCommand = new Command<int>(async (idCliente) => await OnDescargarPdf(idCliente));
            
//        }

//        public async Task LoadClientesAsync()
//        {
//            try
//            {
//                var clientes = await _apiService.GetAsync<List<Models.Clientes>>("api/clientes");
//                if (clientes == null)
//                {
//                    throw new Exception("No se recibieron datos de la API.");
//                }
//                Cliente.Clear();
//                foreach (var cliente in clientes)
//                {
//                    Cliente.Add(cliente);
//                }
//                FiltrarClientes();
//            }
//            catch (Exception ex)
//            {
//                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
//            }
//        }

//        private void FiltrarClientes()
//        {
//            if (string.IsNullOrWhiteSpace(SearchText))
//            {
//                FiltradosClientes = new ObservableCollection<Models.Clientes>(Cliente);
//            }
//            else
//            {
//                FiltradosClientes = new ObservableCollection<Models.Clientes>(
//                    Cliente.Where(c => c.Nombre?.ToLower().Contains(SearchText.ToLower()) == true ||
//                                       c.Email?.ToLower().Contains(SearchText.ToLower()) == true ||
//                                       c.Tel?.ToLower().Contains(SearchText.ToLower()) == true)
//                );
//            }
//            OnPropertyChanged(nameof(FiltradosClientes));
//        }

//        private async Task OnDescargarPdf(int idCliente)
//        {
//            try
//            {
//                // Descargar el archivo PDF como arreglo de bytes
//                var pdfBytes = await _apiService.DescargarPDFAsync(idCliente);

//                if (pdfBytes != null && pdfBytes.Length > 0)
//                {
//                    // Aquí puedes hacer lo que sea necesario para guardar el archivo o mostrarlo
//                    // Por ejemplo, guardar el archivo en el dispositivo
//                    var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "orden_servicio.pdf");

//                    // Guardar el archivo PDF en el dispositivo
//                    File.WriteAllBytes(filePath, pdfBytes);

//                    // Notificar al usuario que el archivo se guardó exitosamente
//                    await Application.Current.MainPage.DisplayAlert("Éxito", "PDF descargado correctamente", "OK");
//                }
//            }
//            catch (Exception ex)
//            {
//                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }

//        private async Task OnAgregarCliente()
//        {
//            try
//            {
//                // Navegar a la página de FormularioClientes
//                await Application.Current.MainPage.Navigation.PushAsync(new FormularioClientes());
//            }
//            catch (Exception ex)
//            {
//                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
//            }
//        }
//    }
//}
