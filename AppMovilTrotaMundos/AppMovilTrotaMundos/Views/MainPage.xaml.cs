using Xamarin.Forms;
using AppMovilTrotaMundos.ViewModels;
using AppMovilTrotaMundos.Models;
using Xamarin.Essentials;
using System;

namespace AppMovilTrotaMundos.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel(); // Asegúrate de que el BindingContext esté configurado

            // Cargar el menú de acuerdo con el usuario
            CargarMenu();
        }

        private async void CargarMenu()
        {
            try
            {
                var idUsuario = await SecureStorage.GetAsync("idUsuario");
                if (!string.IsNullOrEmpty(idUsuario))
                {
                    // Obtener las opciones del menú basadas en el idUsuario o rol
                    var menuViewModel = (MenuViewModel)BindingContext;
                    await menuViewModel.CargarMenu(int.Parse(idUsuario));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error al cargar el menú: {ex.Message}", "OK");
            }
        }

        private async void OnMenuItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null || e.CurrentSelection.Count == 0)
                return;

            if (e.CurrentSelection[0] is AppMovilTrotaMundos.Models.MenuItem selectedItem)
            {
                try
                {
                    if (selectedItem.TargetType != null && typeof(Page).IsAssignableFrom(selectedItem.TargetType))
                    {
                        var page = (Page)Activator.CreateInstance(selectedItem.TargetType);
                        await Navigation.PushAsync(page);
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo abrir la vista destino", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"No se pudo navegar a la vista: {ex.Message}", "OK");
                }
            }

            // Deseleccionar el elemento
            MenuCollectionView.SelectedItem = null;
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Aquí puedes limpiar cualquier sesión/token si lo necesitas

            // Redirige al LoginPage
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }


    }
}
