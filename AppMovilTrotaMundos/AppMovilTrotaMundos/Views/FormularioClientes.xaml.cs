using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;
using System;

namespace AppMovilTrotaMundos.Views
{
	public partial class FormularioClientes : ContentPage
	{
		public FormularioClientes()
		{
			InitializeComponent();
			BindingContext = new FormularioClientesViewModel(); // Establece el ViewModel
		}

        private void OnGuardarClicked(object sender, EventArgs e)
        {
            bool isValid = true;

            // Validar Nombre
            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                ErrorNombre.IsVisible = true;
                isValid = false;
            }
            else ErrorNombre.IsVisible = false;

            // Validar Teléfono
            if (string.IsNullOrWhiteSpace(TelefonoEntry.Text))
            {
                ErrorTelefono.IsVisible = true;
                isValid = false;
            }
            else ErrorTelefono.IsVisible = false;

            // Validar Celular
            if (string.IsNullOrWhiteSpace(CelularEntry.Text))
            {
                ErrorCelular.IsVisible = true;
                isValid = false;
            }
            else ErrorCelular.IsVisible = false;

         
        }

    }
}
