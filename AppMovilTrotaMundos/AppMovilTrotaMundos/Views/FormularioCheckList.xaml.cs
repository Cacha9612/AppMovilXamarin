using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;
using System.Diagnostics;

namespace AppMovilTrotaMundos.Views
{
	public partial class FormularioCheckList : ContentPage
	{
		public FormularioCheckList()
        {
            InitializeComponent();

            // Estableciendo el BindingContext
            var viewModel = new FormularioChecklistViewModel();
            BindingContext = viewModel;

            // Debug para verificar el ViewModel
            Debug.WriteLine($"BindingContext inicializado con: {viewModel?.GetType().Name}");
        }


	}
}
