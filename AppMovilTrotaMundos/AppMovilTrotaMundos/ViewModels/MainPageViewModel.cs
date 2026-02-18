using AppMovilTrotaMundos.ViewModels;
using AppMovilTrotaMundos.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

public class MainPageViewModel : BaseViewModel
{
	public ICommand LogoutCommand { get; }

	public MainPageViewModel()
	{
		LogoutCommand = new Command(async () => await Logout());
	}

	private async Task Logout()
	{
		// Eliminar el token de acceso de manera segura
		SecureStorage.Remove("access_token");

		// Redirigir a la página de inicio de sesión
		Application.Current.MainPage = new NavigationPage(new LoginPage());
	}
}
