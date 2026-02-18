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
	public partial class Clientes : ContentPage
	{
		public Clientes()
		{
			InitializeComponent();
			BindingContext = new ClientesViewModel();
			
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var viewModel = BindingContext as ClientesViewModel;
			await viewModel?.LoadClientesAsync(); // Llama a LoadClientesAsync si es necesario
		}


        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var clienteSeleccionado = e.CurrentSelection[0] as Models.Clientes;

                if (clienteSeleccionado != null)
                {
                    // Navegar a la página de detalles del cliente pasando el ID del cliente
                    await Navigation.PushAsync(new ClientesDetalles(clienteSeleccionado.IdCliente));
                }

                // Deseleccionar el elemento (opcional, si quieres limpiar la selección)
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
