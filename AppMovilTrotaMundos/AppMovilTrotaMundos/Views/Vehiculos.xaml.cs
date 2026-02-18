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
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Views
{
	public partial class Vehiculos : ContentPage
	{
		public Vehiculos()
		{
			InitializeComponent();
			BindingContext = new VehiculosViewModel();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var viewModel = BindingContext as VehiculosViewModel;
			await viewModel?.LoadVehiculosAsync(); // Llama a LoadClientesAsync si es necesario
		}


		private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
			{
				var vehiculoSeleccionado = e.CurrentSelection[0] as Models.Vehiculos;

				if (vehiculoSeleccionado != null)
				{
					// Navega a la página de detalles del vehículo con el ID seleccionado
					await Navigation.PushAsync(new VehiculoDetalles(vehiculoSeleccionado.ID));
				}

				// Deseleccionar el elemento
				((CollectionView)sender).SelectedItem = null;
			}
		}


	}
}




