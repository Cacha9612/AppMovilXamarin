using System.Threading.Tasks;
using Xamarin.Forms;
using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using AppMovilTrotaMundos.Views;

namespace AppMovilTrotaMundos.ViewModels
{
    public class EmpleadosDetallesViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private Models.Empleados _empleado;

        // Propiedad que será vinculada a la vista
        public Models.Empleados Empleado
        {
            get => _empleado;
            set
            {
                _empleado = value;
                OnPropertyChanged();
            }
        }

        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }


        public EmpleadosDetallesViewModel(int empleadoId)
        {
            _apiService = new ApiService();
            EditarCommand = new Command(async () => await EditarEmpleado());
            EliminarCommand = new Command(async () => await EliminarEmpleado());

            Task.Run(async () => await LoadEmpleadosAsync(empleadoId));
        }

        // Método para cargar datos de un empleado desde el endpoint
        private async Task LoadEmpleadosAsync(int empleadoId)
        {
            try
            {
                // Reemplaza la URL según sea necesario
                var empleados = await _apiService.GetAsync<List<Models.Empleados>>($"api/empleado?IdUsuario={empleadoId}");

                // Si se obtiene una lista, toma el primer empleado (asumiendo que la lista contiene solo uno)
                Empleado = empleados?.Count > 0 ? empleados[0] : null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        private async Task EditarEmpleado()
        {
            // Lógica para abrir un formulario de edición
            await Application.Current.MainPage.Navigation.PushAsync(new EditarEmpleadoPage(Empleado));
        }

        private async Task EliminarEmpleado()
        {
            var respuesta = await Application.Current.MainPage.DisplayAlert(
                "Confirmación",
                "¿Está seguro de eliminar este empleado?",
                "Sí",
                "No"
            );

            if (respuesta)
            {
                try
                {
                    await _apiService.DeleteAsync($"api/empleado/{Empleado.IdUsuario}");
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Empleado eliminado correctamente", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync(); // Regresa a la vista anterior
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }
}
