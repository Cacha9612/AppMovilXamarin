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
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace AppMovilTrotaMundos.ViewModels
{
    public class FormularioAsignarOrdenViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiservice;
        private ObservableCollection<Models.Vehiculos> _vehiculos;
        private ObservableCollection<Models.Empleados> _tecnicos;
        private Models.Vehiculos _vehiculoSeleccionado;
        private Models.Empleados _tecnicoSeleccionado;
        private string _mensaje;


        public string Mensaje
        {
            get { return _mensaje; }
            set
            {
                if (_mensaje != value)
                {
                    _mensaje = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Models.Vehiculos> Vehiculos
        {
            get => _vehiculos;
            set
            {
                _vehiculos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Models.Empleados> Tecnicos
        {
            get => _tecnicos;
            set
            {
                _tecnicos = value;
                OnPropertyChanged();
            }
        }

        public Models.Vehiculos VehiculoSeleccionado
        {
            get => _vehiculoSeleccionado;
            set
            {
                _vehiculoSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public Models.Empleados TecnicoSeleccionado
        {
            get => _tecnicoSeleccionado;
            set
            {
                _tecnicoSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public ICommand AsignarTecnicoCommand { get; }

        public FormularioAsignarOrdenViewModel()
        {
            _apiservice = new ApiService();
            _vehiculos = new ObservableCollection<Models.Vehiculos>();
            _tecnicos = new ObservableCollection<Models.Empleados>();

            // Cargar vehículos y técnicos desde el servicio
            LoadVehiculosAndTecnicos();

            AsignarTecnicoCommand = new Command(async () => await AsignarTecnico());
        }

        // Cargar datos de vehículos y técnicos

        private async Task LoadVehiculosAndTecnicos()
        {
            try
            {
                var vehiculos = await _apiservice.GetAsync<List<Models.Vehiculos>>("api/vehiculos");
                Vehiculos = new ObservableCollection<Models.Vehiculos>(vehiculos);

                var tecnicos = await _apiservice.GetAsync<List<Models.Empleados>>("api/obtenertecnicos");
                Tecnicos = new ObservableCollection<Models.Empleados>(tecnicos);
            }
            catch (Exception ex)
            {
                // Aquí puedes mostrar un mensaje de error al usuario si ocurre un problema.
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        // Asignar técnico al vehículo
        private async Task AsignarTecnico()
        {
            // Verificamos que ambos el vehículo y el técnico estén seleccionados
            if (VehiculoSeleccionado != null && TecnicoSeleccionado != null)
            {
                // Obtenemos el vehículo mediante su ID (la orden de servicio asociada al vehículo)
                var vehiculo = await _apiservice.GetTVehiculo<Models.Vehiculos>($"api/vehiculo?idVehiculo={VehiculoSeleccionado.ID}");

                if (vehiculo != null)
                {
                    // Asignamos el ID del técnico y la orden de servicio al método AsignarTecnicoAOrdenAsync
                    await _apiservice.AsignarTecnicoAOrdenAsync(vehiculo.IdOrdenServicio, TecnicoSeleccionado.IdUsuario);
                    Mensaje = "Orden asignada correctamente: ";
                    // Muestra un mensaje de éxito en la interfaz de usuario
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Orden asignada correctamente.", "OK");
                }
                else
                {
                    Mensaje = "Hubo un problema con la asignación.";
                    // Muestra un mensaje de error en la interfaz de usuario
                    await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema con la asignación.", "OK");
                }
            }
            else
            {
                // Si no se seleccionaron el vehículo o el técnico, puedes manejar el error (por ejemplo, mostrar un mensaje al usuario)
                Console.WriteLine("Por favor, seleccione un vehículo y un técnico.");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
