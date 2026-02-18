using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Views;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using AppMovilTrotaMundos.Services;
using System.Collections.Generic;

namespace AppMovilTrotaMundos.ViewModels
{
	public class FormularioEmpleadosViewModel : BaseViewModel
	{
		private readonly ApiService _apiService;
		private Models.Empleados _empleado;

		public Models.Empleados Empleado
		{
			get => _empleado;
			set
			{
				_empleado= value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<Status> _status;
		private Status _selectedStatus;


		private ObservableCollection<Rol> _rol;
		private Rol _selectedRol;

		public ObservableCollection<Status> Status
		{
			get => _status;
			set
			{
				_status = value;
				OnPropertyChanged();
			}
		}

		public Status SelectedStatus
		{
			get => _selectedStatus;
			set
			{
				_selectedStatus = value;
				OnPropertyChanged();
				Empleado.Estatus = _selectedStatus?.Id_Estatus ?? 0;
			}
		}

        public ObservableCollection<Rol> Rol
		{
			get => _rol;
			set
			{
				_rol = value;
				OnPropertyChanged();
			}
		}

		public Rol SelectedRol
		{
			get => _selectedRol;
			set
			{
				_selectedRol = value;
				OnPropertyChanged();
				Empleado.Rol = _selectedRol?.Id_rol ?? 0;
			}
		}


		public Command GuardarEmpleadoCommand { get; }

		public FormularioEmpleadosViewModel()
		{
			// Inicializa un cliente vacío o con valores predeterminados si es necesario
			Empleado = new Models.Empleados();
			_apiService = new ApiService();
			
			_status = new ObservableCollection<Status>();
			_rol = new ObservableCollection<Rol>();
			LoadEsatusAsync();
			LoadRolAsync();
			GuardarEmpleadoCommand = new Command(async () => await GuardarClienteAsync());
		}


		private async Task LoadEsatusAsync()
		{
			var status = await _apiService.GetAsync<List<Status>>("api/estatus");
			Status = new ObservableCollection<Status>(status);
		}

		private async Task LoadRolAsync()
		{
			var roles= await _apiService.GetAsync<List<Rol>>("api/roles");
			Rol = new ObservableCollection<Rol>(roles);
		}


		private async Task GuardarClienteAsync()
		{
			try
			{
				if (string.IsNullOrWhiteSpace(Empleado.Nombre)  || string.IsNullOrWhiteSpace(Empleado.Password))

				{
					await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son requeridos.", "OK");
					return;
				}


				await _apiService.AddEmpeladoAsync(Empleado);
				await Application.Current.MainPage.DisplayAlert("Éxito", "Empleado guardado exitosamente", "OK");
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo guardar el cliente: {ex.Message}", "OK");
			}
		}



		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	
	
}
