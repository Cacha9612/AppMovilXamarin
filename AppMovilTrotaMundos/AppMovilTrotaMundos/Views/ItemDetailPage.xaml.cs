using AppMovilTrotaMundos.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}