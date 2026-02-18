//using AppMovilTrotaMundos.ViewModels;
//using Xamarin.Forms;

//namespace AppMovilTrotaMundos.Views
//{
//	public partial class Clientes : ContentPage
//	{
//		public Clientes()
//		{
//			InitializeComponent();
//		}

//		protected override async void OnAppearing()
//		{
//			base.OnAppearing();
//			var viewModel = BindingContext as ClientesViewModel;
//			await viewModel?.LoadClientesAsync(); // Llama a LoadClientesAsync si es necesario
//		}
//	}
//}


using AppMovilTrotaMundos.ViewModels;
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Views
{
	public partial class Empleados : ContentPage
	{
		public Empleados()
		{
			InitializeComponent();
			BindingContext = new EmpleadosViewModel();
			
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var viewModel = BindingContext as EmpleadosViewModel;
			await viewModel?.LoadEmpleadosAsync(); // Llama a LoadClientesAsync si es necesario

		}


		private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
			{
				var empleadoSeleccionado = e.CurrentSelection[0] as Models.Empleados;

				if (empleadoSeleccionado != null)
				{
					// Navega a la página de detalles del vehículo con el ID seleccionado
					await Navigation.PushAsync(new EmpeladosDetalles(empleadoSeleccionado.IdUsuario));
				}

				// Deseleccionar el elemento
				((CollectionView)sender).SelectedItem = null;
			}
		}
	}
}
