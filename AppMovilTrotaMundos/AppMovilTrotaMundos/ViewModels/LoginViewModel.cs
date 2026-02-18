using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using AppMovilTrotaMundos.Services;
using AppMovilTrotaMundos.Views;

namespace AppMovilTrotaMundos.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private string _usuario;
        private string _contrasena;

        public string Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        public string Contrasena
        {
            get => _contrasena;
            set => SetProperty(ref _contrasena, value);
        }

        public ICommand LoginCommand { get; }

        public LoginPageViewModel()
        {
            _apiService = new ApiService();
            LoginCommand = new Command(async () => await Login());
        }

        private async Task Login()
        {
            try
            {
                // Realizar la autenticación con el API
                var response = await _apiService.LoginAsync(Usuario, Contrasena);

                if (response != null && !string.IsNullOrEmpty(response.access_token))
                {
                    // Almacenar el token de acceso de manera segura
                    SecureStorage.Remove("access_token");

                    await SecureStorage.SetAsync("access_token", response.access_token);

                    var idUsuario = response.idUsuario;

                    // Guardar idUsuario en SecureStorage
                    SecureStorage.Remove("idUsuario"); // Elimina el valor previamente almacenado
                    await SecureStorage.SetAsync("idUsuario", idUsuario.ToString()); // Guarda el nuevo valor

                    // Llamamos al método para cargar el menú después del login
                    await CargarMenu(idUsuario);
                }
                else
                {
                    // Mostrar error si las credenciales son incorrectas
                    await Application.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrectos.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores durante la autenticación
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task CargarMenu(int idUsuario)
        {
            try
            {
                // Verificar si el idUsuario es válido
                if (idUsuario <= 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "ID de usuario no válido.", "OK");
                    return;
                }

                // Cargar el menú después de obtener el idUsuario
                var menuViewModel = new MenuViewModel();
                await menuViewModel.CargarMenu(idUsuario);

                // Cambiar la página principal al menú
                Application.Current.MainPage = new NavigationPage(new ItemsPage())
                {
                    BarBackgroundColor = Color.FromHex("#007ACC"),
                    BarTextColor = Color.White
                };
            }
            catch (Exception ex)
            {
                // Manejo de errores al cargar el menú
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error al cargar el menú. " + ex.Message, "OK");
            }
        }
    }
}
