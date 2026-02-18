//using AppMovilTrotaMundos.Models;
//using AppMovilTrotaMundos.ViewModels;
//using Xamarin.Forms;

//namespace AppMovilTrotaMundos.Views
//{
//	public partial class Vehiculos : ContentPage
//	{
//		public Vehiculos()
//		{
//			InitializeComponent();
//			BindingContext = new VehiculosViewModel();

//		}
//		protected override async void OnAppearing()
//		{
//			base.OnAppearing();
//			var viewModel = BindingContext as VehiculosViewModel;
//			await viewModel?.LoadVehiculosAsync(); // Llama a LoadClientesAsync si es necesario
//		}
//	}
//}


using AppMovilTrotaMundos.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Views
{
	public partial class CheckListServicio : ContentPage
	{
		public CheckListServicio()
		{
			InitializeComponent();
			BindingContext = new CheckListServicioViewModel();
		}

		//protected override async void OnAppearing()
		//{
		//	base.OnAppearing();
		//	var viewModel = BindingContext as CheckListServicioViewModel;
		//	await viewModel?.LoadCheckListAsync(); // Llama a LoadClientesAsync si es necesario
		//}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as CheckListServicioViewModel;

            // Obtener el id del empleado desde SecureStorage
            var empleadoId = await SecureStorage.GetAsync("idUsuario");

            // Llama a LoadCheckListAsync con el empleadoId
            if (!string.IsNullOrEmpty(empleadoId))
            {
                await viewModel?.LoadCheckListAsync(empleadoId);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se encontró el ID del empleado.", "OK");
            }
        }



        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
			{
				var checklistSeleccionado = e.CurrentSelection[0] as Models.CheckList;

				if (checklistSeleccionado != null)
				{
					// Navega a la página de detalles del vehículo con el ID seleccionado
					await Navigation.PushAsync(new CheckListServicioDetalles(checklistSeleccionado.Id));
				}

				// Deseleccionar el elemento
				((CollectionView)sender).SelectedItem = null;
			}
		}


	}
}




