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
using System.Diagnostics;

namespace AppMovilTrotaMundos.ViewModels
{
    public class ClientesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Clientes> Cliente { get; set; }
        public ObservableCollection<Models.Clientes> FiltradosClientes { get; set; }
        public ICommand AgregarClienteCommand { get; }

        private int ClienteSeleccionadoId { get; set; } // Esto debe asignarse en algún lugar en la UI
        private string NumeroSerieSeleccionado { get; set; } // Esto también debe asignarse según lo que selecciones en la UI
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
                    FiltrarClientes();
                }
            }
        }

        public class OrdenServicioParam
        {
            public int IdCliente { get; set; }
            public string NoSerie { get; set; }
        }

        public ClientesViewModel()
        {
            Cliente = new ObservableCollection<Models.Clientes>();
            FiltradosClientes = new ObservableCollection<Models.Clientes>();
            _apiService = new ApiService();
            LoadClientesAsync();
            AgregarClienteCommand = new Command(async () => await OnAgregarCliente());
            //DescargarPdfCommand = new Command(async () => await OnDescargarPdf());
            DescargarPdfCommand = new Command<int>(async (idCliente) => await DescargarOrdenServicio(idCliente));
        }

        public async Task LoadClientesAsync()
        {
            try
            {
                var clientes = await _apiService.GetAsync<List<Models.Clientes>>("api/clientes");
                if (clientes == null)
                {
                    throw new Exception("No existen clientes registrados");
                }
                Cliente.Clear();
                foreach (var cliente in clientes)
                {
                    Cliente.Add(cliente);
                }
                FiltrarClientes();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void FiltrarClientes()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FiltradosClientes = new ObservableCollection<Models.Clientes>(Cliente);
            }
            else
            {
                FiltradosClientes = new ObservableCollection<Models.Clientes>(
                    Cliente.Where(c => c.Nombre?.ToLower().Contains(SearchText.ToLower()) == true ||
                                       c.Email?.ToLower().Contains(SearchText.ToLower()) == true ||
                                       c.Tel?.ToLower().Contains(SearchText.ToLower()) == true)
                );
            }
            OnPropertyChanged(nameof(FiltradosClientes));
        }

        // Este método debe ejecutarse cuando el usuario selecciona un cliente
        public void OnClienteSeleccionado(Models.Clientes clienteSeleccionado)
        {
            // Asignar el IdCliente del cliente seleccionado a la propiedad ClienteSeleccionadoId
            ClienteSeleccionadoId = clienteSeleccionado.IdCliente;
            Console.WriteLine($"Cliente seleccionado: {ClienteSeleccionadoId}");  // Verificar que el valor es correcto
        }


        private async Task DescargarOrdenServicio(int idCliente)
        {
            if (idCliente <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Cliente no válido.", "OK");
                return;
            }

            try
            {
                var fileBytes = await _apiService.DescargarOrdenDeServicioAsync(idCliente);

                if (fileBytes != null && fileBytes.Length > 0)
                {
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"Orden_{idCliente}.pdf");

                    File.WriteAllBytes(filePath, fileBytes);
                    await Application.Current.MainPage.DisplayAlert("Éxito", $"Orden descargada en:\n{filePath}", "OK");

                    // Opcional: Abrir el archivo después de la descarga
                    await AbrirArchivo(filePath);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se recibió un archivo válido.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async Task AbrirArchivo(string filePath)
        {
            try
            {
                await Xamarin.Essentials.Launcher.OpenAsync(new Xamarin.Essentials.OpenFileRequest
                {
                    File = new Xamarin.Essentials.ReadOnlyFile(filePath)
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo abrir el archivo: {ex.Message}", "OK");
            }
        }


        //public async Task OnDescargarPdf()
        //{
        //    try
        //    {
        //        // Obtener el cliente seleccionado (esto depende de cómo tengas estructurada la UI)
        //        Console.WriteLine($"IdCliente seleccionado: {ClienteSeleccionadoId}");
        //        var cliente = FiltradosClientes.FirstOrDefault(c => c.IdCliente == ClienteSeleccionadoId); // Aquí 'ClienteSeleccionadoId' es la variable con el IdCliente que usas

        //        if (cliente != null)
        //        {
        //            // Llamar al servicio para obtener las órdenes de servicio del cliente directamente desde la API
        //            var ordenes = await _apiService.GetOrdenesDeServicioAsync(cliente.IdCliente);

        //            // Verificar que se haya recibido la lista de órdenes
        //            if (ordenes != null && ordenes.Any())
        //            {
        //                // Buscar la orden de servicio con el número de serie
        //                var ordenSeleccionada = ordenes.FirstOrDefault(o => o.No_Serie == NumeroSerieSeleccionado); // 'NumeroSerieSeleccionado' es el número de serie que buscas

        //                if (ordenSeleccionada != null)
        //                {
        //                    // Ahora ya tienes la orden seleccionada, puedes continuar con la descarga o cualquier otra operación.
        //                    // Ejemplo para descargar el archivo de la orden de servicio
        //                    var documentoBytes = await _apiService.DescargarOrdenDeServicioAsync(ordenSeleccionada.idOrden);

        //                    if (documentoBytes != null && documentoBytes.Length > 0)
        //                    {
        //                        // Guardar el archivo en el dispositivo
        //                        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"orden_servicio_{ordenSeleccionada.idOrden}.docx");
        //                        File.WriteAllBytes(filePath, documentoBytes);

        //                        // Notificar éxito
        //                        await Application.Current.MainPage.DisplayAlert("Éxito", "Orden de servicio descargada correctamente", "OK");
        //                    }
        //                    else
        //                    {
        //                        await Application.Current.MainPage.DisplayAlert("Error", "No se pudo descargar la orden de servicio", "OK");
        //                    }
        //                }
        //                else
        //                {
        //                    await Application.Current.MainPage.DisplayAlert("Error", "No se encontró la orden de servicio para ese vehículo", "OK");
        //                }
        //            }
        //            else
        //            {
        //                await Application.Current.MainPage.DisplayAlert("Error", "No se encontraron órdenes de servicio para este cliente", "OK");
        //            }
        //        }
        //        else
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Error", "No se encontró el cliente", "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        //    }
        //}



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task OnAgregarCliente()
        {
            try
            {
                // Navegar a la página de FormularioClientes
                await Application.Current.MainPage.Navigation.PushAsync(new FormularioClientes());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
