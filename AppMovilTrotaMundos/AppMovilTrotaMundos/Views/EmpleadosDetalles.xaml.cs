using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class EmpeladosDetalles : ContentPage
	{
		public EmpeladosDetalles(int clienteId)
		{
			InitializeComponent();
			BindingContext = new EmpleadosDetallesViewModel(clienteId);
		}



	}
}
