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
    public class FormularioClientesViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private Models.Clientes _cliente;
        private bool _isSaving;

        public Models.Clientes Cliente
        {
            get => _cliente;
            set
            {
                _cliente = value;
                OnPropertyChanged(nameof(Cliente));
            }
        }

        public Command GuardarClienteCommand { get; }

        public FormularioClientesViewModel()
        {
            _apiService = new ApiService();
            Cliente = new Models.Clientes();
            GuardarClienteCommand = new Command(async () => await GuardarClienteAsync(), () => !_isSaving);
        }

        private async Task GuardarClienteAsync()
        {
            if (_isSaving) return;
            _isSaving = true;
            GuardarClienteCommand.ChangeCanExecute();

            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(Cliente.Nombre) ||
                    string.IsNullOrWhiteSpace(Cliente.Tel) ||
                    string.IsNullOrWhiteSpace(Cliente.Cel) ||
                    string.IsNullOrWhiteSpace(Cliente.Email))
                {
                    await Application.Current.MainPage.DisplayAlert("Validación", "Todos los campos obligatorios deben estar llenos.", "OK");
                    return;
                }

                // Evita duplicados
                bool clienteExiste = await _apiService.ClienteYaExisteAsync(Cliente.Nombre, Cliente.Email);
                if (clienteExiste)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Ya existe un cliente con ese nombre o correo electrónico.", "OK");
                    return;
                }
                else
                {
                    await _apiService.AddClienteAsync(Cliente);
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Cliente guardado exitosamente", "OK");
                    Cliente = new Models.Clientes();
                }
               
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo guardar el cliente: {ex.Message}", "OK");
            }
            finally
            {
                _isSaving = false;
                GuardarClienteCommand.ChangeCanExecute();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }




}
