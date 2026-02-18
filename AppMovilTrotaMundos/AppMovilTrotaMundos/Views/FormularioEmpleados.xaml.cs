using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class FormularioEmpleados: ContentPage
	{
		public FormularioEmpleados()
		{
			InitializeComponent();
			BindingContext = new FormularioEmpleadosViewModel(); // Establece el ViewModel
		}
	}
}
