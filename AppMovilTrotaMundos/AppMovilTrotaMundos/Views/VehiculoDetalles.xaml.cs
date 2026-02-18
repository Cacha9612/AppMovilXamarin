using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class VehiculoDetalles : ContentPage
	{
		public VehiculoDetalles(int vehiculoId)
		{
			InitializeComponent();
			BindingContext = new VehiculoDetallesViewModel(vehiculoId);
		}
	}
}
