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
	public class CheckListViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Models.CheckList> CheckList { get; set; }
		public ObservableCollection<Models.CheckList> FiltradosCheckList { get; set; }
		public ICommand AgregarCheckListCommand { get; }
		public ICommand ModificarCheckListCommand { get; }

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
					FiltrarCheckList();
				}
			}
		}

		public CheckListViewModel()
		{
			CheckList = new ObservableCollection<Models.CheckList>();
			FiltradosCheckList = new ObservableCollection<Models.CheckList>();
			_apiService = new ApiService();
			//LoadCheckListAsync();
			AgregarCheckListCommand = new Command(async () => await OnAgregarCheckList()); // Se usa un Action sin parámetros
            ModificarCheckListCommand = new Command<Models.CheckList>(async (checklist) => await OnModificarCheck(checklist));
        }

        public async Task LoadCheckListAsync(string empleadoId)
        {
            try
            {
                // Llama al API con el id del empleado
                var checklists = await _apiService.GetAsync<List<Models.CheckList>>($"api/obtenerchecklists?IdEmpleado={empleadoId}");

                if (checklists == null)
                {
                    throw new Exception("No se recibieron datos de la API.");
                }

                CheckList.Clear();
                foreach (var checklist in checklists)
                {
                    CheckList.Add(checklist);
                }

                FiltrarCheckList();
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje de error
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        private void FiltrarCheckList()
		{
			if (string.IsNullOrWhiteSpace(SearchText))
			{
				FiltradosCheckList = new ObservableCollection<Models.CheckList>(CheckList);
			}
			else
			{
				FiltradosCheckList = new ObservableCollection<Models.CheckList>(
					CheckList.Where(c => c.NumeroSerie?.ToLower().Contains(SearchText.ToLower()) == true)
				);
			}
			OnPropertyChanged(nameof(FiltradosCheckList));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private async Task OnAgregarCheckList()
		{

            try
            {
                // Navegar a la página de FormularioClientes
                var viewModel = new FormularioChecklistViewModel
                {
                    IsEditing = false  // Se indica que estamos en modo creación
                };

                // Crear una instancia de la página FormularioVehiculos y asignar el ViewModel
                var pagina = new FormularioCheckList
                {
                    BindingContext = viewModel
                };
                await Application.Current.MainPage.Navigation.PushAsync(new FormularioCheckList());
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}

        private async Task OnModificarCheck(Models.CheckList checklist)
        {
            if (checklist == null)
                return;

            try
            {
                // Crear el ViewModel en modo de edición
                var viewModel = new FormularioChecklistViewModel
                {
                    IsEditing = true,  // Se indica que estamos en modo edición
                    CheckList = checklist
                };

                // Crear una instancia de la página FormularioVehiculos y asignar el ViewModel
                var pagina = new FormularioCheckList
                {
                    BindingContext = viewModel
                };

                // Navegar a la página
                await Application.Current.MainPage.Navigation.PushAsync(pagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


      

    }
}
