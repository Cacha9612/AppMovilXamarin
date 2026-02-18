using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class FormularioVehiculos : ContentPage
	{
		public FormularioVehiculos()
		{
			InitializeComponent();
			BindingContext = new FormularioVehiculosViewModel();
		}

	}
}
