using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;
using System.Diagnostics;

namespace AppMovilTrotaMundos.Views
{
	public partial class FormularioCheckListServicio : ContentPage
	{
		public FormularioCheckListServicio()
        {
            InitializeComponent();

            // Estableciendo el BindingContext
            var viewModel = new FormularioChecklistServicioViewModel();
            BindingContext = viewModel;

            // Debug para verificar el ViewModel
            Debug.WriteLine($"BindingContext inicializado con: {viewModel?.GetType().Name}");
        }


	}
}
