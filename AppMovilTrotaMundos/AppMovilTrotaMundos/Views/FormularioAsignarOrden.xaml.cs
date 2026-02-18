using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class FormularioAsignarOrden : ContentPage
	{
		public FormularioAsignarOrden()
		{
			InitializeComponent();
			BindingContext = new FormularioAsignarOrdenViewModel(); // Establece el ViewModel
		}
	}
}
