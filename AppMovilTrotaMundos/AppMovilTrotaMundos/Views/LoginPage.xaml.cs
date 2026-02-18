using System;
using Xamarin.Forms;
using AppMovilTrotaMundos.Services;
using Xamarin.Essentials;

namespace AppMovilTrotaMundos.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService;
        private bool isPasswordVisible = false;

        public LoginPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = ContrasenaEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor, ingrese su usuario y contraseña.", "OK");
                return;
            }

            try
            {
                LoginButton.IsEnabled = false; // Deshabilita el botón para evitar múltiples toques
                LoginButton.Text = "Iniciando...";
                LoginButton.BackgroundColor = Color.Gray;

                var response = await _apiService.LoginAsync(username, password);

                if (response != null && !string.IsNullOrEmpty(response.access_token))
                {
                    // Guardar el token de acceso y el idUsuario de manera segura
                    SecureStorage.Remove("access_token");
                    SecureStorage.Remove("idUsuario");
                    await SecureStorage.SetAsync("access_token", response.access_token);
                    await SecureStorage.SetAsync("idUsuario", response.idUsuario.ToString());

                    // Navegar a la página principal y limpiar la pila de navegación
                    Application.Current.MainPage = new NavigationPage(new MainPage())
                    {
                        BarBackgroundColor = Color.FromHex("#007ACC"),
                        BarTextColor = Color.White
                    };
                }
                else
                {
                    await DisplayAlert("Error", "Usuario o contraseña incorrectos.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            finally
            {
                LoginButton.IsEnabled = true; // Habilita el botón nuevamente
                LoginButton.Text = "Iniciar sesión";
                LoginButton.BackgroundColor = Color.Black;
            }
        }

        private void OnTogglePasswordVisibility(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            ContrasenaEntry.IsPassword = !isPasswordVisible;

            // Cambia el icono del botón de ojo
            PasswordToggleButton.Source = isPasswordVisible ? "eye_open.png" : "eye_closed.png";
        }
    }
}
