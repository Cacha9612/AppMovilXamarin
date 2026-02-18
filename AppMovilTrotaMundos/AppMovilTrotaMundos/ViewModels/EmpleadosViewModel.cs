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

namespace AppMovilTrotaMundos.ViewModels
{
	public class EmpleadosViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Models.Empleados> Empleado { get; set; }
		public ObservableCollection<Models.Empleados> FiltradosEmpleados { get; set; }
		public ICommand AgregarEmpleadoCommand { get; }
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
					FiltrarEmpleados();
				}
			}
		}

		public EmpleadosViewModel()
		{
			Empleado = new ObservableCollection<Models.Empleados>();
			FiltradosEmpleados = new ObservableCollection<Models.Empleados>();
			_apiService = new ApiService();
			LoadEmpleadosAsync();
			AgregarEmpleadoCommand = new Command(async () => await OnAgregarEmpleado()); // Se usa un Action sin parámetros
		}

		public async Task LoadEmpleadosAsync()
		{
			try
			{
				var empleados = await _apiService.GetAsync<List<Models.Empleados>>("api/empleados");
				if (empleados == null)
				{
					throw new Exception("No se recibieron datos de la API.");
				}
				Empleado.Clear();
				foreach (var empleado in empleados)
				{
					Empleado.Add(empleado);
				}
				FiltrarEmpleados();
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}

		private void FiltrarEmpleados()
		{
			if (string.IsNullOrWhiteSpace(SearchText))
			{
				FiltradosEmpleados = new ObservableCollection<Models.Empleados>(Empleado);
			}
			else
			{
				FiltradosEmpleados = new ObservableCollection<Models.Empleados>(
					Empleado.Where(c => c.Nombre?.ToLower().Contains(SearchText.ToLower()) == true)
				);
			}
			OnPropertyChanged(nameof(FiltradosEmpleados));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private async Task OnAgregarEmpleado()
		{
			try
			{
				// Navegar a la página de FormularioClientes
				await Application.Current.MainPage.Navigation.PushAsync(new FormularioEmpleados());
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}




	}
}
