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
	public class CheckListServicioViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Models.CheckListServicio> CheckList { get; set; }
		public ObservableCollection<Models.CheckListServicio> FiltradosCheckList { get; set; }
		public ICommand AgregarCheckListCommand { get; }
        public ICommand ModificarCheckListServicioCommand { get; }

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

		public CheckListServicioViewModel()
		{
			CheckList = new ObservableCollection<Models.CheckListServicio>();
			FiltradosCheckList = new ObservableCollection<Models.CheckListServicio>();
			_apiService = new ApiService();
			
			AgregarCheckListCommand = new Command(async () => await OnAgregarCheckList()); // Se usa un Action sin parámetros
            ModificarCheckListServicioCommand = new Command<Models.CheckListServicio>(async (checklist) => await OnModificarServicio(checklist));
        }

        public async Task LoadCheckListAsync(string empleadoId)
        {
            try
            {
                // Llama al API con el id del empleado
                
                var checklists = await _apiService.GetAsync<List<Models.CheckListServicio>>($"api/obtenerservicios?IdEmpleado={empleadoId}");
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
				FiltradosCheckList = new ObservableCollection<Models.CheckListServicio>(CheckList);
			}
			else
			{
				FiltradosCheckList = new ObservableCollection<Models.CheckListServicio>(
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
				await Application.Current.MainPage.Navigation.PushAsync(new FormularioCheckListServicio());
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}

        private async Task OnModificarServicio(Models.CheckListServicio checklist)
        {
            if (checklist == null)
                return;

            try
            {
                // Crear una nueva instancia del ViewModel y asignar los datos
                var viewModel = new FormularioChecklistServicioViewModel
                {
                    CheckList = checklist,  // Asignamos el CheckList
                    IsEditing = true,  // Indicamos que estamos en modo de edición
                };

                // Crear una instancia de la página FormularioCheckListServicio
                var pagina = new FormularioCheckListServicio
                {
                    BindingContext = viewModel  // Asignamos el BindingContext al ViewModel
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
