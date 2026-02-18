using System.Threading.Tasks;
using Xamarin.Forms;
using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AppMovilTrotaMundos.ViewModels
{
    public class EditarEmpleadoViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        public Empleados Empleado { get; }
        public List<string> Roles { get; } = new List<string> { "Administrador", "Almacén", "Técnicos", "Jefe de Taller" };
        public List<string> Estatus { get; } = new List<string> { "Activo", "Inactivo" };

        public ICommand GuardarCommand { get; }

        public EditarEmpleadoViewModel(Empleados empleado)
        {
            _apiService = new ApiService();
            Empleado = empleado;
            GuardarCommand = new Command(async () => await GuardarEmpleado());
        }

        private async Task GuardarEmpleado()
        {
            try
            {
                await _apiService.PutAsync($"api/empleado/{Empleado.IdUsuario}", Empleado);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Empleado actualizado correctamente", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

}