using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;

namespace AppMovilTrotaMundos.Views
{
	public partial class ClientesDetalles : ContentPage
	{
        private int _idCliente;
        public ClientesDetalles(int idCliente)
        {
			InitializeComponent();
            _idCliente = idCliente;
            BindingContext = new ClientesDetallesViewModel(idCliente);
		}



	}
}
