using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class EditarEmpleadoPage : ContentPage
	{
		public EditarEmpleadoPage(Models.Empleados empleadoId)
		{
			InitializeComponent();
			BindingContext = new EditarEmpleadoViewModel(empleadoId);
		}
			


	}
}
