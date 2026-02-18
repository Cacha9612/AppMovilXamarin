using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class FormularioFlotillas : ContentPage
	{
		public FormularioFlotillas()
		{
			InitializeComponent();
			BindingContext = new FormularioFlotillasViewModel(); // Establece el ViewModel
		}
	}
}
