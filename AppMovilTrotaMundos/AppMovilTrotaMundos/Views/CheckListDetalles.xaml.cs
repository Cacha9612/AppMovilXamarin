using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class CheckListDetalles : ContentPage
	{
		public CheckListDetalles(int Id)
		{
			InitializeComponent();
			BindingContext = new CheckListDetallesViewModel(Id);
		}
	}
}
