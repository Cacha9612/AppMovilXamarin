using AppMovilTrotaMundos.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Forms.Device;


namespace AppMovilTrotaMundos
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			if (SecureStorage.GetAsync("access_token").Result != null)
			{
				MainPage = new NavigationPage(new MainPage());
			}
			else
			{
				MainPage = new NavigationPage(new LoginPage());
			}
			var styles = new ResourceDictionary();
			styles.Add("ButtonStyle", new Style(typeof(Button))
			{
				Setters = {
					new Setter { Property = Button.BackgroundColorProperty, Value = Color.Green },
					new Setter { Property = Button.TextColorProperty, Value = Color.White },
					new Setter { Property = Button.CornerRadiusProperty, Value = 5 },
					new Setter { Property = Button.PaddingProperty, Value = new Thickness(10) },
					new Setter { Property = Button.MarginProperty, Value = new Thickness(10) }
				}
			});

			Resources = styles;

			MainPage = new NavigationPage(new MainPage());
		}

		protected override async void OnStart()
		{
			var token = await SecureStorage.GetAsync("access_token");

			if (!string.IsNullOrEmpty(token))
			{
				MainPage = new NavigationPage(new MainPage());
			}
			else
			{
				MainPage = new NavigationPage(new LoginPage());
			}
		}

		protected override void OnSleep() { }

		protected override void OnResume() { }
	}
}