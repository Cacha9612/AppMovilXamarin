using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class CheckListServicioDetalles : ContentPage
	{
		public CheckListServicioDetalles(int Id)
		{
			InitializeComponent();
			BindingContext = new CheckListServicioDetallesViewModel(Id);
		}
	}
}
