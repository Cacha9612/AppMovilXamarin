//using System;
//using System.Collections.ObjectModel;
//using System.IO;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using Xamarin.Forms;
//using AppMovilTrotaMundos.Models;
//using AppMovilTrotaMundos.Services;
//using Plugin.Media;
//using Plugin.Media.Abstractions;
//using System.Collections.Generic;
//using SkiaSharp;
//using System.Linq;


//namespace AppMovilTrotaMundos.ViewModels
//{
//	public class FormularioVehiculosViewModel : BaseViewModel
//	{
//		private readonly ApiService _apiService;
//		private Vehiculos _vehiculo;
//		private Empleados _empleado;

//		private LoginResponse _login;
//		private ObservableCollection<Clientes> _clientes;
//		private Clientes _selectedCliente;
//		private MediaFile _photo;

//		public Vehiculos Vehiculo
//		{
//			get => _vehiculo;
//			set
//			{
//				_vehiculo = value;
//				OnPropertyChanged();
//			}
//		}

//		public ObservableCollection<Clientes> Clientes
//		{
//			get => _clientes;
//			set
//			{
//				_clientes = value;
//				OnPropertyChanged();
//			}
//		}

//		public Clientes SelectedCliente
//		{
//			get => _selectedCliente;
//			set
//			{
//				_selectedCliente = value;
//				OnPropertyChanged();
//				Vehiculo.IdCliente = _selectedCliente?.IdCliente ?? 0;
//			}
//		}

//		public Empleados Empleado
//		{
//			get => _empleado;
//			set
//			{
//				_empleado = value;
//				OnPropertyChanged();
//			}
//		}
//		public LoginResponse Login
//		{
//			get => _login;
//			set
//			{
//				_login = value;
//				OnPropertyChanged();
//			}
//		}

//		public ICommand GuardarVehiculoCommand { get; }
//		public ICommand TomarFotoCommand { get; }
//		public ICommand TomarEspejoRetrovisorFotoCommand { get; }
//		public ICommand TomarEspejoIzquierdoFotoCommand { get; }
//		public ICommand TomarEspejoDerechoFotoCommand { get; }
//		public ICommand TomarAntenaFotoCommand { get; }
//		public ICommand TomarTaponesRuedasFotoCommand { get; }
//		public ICommand TomarRadioFotoCommand { get; }
//		public ICommand TomarEncendedorFotoCommand { get; }
//		public ICommand TomarGatoFotoCommand { get; }
//		public ICommand TomarHerramientaFotoCommand { get; }
//		public ICommand TomarLlantaRefaccionFotoCommand { get; }
//		public ICommand TomarLimpiadoresFotoCommand { get; }
//		public ICommand TomarPinturaRayadaFotoCommand { get; }
//		public ICommand TomarCristalesRotosFotoCommand { get; }
//		public ICommand TomarGolpesFotoCommand { get; }
//		public ICommand TomarTapetesFotoCommand { get; }
//		public ICommand TomarExtintorFotoCommand { get; }
//		public ICommand TomarTaponesGasolinaFotoCommand { get; }
//		public ICommand TomarCalaverasRotasFotoCommand { get; }
//		public ICommand TomarMoldurasCompletasFotoCommand { get; }


//		public ICommand ExpandImageCommand { get; }



//		public FormularioVehiculosViewModel()
//		{
//			_apiService = new ApiService();
//			Vehiculo = new Vehiculos();
//			_clientes = new ObservableCollection<Clientes>();
//			GuardarVehiculoCommand = new Command(async () => await OnGuardarVehiculo());
//			ExpandImageCommand = new Command<string>(OnExpandImage);

//			TomarEspejoRetrovisorFotoCommand = new Command(async () => await OnTomarFoto("Espejo_retrovisor_foto"));
//			TomarEspejoIzquierdoFotoCommand = new Command(async () => await OnTomarFoto("Espejo_izquierdo_foto"));
//			TomarEspejoDerechoFotoCommand = new Command(async () => await OnTomarFoto("Espejo_derecho_foto"));
//			TomarAntenaFotoCommand = new Command(async () => await OnTomarFoto("Antena_foto"));
//			TomarTaponesRuedasFotoCommand = new Command(async () => await OnTomarFoto("Tapones_ruedas_foto"));
//			TomarRadioFotoCommand = new Command(async () => await OnTomarFoto("Radio_foto"));
//			TomarEncendedorFotoCommand = new Command(async () => await OnTomarFoto("Encendedor_foto"));

//			TomarGatoFotoCommand = new Command(async () => await OnTomarFoto("Gato_foto"));
//			TomarHerramientaFotoCommand = new Command(async () => await OnTomarFoto("Herramienta_foto"));
//			TomarLlantaRefaccionFotoCommand = new Command(async () => await OnTomarFoto("Llanta_refaccion_foto"));
//			TomarLimpiadoresFotoCommand = new Command(async () => await OnTomarFoto("Limpiadores_foto"));
//			TomarPinturaRayadaFotoCommand = new Command(async () => await OnTomarFoto("Pintura_rayada_foto"));
//			TomarCristalesRotosFotoCommand = new Command(async () => await OnTomarFoto("Cristales_rotos_foto"));

//			TomarGolpesFotoCommand = new Command(async () => await OnTomarFoto("Golpes_foto"));
//			TomarTapetesFotoCommand = new Command(async () => await OnTomarFoto("Tapetes_foto"));
//			TomarExtintorFotoCommand = new Command(async () => await OnTomarFoto("Extintor_foto"));
//			TomarTaponesGasolinaFotoCommand = new Command(async () => await OnTomarFoto("Tapones_gasolina_foto"));
//			TomarCalaverasRotasFotoCommand = new Command(async () => await OnTomarFoto("Calaveras_rotas_foto"));
//			TomarMoldurasCompletasFotoCommand = new Command(async () => await OnTomarFoto("Molduras_completas_foto"));


//			LoadClientesAsync();
//		}

//		private async void OnExpandImage(string imageSource)
//		{
//			if (string.IsNullOrEmpty(imageSource))
//			{
//				// Mostrar un mensaje de error o manejar el caso donde la imagen no está disponible
//				await Application.Current.MainPage.DisplayAlert("Error", "La imagen no es accesible", "OK");
//				return;
//			}

//			// Aquí puedes mostrar la imagen en una nueva página o ventana modal
//			await Application.Current.MainPage.Navigation.PushAsync(new Views.ImageDetailPage(imageSource));
//		}


//		private async Task LoadClientesAsync()
//		{
//			var clientes = await _apiService.GetAsync<List<Clientes>>("api/clientes");
//			Clientes = new ObservableCollection<Clientes>(clientes);
//		}

//		private async Task OnGuardarVehiculo()
//		{
//			try
//			{
//				// Validación de campos obligatorios
//				if (string.IsNullOrWhiteSpace(Vehiculo.Marca) ||
//					string.IsNullOrWhiteSpace(Vehiculo.Modelo) ||
//					string.IsNullOrWhiteSpace(Vehiculo.Color) ||
//					string.IsNullOrWhiteSpace(Vehiculo.No_serie) ||
//					string.IsNullOrWhiteSpace(Vehiculo.Placa) ||
//					string.IsNullOrWhiteSpace(Vehiculo.Tipo) ||
//					string.IsNullOrWhiteSpace(Vehiculo.Motor) ||
//					string.IsNullOrWhiteSpace(Vehiculo.Kms))
//				{
//					await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son requeridos.", "OK");
//					return;
//				}

//				// Inicializar listas de fotos si son nulas
//				Vehiculo.Espejo_retrovisor_foto = Vehiculo.Espejo_retrovisor_foto ?? new List<string>();
//				Vehiculo.Espejo_izquierdo_foto = Vehiculo.Espejo_izquierdo_foto ?? new List<string>();
//				Vehiculo.Espejo_derecho_foto = Vehiculo.Espejo_derecho_foto ?? new List<string>();
//				Vehiculo.Antena_foto = Vehiculo.Antena_foto ?? new List<string>();
//				Vehiculo.Tapones_ruedas_foto = Vehiculo.Tapones_ruedas_foto ?? new List<string>();
//				Vehiculo.Radio_foto = Vehiculo.Radio_foto ?? new List<string>();
//				Vehiculo.Encendedor_foto = Vehiculo.Encendedor_foto ?? new List<string>();
//				Vehiculo.Gato_foto = Vehiculo.Gato_foto ?? new List<string>();
//				Vehiculo.Herramienta_foto = Vehiculo.Herramienta_foto ?? new List<string>();
//				Vehiculo.Llanta_refaccion_foto = Vehiculo.Llanta_refaccion_foto ?? new List<string>();
//				Vehiculo.Limpiadores_foto = Vehiculo.Limpiadores_foto ?? new List<string>();
//				Vehiculo.Pintura_rayada_foto = Vehiculo.Pintura_rayada_foto ?? new List<string>();
//				Vehiculo.Cristales_rotos_foto = Vehiculo.Cristales_rotos_foto ?? new List<string>();
//				Vehiculo.Golpes_foto = Vehiculo.Golpes_foto ?? new List<string>();
//				Vehiculo.Tapetes_foto = Vehiculo.Tapetes_foto ?? new List<string>();
//				Vehiculo.Extintor_foto = Vehiculo.Extintor_foto ?? new List<string>();
//				Vehiculo.Tapones_gasolina_foto = Vehiculo.Tapones_gasolina_foto ?? new List<string>();
//				Vehiculo.Calaveras_rotas_foto = Vehiculo.Calaveras_rotas_foto ?? new List<string>();
//				Vehiculo.Molduras_completas_foto = Vehiculo.Molduras_completas_foto ?? new List<string>();

//				// Manejar la lógica de agregar múltiples fotos al campo
//				// Suponiendo que tienes una nueva foto para agregar en cada campo, que puede ser obtenida dinámicamente
//				string nuevaFotoBase64 = await ObtenerFotoBase64Async();  // Suponiendo que esta función obtiene la nueva foto

//				// Agregar la nueva foto a la lista de cada campo si es que la hay
//				if (!string.IsNullOrWhiteSpace(nuevaFotoBase64))
//				{
//					Vehiculo.Espejo_retrovisor_foto.Add(nuevaFotoBase64);
//					Vehiculo.Espejo_izquierdo_foto.Add(nuevaFotoBase64);
//					Vehiculo.Espejo_derecho_foto.Add(nuevaFotoBase64);
//					Vehiculo.Antena_foto.Add(nuevaFotoBase64);
//					Vehiculo.Tapones_ruedas_foto.Add(nuevaFotoBase64);
//					Vehiculo.Radio_foto.Add(nuevaFotoBase64);
//					Vehiculo.Encendedor_foto.Add(nuevaFotoBase64);
//					Vehiculo.Gato_foto.Add(nuevaFotoBase64);
//					Vehiculo.Herramienta_foto.Add(nuevaFotoBase64);
//					Vehiculo.Llanta_refaccion_foto.Add(nuevaFotoBase64);
//					Vehiculo.Limpiadores_foto.Add(nuevaFotoBase64);
//					Vehiculo.Pintura_rayada_foto.Add(nuevaFotoBase64);
//					Vehiculo.Cristales_rotos_foto.Add(nuevaFotoBase64);
//					Vehiculo.Golpes_foto.Add(nuevaFotoBase64);
//					Vehiculo.Tapetes_foto.Add(nuevaFotoBase64);
//					Vehiculo.Extintor_foto.Add(nuevaFotoBase64);
//					Vehiculo.Tapones_gasolina_foto.Add(nuevaFotoBase64);
//					Vehiculo.Calaveras_rotas_foto.Add(nuevaFotoBase64);
//					Vehiculo.Molduras_completas_foto.Add(nuevaFotoBase64);
//				}

//				// Convertir listas de fotos a cadenas de Base64 separadas por comas
//				Vehiculo.Espejo_retrovisor_foto = string.Join(",", Vehiculo.Espejo_retrovisor_foto);
//				Vehiculo.Espejo_izquierdo_foto = string.Join(",", Vehiculo.Espejo_izquierdo_foto);
//				Vehiculo.Espejo_derecho_foto = string.Join(",", Vehiculo.Espejo_derecho_foto);
//				Vehiculo.Antena_foto = string.Join(",", Vehiculo.Antena_foto);
//				Vehiculo.Tapones_ruedas_foto = string.Join(",", Vehiculo.Tapones_ruedas_foto);
//				Vehiculo.Radio_foto = string.Join(",", Vehiculo.Radio_foto);
//				Vehiculo.Encendedor_foto = string.Join(",", Vehiculo.Encendedor_foto);
//				Vehiculo.Gato_foto = string.Join(",", Vehiculo.Gato_foto);
//				Vehiculo.Herramienta_foto = string.Join(",", Vehiculo.Herramienta_foto);
//				Vehiculo.Llanta_refaccion_foto = string.Join(",", Vehiculo.Llanta_refaccion_foto);
//				Vehiculo.Limpiadores_foto = string.Join(",", Vehiculo.Limpiadores_foto);
//				Vehiculo.Pintura_rayada_foto = string.Join(",", Vehiculo.Pintura_rayada_foto);
//				Vehiculo.Cristales_rotos_foto = string.Join(",", Vehiculo.Cristales_rotos_foto);
//				Vehiculo.Golpes_foto = string.Join(",", Vehiculo.Golpes_foto);
//				Vehiculo.Tapetes_foto = string.Join(",", Vehiculo.Tapetes_foto);
//				Vehiculo.Extintor_foto = string.Join(",", Vehiculo.Extintor_foto);
//				Vehiculo.Tapones_gasolina_foto = string.Join(",", Vehiculo.Tapones_gasolina_foto);
//				Vehiculo.Calaveras_rotas_foto = string.Join(",", Vehiculo.Calaveras_rotas_foto);
//				Vehiculo.Molduras_completas_foto = string.Join(",", Vehiculo.Molduras_completas_foto);

//				// Validar el IdUsuario
//				if (AppSettings.IdUsuario > 0)
//				{
//					Vehiculo.Id_empleado = AppSettings.IdUsuario;
//				}
//				else
//				{
//					await Application.Current.MainPage.DisplayAlert("Error", "El IdUsuario no está disponible.", "OK");
//					return;
//				}

//				// Guardar el vehículo
//				await _apiService.AddVehiculoAsync(Vehiculo);
//				await Application.Current.MainPage.Navigation.PopAsync();
//				await Application.Current.MainPage.DisplayAlert("Éxito", "Vehículo agregado correctamente.", "OK");
//			}
//			catch (Exception ex)
//			{
//				await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
//			}
//		}



//		//private async Task OnGuardarVehiculo()
//		//{
//		//	try
//		//	{
//		//		if (string.IsNullOrWhiteSpace(Vehiculo.Marca) ||
//		//			string.IsNullOrWhiteSpace(Vehiculo.Modelo) ||
//		//			string.IsNullOrWhiteSpace(Vehiculo.Color) ||
//		//			string.IsNullOrWhiteSpace(Vehiculo.No_serie) ||
//		//			string.IsNullOrWhiteSpace(Vehiculo.Placa) ||
//		//			string.IsNullOrWhiteSpace(Vehiculo.Tipo) ||
//		//			string.IsNullOrWhiteSpace(Vehiculo.Motor) ||
//		//			string.IsNullOrWhiteSpace(Vehiculo.Kms))
//		//		{
//		//			await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son requeridos.", "OK");
//		//			return;
//		//		}

//		//		// Verifica y asigna cadena vacía a las propiedades de imagen si no hay foto

//		//		Vehiculo.Espejo_retrovisor_foto = Vehiculo.Espejo_retrovisor_foto ?? new List<string>();
//		//		Vehiculo.Espejo_izquierdo_foto =  (Vehiculo.Espejo_izquierdo_foto) ?? new List<string>();
//		//		Vehiculo.Espejo_derecho_foto = (Vehiculo.Espejo_derecho_foto) ?? new List<string>();
//		//		Vehiculo.Antena_foto = (Vehiculo.Antena_foto) ?? new List<string>();
//		//		Vehiculo.Tapones_ruedas_foto = (Vehiculo.Tapones_ruedas_foto) ?? new List<string>();
//		//		Vehiculo.Radio_foto = (Vehiculo.Radio_foto) ?? new List<string>();
//		//		Vehiculo.Encendedor_foto = (Vehiculo.Encendedor_foto) ?? new List<string>();
//		//		Vehiculo.Gato_foto = (Vehiculo.Gato_foto) ?? new List<string>();
//		//		Vehiculo.Herramienta_foto = (Vehiculo.Herramienta_foto) ?? new List<string>();
//		//		Vehiculo.Llanta_refaccion_foto = (Vehiculo.Llanta_refaccion_foto) ?? new List<string>();
//		//		Vehiculo.Limpiadores_foto =  (Vehiculo.Limpiadores_foto) ?? new List<string>();
//		//		Vehiculo.Pintura_rayada_foto =  (Vehiculo.Pintura_rayada_foto) ?? new List<string>();
//		//		Vehiculo.Cristales_rotos_foto =  (Vehiculo.Cristales_rotos_foto) ?? new List<string>();
//		//		Vehiculo.Golpes_foto = (Vehiculo.Golpes_foto) ?? new List<string>();
//		//		Vehiculo.Tapetes_foto = (Vehiculo.Tapetes_foto) ?? new List<string>();
//		//		Vehiculo.Extintor_foto = (Vehiculo.Extintor_foto) ?? new List<string>();
//		//		Vehiculo.Tapones_gasolina_foto =  (Vehiculo.Tapones_gasolina_foto) ?? new List<string>();
//		//		Vehiculo.Calaveras_rotas_foto =  (Vehiculo.Calaveras_rotas_foto) ?? new List<string>();
//		//		Vehiculo.Molduras_completas_foto = (Vehiculo.Molduras_completas_foto) ?? new List<string>();



//		//		// Convertir las cadenas de imágenes a listas
//		//		Vehiculo.Espejo_retrovisor_foto = string.IsNullOrWhiteSpace(Vehiculo.Espejo_retrovisor_foto) ? new List<string>(): Vehiculo.Espejo_retrovisor_foto.Split(',').ToList();

//		//		Vehiculo.Espejo_izquierdo_foto = string.IsNullOrWhiteSpace(Vehiculo.Espejo_izquierdo_foto)? new List<string>(): Vehiculo.Espejo_izquierdo_foto.Split(',').ToList();

//		//		Vehiculo.Espejo_derecho_foto = string.IsNullOrWhiteSpace(Vehiculo.Espejo_derecho_foto)	? new List<string>(): Vehiculo.Espejo_derecho_foto.Split(',').ToList();

//		//		Vehiculo.Antena_foto = string.IsNullOrWhiteSpace(Vehiculo.Antena_foto)? new List<string>(): Vehiculo.Antena_foto.Split(',').ToList();

//		//		Vehiculo.Radio_foto = string.IsNullOrWhiteSpace(Vehiculo.Radio_foto)? new List<string>(): Vehiculo.Radio_foto.Split(',').ToList();

//		//		Vehiculo.Encendedor_foto = string.IsNullOrWhiteSpace(Vehiculo.Encendedor_foto)	? new List<string>(): Vehiculo.Encendedor_foto.Split(',').ToList();

//		//		Vehiculo.Gato_foto = string.IsNullOrWhiteSpace(Vehiculo.Gato_foto)? new List<string>(): Vehiculo.Gato_foto.Split(',').ToList();

//		//		Vehiculo.Herramienta_foto = string.IsNullOrWhiteSpace(Vehiculo.Herramienta_foto)? new List<string>(): Vehiculo.Herramienta_foto.Split(',').ToList();

//		//		Vehiculo.Llanta_refaccion_foto = string.IsNullOrWhiteSpace(Vehiculo.Llanta_refaccion_foto)? new List<string>(): Vehiculo.Llanta_refaccion_foto.Split(',').ToList();

//		//		Vehiculo.Limpiadores_foto = string.IsNullOrWhiteSpace(Vehiculo.Limpiadores_foto)? new List<string>(): Vehiculo.Limpiadores_foto.Split(',').ToList();

//		//		Vehiculo.Pintura_rayada_foto = string.IsNullOrWhiteSpace(Vehiculo.Pintura_rayada_foto)? new List<string>(): Vehiculo.Pintura_rayada_foto.Split(',').ToList();

//		//		Vehiculo.Cristales_rotos_foto = string.IsNullOrWhiteSpace(Vehiculo.Cristales_rotos_foto)? new List<string>(): Vehiculo.Cristales_rotos_foto.Split(',').ToList();

//		//		Vehiculo.Golpes_foto = string.IsNullOrWhiteSpace(Vehiculo.Golpes_foto)? new List<string>(): Vehiculo.Golpes_foto.Split(',').ToList();
//		//		Vehiculo.Tapetes_foto = string.IsNullOrWhiteSpace(Vehiculo.Tapetes_foto)? new List<string>(): Vehiculo.Tapetes_foto.Split(',').ToList();

//		//		Vehiculo.Extintor_foto = string.IsNullOrWhiteSpace(Vehiculo.Extintor_foto)? new List<string>(): Vehiculo.Extintor_foto.Split(',').ToList();

//		//		Vehiculo.Tapones_gasolina_foto = string.IsNullOrWhiteSpace(Vehiculo.Tapones_gasolina_foto)? new List<string>(): Vehiculo.Tapones_gasolina_foto.Split(',').ToList();

//		//		Vehiculo.Calaveras_rotas_foto = string.IsNullOrWhiteSpace(Vehiculo.Calaveras_rotas_foto)? new List<string>(): Vehiculo.Calaveras_rotas_foto.Split(',').ToList();

//		//		Vehiculo.Molduras_completas_foto = string.IsNullOrWhiteSpace(Vehiculo.Molduras_completas_foto)? new List<string>(): Vehiculo.Molduras_completas_foto.Split(',').ToList();



//		//		// Convertir listas a cadenas
//		//		Vehiculo.Espejo_retrovisor_foto = Vehiculo.Espejo_retrovisor_foto == null || !Vehiculo.Espejo_retrovisor_foto.Any()? string.Empty: string.Join(",", Vehiculo.Espejo_retrovisor_foto);
//		//		Vehiculo.Espejo_izquierdo_foto = Vehiculo.Espejo_izquierdo_foto == null || !Vehiculo.Espejo_izquierdo_foto.Any()? string.Empty: string.Join(",", Vehiculo.Espejo_izquierdo_foto);
//		//		Vehiculo.Antena_foto = Vehiculo.Antena_foto == null || !Vehiculo.Antena_foto.Any()
//		//		? string.Empty
//		//		: string.Join(",", Vehiculo.Antena_foto);
//		//		Vehiculo.Tapones_ruedas_foto = Vehiculo.Tapones_ruedas_foto == null || !Vehiculo.Tapones_ruedas_foto.Any() ? string.Empty
//		//		: string.Join(",", Vehiculo.Tapones_ruedas_foto);
//		//		Vehiculo.Radio_foto = Vehiculo.Radio_foto == null || !Vehiculo.Radio_foto.Any()? string.Empty: string.Join(",", Vehiculo.Radio_foto);
//		//		Vehiculo.Encendedor_foto = Vehiculo.Encendedor_foto == null || !Vehiculo.Encendedor_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Encendedor_foto);
//		//		Vehiculo.Gato_foto = Vehiculo.Gato_foto == null || !Vehiculo.Gato_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Gato_foto);
//		//		Vehiculo.Herramienta_foto = Vehiculo.Herramienta_foto == null || !Vehiculo.Herramienta_foto.Any() ? string.Empty : string.Join(",", Vehiculo.Herramienta_foto);
//		//		Vehiculo.Llanta_refaccion_foto = Vehiculo.Llanta_refaccion_foto == null || !Vehiculo.Llanta_refaccion_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Llanta_refaccion_foto);
//		//		Vehiculo.Limpiadores_foto = Vehiculo.Limpiadores_foto == null || !Vehiculo.Limpiadores_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Limpiadores_foto);
//		//		Vehiculo.Pintura_rayada_foto = Vehiculo.Pintura_rayada_foto == null || !Vehiculo.Pintura_rayada_foto.Any() ? string.Empty : string.Join(",", Vehiculo.Pintura_rayada_foto);
//		//		Vehiculo.Cristales_rotos_foto = Vehiculo.Cristales_rotos_foto == null || !Vehiculo.Cristales_rotos_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Cristales_rotos_foto);
//		//		Vehiculo.Golpes_foto = Vehiculo.Golpes_foto == null || !Vehiculo.Golpes_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Golpes_foto);
//		//		Vehiculo.Tapetes_foto = Vehiculo.Tapetes_foto == null || !Vehiculo.Tapetes_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Tapetes_foto);
//		//		Vehiculo.Extintor_foto = Vehiculo.Extintor_foto == null || !Vehiculo.Extintor_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Extintor_foto);
//		//		Vehiculo.Tapones_gasolina_foto = Vehiculo.Tapones_gasolina_foto == null || !Vehiculo.Tapones_gasolina_foto.Any() ? string.Empty : string.Join(",", Vehiculo.Tapones_gasolina_foto);
//		//		Vehiculo.Calaveras_rotas_foto = Vehiculo.Calaveras_rotas_foto == null || !Vehiculo.Calaveras_rotas_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Calaveras_rotas_foto);
//		//		Vehiculo.Molduras_completas_foto = Vehiculo.Molduras_completas_foto == null || !Vehiculo.Molduras_completas_foto.Any() ? string.Empty: string.Join(",", Vehiculo.Molduras_completas_foto);
//		//		// Repetir para las demás propiedades


//		//		// Repetir para las demás propiedades


//		//		if (AppSettings.IdUsuario > 0)
//		//		{
//		//			Vehiculo.Id_empleado = AppSettings.IdUsuario;
//		//		}
//		//		else
//		//		{
//		//			await Application.Current.MainPage.DisplayAlert("Error", "El IdUsuario no está disponible.", "OK");
//		//			return;
//		//		}

//		//		await _apiService.AddVehiculoAsync(Vehiculo);
//		//		await Application.Current.MainPage.Navigation.PopAsync();
//		//		await Application.Current.MainPage.DisplayAlert("Éxito", "Vehículo agregado correctamente.", "OK");
//		//	}
//		//	catch (Exception ex)
//		//	{
//		//		await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
//		//	}
//		//}



//		private async Task OnTomarFoto(string propiedad)
//		{
//			await CrossMedia.Current.Initialize();

//			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
//			{
//				await Application.Current.MainPage.DisplayAlert("No Camera", "No camera available.", "OK");
//				return;
//			}

//			var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
//			{
//				PhotoSize = PhotoSize.Small,
//				Directory = "Vehiculo",
//				Name = $"{propiedad}.jpg"
//			});

//			if (photo != null)
//			{
//				// Redimensionar y comprimir la imagen
//				var base64Compressed = CompressImage(photo, 500, 200); // Ajusta el tamaño y calidad

//				// Asigna la cadena Base64 comprimida a la propiedad correspondiente
//				switch (propiedad)
//				{
//					case "Espejo_retrovisor_foto":
//						Vehiculo.Espejo_retrovisor_foto.Add(base64Compressed);
//						break;
//					case "Espejo_izquierdo_foto":
//						Vehiculo.Espejo_izquierdo_foto.Add(base64Compressed);
//						break;
//					case "Espejo_derecho_foto":
//						Vehiculo.Espejo_derecho_foto.Add(base64Compressed);
//						break;
//					case "Antena_foto":
//						Vehiculo.Antena_foto.Add(base64Compressed);
//						break;
//					case "Tapones_ruedas_foto":
//						Vehiculo.Tapones_ruedas_foto.Add(base64Compressed);
//						break;
//					case "Radio_foto":
//						Vehiculo.Radio_foto.Add(base64Compressed);
//						break;
//					case "Encendedor_foto":
//						Vehiculo.Encendedor_foto.Add(base64Compressed);
//						break;
//					case "Gato_foto":
//						Vehiculo.Gato_foto.Add(base64Compressed);
//						break;
//					case "Herramienta_foto":
//						Vehiculo.Herramienta_foto.Add(base64Compressed);
//						break;
//					case "Llanta_refaccion_foto":
//						Vehiculo.Llanta_refaccion_foto.Add(base64Compressed);
//						break;
//					case "Limpiadores_foto":
//						Vehiculo.Limpiadores_foto.Add(base64Compressed);
//						break;
//					case "Pintura_rayada_foto":
//						Vehiculo.Pintura_rayada_foto.Add(base64Compressed);
//						break;
//					case "Cristales_rotos_foto":
//						Vehiculo.Cristales_rotos_foto.Add(base64Compressed);
//						break;
//					case "Golpes_foto":
//						Vehiculo.Golpes_foto.Add(base64Compressed);
//						break;
//					case "Tapetes_foto":
//						Vehiculo.Tapetes_foto.Add(base64Compressed);
//						break;
//					case "Extintor_foto":
//						Vehiculo.Extintor_foto.Add(base64Compressed);
//						break;
//					case "Tapones_gasolina_foto":
//						Vehiculo.Tapones_gasolina_foto.Add(base64Compressed);
//						break;
//					case "Calaveras_rotas_foto":
//						Vehiculo.Calaveras_rotas_foto.Add(base64Compressed);
//						break;
//					case "Molduras_completas_foto":
//						Vehiculo.Molduras_completas_foto.Add(base64Compressed);
//						break;
//				}

//				OnPropertyChanged(nameof(Vehiculo));
//			}
//		}

//		private async Task<string> ObtenerFotoBase64Async()
//		{
//			try
//			{
//				// Inicializar la biblioteca de medios si no ha sido hecha
//				await CrossMedia.Current.Initialize();

//				// Verificar si la cámara está disponible
//				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
//				{
//					await Application.Current.MainPage.DisplayAlert("Error", "La cámara no está disponible.", "OK");
//					return null;
//				}

//				// Tomar la foto
//				var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
//				{
//					PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium, // Ajustar el tamaño de la imagen si es necesario
//					CompressionQuality = 75, // Ajustar la calidad de la compresión si es necesario
//					Directory = "FotosVehiculos",
//					Name = $"vehiculo_{DateTime.Now:yyyyMMdd_HHmmss}.jpg"
//				});

//				// Verificar si se tomó la foto
//				if (file == null)
//					return null;

//				// Leer la foto en un array de bytes
//				byte[] imageBytes;
//				using (var memoryStream = new MemoryStream())
//				{
//					file.GetStream().CopyTo(memoryStream);
//					file.Dispose(); // Liberar el archivo después de obtener la foto
//					imageBytes = memoryStream.ToArray();
//				}

//				// Convertir los bytes de la imagen a Base64
//				string base64Image = Convert.ToBase64String(imageBytes);
//				return base64Image;
//			}
//			catch (Exception ex)
//			{
//				await Application.Current.MainPage.DisplayAlert("Error", $"Hubo un error al capturar la foto: {ex.Message}", "OK");
//				return null;
//			}
//		}



//		private string CompressImage(MediaFile photo, int maxWidth, int maxHeight)
//		{
//			using (var stream = photo.GetStream())
//			{
//				using (var originalBitmap = SKBitmap.Decode(stream))
//				{
//					// Calcula las dimensiones nuevas para la imagen
//					var ratio = Math.Min((float)maxWidth / originalBitmap.Width, (float)maxHeight / originalBitmap.Height);
//					var width = (int)(originalBitmap.Width * ratio);
//					var height = (int)(originalBitmap.Height * ratio);

//					var imageInfo = new SKImageInfo(width, height);
//					using (var resizedBitmap = originalBitmap.Resize(imageInfo, SKFilterQuality.High))
//					{
//						using (var image = SKImage.FromBitmap(resizedBitmap))
//						{
//							using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 75)) // Ajusta la calidad según tus necesidades
//							{
//								return Convert.ToBase64String(data.ToArray());
//							}
//						}
//					}
//				}
//			}
//		}

//	}
//}


using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppMovilTrotaMundos.Models;
using AppMovilTrotaMundos.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using SkiaSharp;
using System.Linq;
using System.Globalization;
using AppMovilTrotaMundos.Views;
using Xamarin.Essentials;
namespace AppMovilTrotaMundos.ViewModels
{
    public class FormularioVehiculosViewModel : BaseViewModel
    {
        private LoginResponse _login;
        private MediaFile _photo;
        private readonly ApiService _apiService;
        private Models.Vehiculos _vehiculo;
        private Models.Empleados _empleado;
        private ObservableCollection<Models.Clientes> _clientes;
        private ObservableCollection<Models.Flotillas> _flotillas;
        private Models.Clientes _selectedCliente;
        private Models.Flotillas _selectedFlotilla;
        public Models.CheckList CheckList { get; set; }



        // Propiedades para enlazar con los Pickers


        public ObservableCollection<Models.Clientes> Clientes { get => _clientes; set => SetProperty(ref _clientes, value); }
        public ObservableCollection<Models.Flotillas> Flotillas { get => _flotillas; set => SetProperty(ref _flotillas, value); }

        private bool _isEditing;

        private int _estadoSemaforo;
        public int EstadoSemaforo
        {
            get => _estadoSemaforo;
            set
            {
                if (_estadoSemaforo != value)
                {
                    _estadoSemaforo = value;
                    OnPropertyChanged(); // Notifica el cambio a la UI
                    OnPropertyChanged(nameof(SemaforoIcon)); // Para actualizar la imagen del semáforo
                }
            }
        }


        // Para que puedas mostrar el icono del semáforo en la UI:
        public string SemaforoIcon
        {
            get
            {
                switch (EstadoSemaforo)
                {
                    case 0:
                        return "semaforo_rojo.png";
                    case 1:
                        return "semaforo_amarillo.png";
                    case 2:
                        return "semaforo_verde.png";
                    default:
                        return "semaforo_gris.png";
                }
            }
        }

        private void ActualizarEstadoSemaforo()
        {
            if (CheckList == null) return;

            bool tieneRojo = false;
            bool tieneAmarillo = false;

            var props = typeof(AppMovilTrotaMundos.Models.CheckList).GetProperties();

            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(int) &&
                    !prop.Name.EndsWith("_observacion") &&
                    !prop.Name.EndsWith("_foto") &&
                    !prop.Name.Equals("IdEmpleado") &&
                    !prop.Name.Equals("IdVehiculo") &&
                    !prop.Name.Equals("Id_ordendeservicio") &&
                    !prop.Name.Equals("Id") &&
                    !prop.Name.Equals("Activo"))
                {
                    var valor = (int)prop.GetValue(CheckList); // <-- Aquí usas la instancia

                    if (valor == 0)
                    {
                        tieneRojo = true;
                        break;
                    }
                    else if (valor == 1)
                    {
                        tieneAmarillo = true;
                    }
                }
            }

            if (tieneRojo)
                EstadoSemaforo = 0;
            else if (tieneAmarillo)
                EstadoSemaforo = 1;
            else
                EstadoSemaforo = 2;
        }




        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(); // Notificar cambios
            }
        }

        public Models.Clientes SelectedCliente
        {
            get => _selectedCliente;
            set
            {
                if (SetProperty(ref _selectedCliente, value))
                {
                    Vehiculo.Id_Cliente = value?.IdCliente ?? 0;
                }
            }
        }



        
        public Models.Flotillas SelectedFlotilla
        {
            get => _selectedFlotilla;
            set
            {
                if (SetProperty(ref _selectedFlotilla, value))
                {
                    Vehiculo.IdFlotilla = value?.IdFlotilla ?? 0;

                    // Notifica que MostrarNumeroEconomico ha cambiado
                    OnPropertyChanged(nameof(MostrarNumeroEconomico));

                    if (!string.IsNullOrEmpty(value?.Encargado))
                    {
                        var clienteEncargado = Clientes.FirstOrDefault(c => c.Nombre.Equals(value.Encargado, StringComparison.OrdinalIgnoreCase));

                        if (clienteEncargado != null)
                        {
                            SelectedCliente = clienteEncargado;
                        }
                        else
                        {
                            var nuevoCliente = new Models.Clientes { Nombre = value.Encargado };
                            Clientes.Add(nuevoCliente);
                            SelectedCliente = nuevoCliente;
                        }
                    }
                }
            }
        }

        public bool MostrarNumeroEconomico => SelectedFlotilla != null;






        public Models.Vehiculos Vehiculo
        {
            get => _vehiculo;
            set
            {
                _vehiculo = value;
                OnPropertyChanged();
            }
        }

     

        public Models.Empleados Empleado
        {
            get => _empleado;
            set
            {
                _empleado = value;
                OnPropertyChanged();
            }
        }

        public LoginResponse Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public ICommand GrabarVideoCommand { get; }

		public ICommand GuardarVehiculoCommand { get; }
        public ICommand ModificarVehiculoCommand { get; }
        public ICommand TomarFotoCommand { get; }
        public ICommand TomarAcumuladorFotoCommand { get; }
        public ICommand TomarMotorVehiculoFotoCommand { get; }
        public ICommand TomarEspejoRetrovisorFotoCommand { get; }
		public ICommand TomarEspejoIzquierdoFotoCommand { get; }
		public ICommand TomarEspejoDerechoFotoCommand { get; }
		public ICommand TomarAntenaFotoCommand { get; }
		public ICommand TomarTaponesRuedasFotoCommand { get; }
		public ICommand TomarRadioFotoCommand { get; }
		public ICommand TomarEncendedorFotoCommand { get; }
		public ICommand TomarGatoFotoCommand { get; }
		public ICommand TomarHerramientaFotoCommand { get; }
		public ICommand TomarLlantaRefaccionFotoCommand { get; }
		public ICommand TomarLimpiadoresFotoCommand { get; }
		public ICommand TomarPinturaRayadaFotoCommand { get; }
		public ICommand TomarCristalesRotosFotoCommand { get; }
		public ICommand TomarGolpesFotoCommand { get; }
		public ICommand TomarTapetesFotoCommand { get; }
		public ICommand TomarExtintorFotoCommand { get; }
		public ICommand TomarTaponesGasolinaFotoCommand { get; }
		public ICommand TomarCalaverasRotasFotoCommand { get; }
		public ICommand TomarMoldurasCompletasFotoCommand { get; }

        public ICommand TomarAcumuladorVideoCommand { get; }
        public ICommand TomarMotorVehiculoVideoCommand { get; }
        public ICommand TomarEspejoRetrovisorVideoCommand { get; }
		public ICommand TomarEspejoIzquierdoVideoCommand { get; }
		public ICommand TomarEspejoDerechoVideoCommand { get; }
		public ICommand TomarAntenaVideoCommand { get; }
		public ICommand TomarTaponesRuedasVideoCommand { get; }
		public ICommand TomarRadioVideoCommand { get; }
		public ICommand TomarEncendedorVideoCommand { get; }
		public ICommand TomarGatoVideoCommand { get; }
		public ICommand TomarHerramientaVideoCommand { get; }
		public ICommand TomarLlantaRefaccionVideoCommand { get; }
		public ICommand TomarLimpiadoresVideoCommand { get; }
		public ICommand TomarPinturaRayadaVideoCommand { get; }
		public ICommand TomarCristalesRotosVideoCommand { get; }
		public ICommand TomarGolpesVideoCommand { get; }
		public ICommand TomarTapetesVideoCommand { get; }
		public ICommand TomarExtintorVideoCommand { get; }
		public ICommand TomarTaponesGasolinaVideoCommand { get; }
		public ICommand TomarCalaverasRotasVideoCommand { get; }
		public ICommand TomarMoldurasCompletasVideoCommand { get; }

        public ICommand TomarPanelInstrumentosVideoCommand { get; }
        public ICommand TomarLadoIzquierdoVideoCommand { get; }
        public ICommand TomarLadoIzquierdoInfVideoCommand{ get; }
        public ICommand TomarLadoDerechoVideoCommand { get; }
        public ICommand TomarLadoDerechoInfVideoCommand { get; }
        public ICommand TomarTableroVideoCommand { get; }
        public ICommand TomarGuanteraVideoCommand{ get; }
        public ICommand TomarConsolaVideoCommand { get; }
        public ICommand TomarLadoFrontalVideoCommand { get; }
        public ICommand TomarLadoTraseroVideoCommand { get; }
        public ICommand TomarCajuelaVideoCommand { get; }
        
        public ICommand TomarNumeroEconomicoVideoCommand { get; }

        public ICommand TomarPanelInstrumentosFotoCommand { get; }
        public ICommand TomarLadoIzquierdoFotoCommand { get; }
        public ICommand TomarLadoIzquierdoInfFotoCommand { get; }
        public ICommand TomarLadoDerechoFotoCommand { get; }
        public ICommand TomarLadoDerechoInfFotoCommand { get; }
        public ICommand TomarTableroFotoCommand { get; }
        public ICommand TomarGuanteraFotoCommand { get; }
        public ICommand TomarConsolaFotoCommand { get; }

        public ICommand TomarLadoFrontalFotoCommand { get; }
        public ICommand TomarLadoTraseroFotoCommand { get; }
        public ICommand TomarCajuelaFotoCommand { get; }
        public ICommand TomarNumeroEconomicoFotoCommand { get; }




        public ICommand ExpandImageCommand { get; }

       



        public FormularioVehiculosViewModel()
		{
			_apiService = new ApiService();
			Vehiculo = new Models.Vehiculos();
			_clientes = new ObservableCollection<Models.Clientes>();
            _flotillas = new ObservableCollection<Models.Flotillas>();



            ActualizarEstadoSemaforo();
            GuardarVehiculoCommand = new Command(async () => await OnGuardarVehiculo());
			ModificarVehiculoCommand = new Command(async () => await OnModificarVehiculo());
			ExpandImageCommand = new Command<string>(OnExpandImage);



			GrabarVideoCommand = new Command<string>(async (propiedad) => await OnTomarVideo(propiedad));
            TomarAcumuladorFotoCommand = new Command(async () => await OnTomarFoto("Acumulador_foto"));
            TomarMotorVehiculoFotoCommand = new Command(async () => await OnTomarFoto("MotorVehiculo_foto"));
            TomarEspejoRetrovisorFotoCommand = new Command(async () => await OnTomarFoto("Espejo_retrovisor_foto"));
			TomarEspejoIzquierdoFotoCommand = new Command(async () => await OnTomarFoto("Espejo_izquierdo_foto"));
			TomarEspejoDerechoFotoCommand = new Command(async () => await OnTomarFoto("Espejo_derecho_foto"));
			TomarAntenaFotoCommand = new Command(async () => await OnTomarFoto("Antena_foto"));
			TomarTaponesRuedasFotoCommand = new Command(async () => await OnTomarFoto("Tapones_ruedas_foto"));
			TomarRadioFotoCommand = new Command(async () => await OnTomarFoto("Radio_foto"));
			TomarEncendedorFotoCommand = new Command(async () => await OnTomarFoto("Encendedor_foto"));

			TomarGatoFotoCommand = new Command(async () => await OnTomarFoto("Gato_foto"));
			TomarHerramientaFotoCommand = new Command(async () => await OnTomarFoto("Herramienta_foto"));
			TomarLlantaRefaccionFotoCommand = new Command(async () => await OnTomarFoto("Llanta_refaccion_foto"));
			TomarLimpiadoresFotoCommand = new Command(async () => await OnTomarFoto("Limpiadores_foto"));
			TomarPinturaRayadaFotoCommand = new Command(async () => await OnTomarFoto("Pintura_rayada_foto"));
			TomarCristalesRotosFotoCommand = new Command(async () => await OnTomarFoto("Cristales_rotos_foto"));

			TomarGolpesFotoCommand = new Command(async () => await OnTomarFoto("Golpes_foto"));
			TomarTapetesFotoCommand = new Command(async () => await OnTomarFoto("Tapetes_foto"));
            TomarExtintorFotoCommand = new Command(async () => await OnTomarFoto("Extintor_foto"));
            TomarTaponesGasolinaFotoCommand = new Command(async () => await OnTomarFoto("Tapones_gasolina_foto"));
			TomarCalaverasRotasFotoCommand = new Command(async () => await OnTomarFoto("Calaveras_rotas_foto"));
			TomarMoldurasCompletasFotoCommand = new Command(async () => await OnTomarFoto("Molduras_completas_foto"));


            TomarPanelInstrumentosFotoCommand = new Command(async () => await OnTomarFoto("Panel_instrumentos_foto"));
            TomarLadoIzquierdoFotoCommand = new Command(async () => await OnTomarFoto("Lado_izquierdo_foto"));
            TomarLadoIzquierdoInfFotoCommand = new Command(async () => await OnTomarFoto("Lado_izquierdo_inf_foto"));
            TomarLadoDerechoFotoCommand = new Command(async () => await OnTomarFoto("Lado_derecho_foto"));
            TomarLadoDerechoInfFotoCommand = new Command(async () => await OnTomarFoto("Lado_derecho_inf_foto"));
            TomarTableroFotoCommand = new Command(async () => await OnTomarFoto("Tablero_foto"));
            TomarGuanteraFotoCommand = new Command(async () => await OnTomarFoto("Guantera_foto"));
            TomarConsolaFotoCommand = new Command(async () => await OnTomarFoto("Consola_foto"));
            TomarLadoFrontalFotoCommand = new Command(async () => await OnTomarFoto("LadoFrontal_foto"));
            TomarLadoTraseroFotoCommand = new Command(async () => await OnTomarFoto("LadoTrasero_foto"));
            TomarCajuelaFotoCommand = new Command(async () => await OnTomarFoto("Cajuela_foto"));
            TomarNumeroEconomicoFotoCommand = new Command(async () => await OnTomarFoto("NumeroEconomico_foto"));


            TomarAcumuladorVideoCommand = new Command(async () => await OnTomarFoto("Acumulador_video"));
            TomarMotorVehiculoVideoCommand = new Command(async () => await OnTomarFoto("MotorVehiculo_video"));
            TomarEspejoRetrovisorVideoCommand = new Command(async () => await OnTomarVideo("Espejo_retrovisor_video"));
			TomarEspejoIzquierdoVideoCommand = new Command(async () => await OnTomarVideo("Espejo_izquierdo_video"));
			TomarEspejoDerechoVideoCommand = new Command(async () => await OnTomarVideo("Espejo_derecho_video"));
			TomarAntenaVideoCommand = new Command(async () => await OnTomarVideo("Antena_video"));
			TomarTaponesRuedasVideoCommand = new Command(async () => await OnTomarVideo("Tapones_ruedas_video"));
			TomarRadioVideoCommand = new Command(async () => await OnTomarVideo("Radio_video"));
			TomarEncendedorVideoCommand = new Command(async () => await OnTomarVideo("Encendedor_video"));

			TomarGatoVideoCommand = new Command(async () => await OnTomarVideo("Gato_video"));
			TomarHerramientaVideoCommand = new Command(async () => await OnTomarVideo("Herramienta_video"));
			TomarLlantaRefaccionVideoCommand = new Command(async () => await OnTomarVideo("Llanta_refaccion_video"));
			TomarLimpiadoresVideoCommand = new Command(async () => await OnTomarVideo("Limpiadores_video"));
			TomarPinturaRayadaVideoCommand = new Command(async () => await OnTomarVideo("Pintura_rayada_video"));
			TomarCristalesRotosVideoCommand = new Command(async () => await OnTomarVideo("Cristales_rotos_video"));

			TomarGolpesVideoCommand = new Command(async () => await OnTomarVideo("Golpes_video"));
			TomarTapetesVideoCommand = new Command(async () => await OnTomarVideo("Tapetes_video"));
			TomarExtintorVideoCommand = new Command(async () => await OnTomarVideo("Extintor_video"));
			TomarTaponesGasolinaVideoCommand = new Command(async () => await OnTomarVideo("Tapones_gasolina_video"));
			TomarCalaverasRotasVideoCommand = new Command(async () => await OnTomarVideo("Calaveras_rotas_video"));
			TomarMoldurasCompletasVideoCommand = new Command(async () => await OnTomarVideo("Molduras_completas_video"));


            TomarPanelInstrumentosVideoCommand = new Command(async () => await OnTomarFoto("Panel_instrumentos_video"));
            TomarLadoIzquierdoVideoCommand = new Command(async () => await OnTomarFoto("Lado_izquierdo_video"));
            TomarLadoIzquierdoInfVideoCommand = new Command(async () => await OnTomarFoto("Lado_izquierdo_inf_video"));
            TomarLadoDerechoVideoCommand = new Command(async () => await OnTomarFoto("Lado_derecho_video"));
            TomarLadoDerechoInfVideoCommand = new Command(async () => await OnTomarFoto("Lado_derecho_inf_video"));
            TomarTableroVideoCommand = new Command(async () => await OnTomarFoto("Tablero_video"));
            TomarGuanteraVideoCommand = new Command(async () => await OnTomarFoto("Guantera_video"));
            TomarConsolaVideoCommand = new Command(async () => await OnTomarFoto("Consola_video"));
            TomarLadoFrontalVideoCommand = new Command(async () => await OnTomarFoto("LadoFrontal_video"));
            TomarLadoTraseroVideoCommand = new Command(async () => await OnTomarFoto("LadoTrasero_video"));
            TomarCajuelaVideoCommand = new Command(async () => await OnTomarFoto("Cajuela_video"));
            TomarNumeroEconomicoVideoCommand = new Command(async () => await OnTomarFoto("NumeroEconomico_video"));


            LoadClientesAsync();
            LoadFlotillasAsync();
        }



        public async Task LoadVehiculoForEditing(int idVehiculo)
        {
            Vehiculo = await _apiService.GetAsync<Models.Vehiculos>($"api/vehiculos/{idVehiculo}");
            IsEditing = true;
            SelectedCliente = _clientes.FirstOrDefault(c => c.IdCliente == Vehiculo.Id_Cliente);
            SelectedFlotilla = _flotillas.FirstOrDefault(f => f.IdFlotilla == Vehiculo.IdFlotilla);
        }
        private async void OnExpandImage(string imageSource)
		{
			if (string.IsNullOrEmpty(imageSource))
			{
				// Mostrar un mensaje de error o manejar el caso donde la imagen no está disponible
				await Application.Current.MainPage.DisplayAlert("Error", "La imagen no es accesible", "OK");
				return;
			}

			// Aquí puedes mostrar la imagen en una nueva página o ventana modal
			await Application.Current.MainPage.Navigation.PushAsync(new ImageDetailPage(imageSource));
		}

        private async Task LoadClientesAsync()
        {
            var clientes = await _apiService.GetAsync<List<Models.Clientes>>("api/clientes");
            Clientes = new ObservableCollection<Models.Clientes>(clientes);
        }

        private async Task LoadFlotillasAsync()
        {
            var flotillas = await _apiService.GetAsync<List<Models.Flotillas>>("api/obtenerflotillas");
            Flotillas = new ObservableCollection<Models.Flotillas>(flotillas);
        }

        public async Task LoadVehiculoAsync(int idVehiculo)
        {
            if (idVehiculo > 0)
            {
                IsEditing = true; // Establecer el modo de edición
                var vehiculo = await _apiService.GetAsync<Models.Vehiculos>($"api/vehiculos/{idVehiculo}");
                Vehiculo = vehiculo;

                // Cargar los datos relacionados al vehículo (cliente y flotilla)
                await LoadClienteFlotilla(vehiculo.Id_Cliente, vehiculo.IdFlotilla);
            }
            else
            {
                IsEditing = false; // Establecer el modo de creación
                Vehiculo = new Models.Vehiculos(); // Inicializar un nuevo vehículo para crear
            }
        }

        private async Task LoadClienteFlotilla(int idCliente, int idFlotilla)
        {
            // Cargar el Cliente usando su ID
            
            var cliente = await _apiService.GetAsync<Models.Clientes>($"api/cliente?idCliente={idCliente}");
            SelectedCliente = cliente;

            // Cargar la Flotilla usando su ID
            var flotilla = await _apiService.GetAsync<Models.Flotillas>($"api/obtenerflotillaporid?IdFlotilla={idFlotilla}");
            SelectedFlotilla = flotilla;
        }
        private async Task OnGuardarVehiculo()
		{
			try
			{
				// Validación de campos obligatorios
				if (string.IsNullOrWhiteSpace(Vehiculo.Marca) ||
					string.IsNullOrWhiteSpace(Vehiculo.Modelo) ||
					string.IsNullOrWhiteSpace(Vehiculo.Color) ||
					string.IsNullOrWhiteSpace(Vehiculo.No_serie) ||
					string.IsNullOrWhiteSpace(Vehiculo.Placa) ||
					string.IsNullOrWhiteSpace(Vehiculo.Tipo) ||
					string.IsNullOrWhiteSpace(Vehiculo.Motor) ||
                    
                    string.IsNullOrWhiteSpace(Vehiculo.Kms))
				{
					await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son requeridos.", "OK");
					return;
				}

				// Inicializar listas de fotos si son nulas
				EnsurePhotoListsInitialized();

                // Guardar el vehículo
                var idUsuario = await SecureStorage.GetAsync("idUsuario");

                if (!string.IsNullOrEmpty(idUsuario))
                {
                    if (int.TryParse(idUsuario, out int idEmpleado))
                    {
                        Vehiculo.Id_Empleado = idEmpleado;
                        
                       
                       
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "El IdUsuario no es un número válido.", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El IdUsuario no está disponible.", "OK");
                }

                await _apiService.AddVehiculoAsync(Vehiculo);
				await Application.Current.MainPage.Navigation.PopAsync();
				await Application.Current.MainPage.DisplayAlert("Éxito", "Vehículo agregado correctamente.", "OK");
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al guardar el vehículo: {ex.Message}", "OK");
			}
		}


        private async Task OnModificarVehiculo()
        {
            try
            {
                // Validación de campos obligatorios
                if (string.IsNullOrWhiteSpace(Vehiculo.Marca) ||
                    string.IsNullOrWhiteSpace(Vehiculo.Modelo) ||
                    string.IsNullOrWhiteSpace(Vehiculo.Color) ||
                    string.IsNullOrWhiteSpace(Vehiculo.No_serie) ||
                    string.IsNullOrWhiteSpace(Vehiculo.Placa) ||
                    string.IsNullOrWhiteSpace(Vehiculo.Tipo) ||
                    string.IsNullOrWhiteSpace(Vehiculo.Motor) ||
                    string.IsNullOrWhiteSpace(Vehiculo.Kms))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son requeridos.", "OK");
                    return;
                }

                // Inicializar listas de fotos si son nulas
                EnsurePhotoListsInitialized();

                // Guardar el vehículo
                var idUsuario = await SecureStorage.GetAsync("idUsuario");

                if (!string.IsNullOrEmpty(idUsuario))
                {
                    if (int.TryParse(idUsuario, out int idEmpleado))
                    {
                        Vehiculo.Id_Empleado = idEmpleado;



                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "El IdUsuario no es un número válido.", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El IdUsuario no está disponible.", "OK");
                }

                bool isUpdated = await _apiService.UpdateVehiculoAsync(Vehiculo);

				if (isUpdated)
				{
					await Application.Current.MainPage.Navigation.PopAsync();
					await Application.Current.MainPage.DisplayAlert("Éxito", "Vehículo modificado correctamente.", "OK");
				}
				else
				{
					await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error al modificar el vehículo.", "OK");
				}
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al guardar el vehículo: {ex.Message}", "OK");
            }
        }


        //private async Task OnModificarVehiculo()
        //{
        //    try
        //    {
        //        // Validación de campos obligatorios
        //        if (string.IsNullOrWhiteSpace(Vehiculo.Marca) ||
        //            string.IsNullOrWhiteSpace(Vehiculo.Modelo) ||
        //            string.IsNullOrWhiteSpace(Vehiculo.Color) ||
        //            string.IsNullOrWhiteSpace(Vehiculo.No_serie) ||
        //            string.IsNullOrWhiteSpace(Vehiculo.Placa) ||
        //            string.IsNullOrWhiteSpace(Vehiculo.Tipo) ||
        //            string.IsNullOrWhiteSpace(Vehiculo.Motor) ||
        //            string.IsNullOrWhiteSpace(Vehiculo.Kms))
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son requeridos.", "OK");
        //            return;
        //        }

        //        // Inicializar listas de fotos si son nulas
        //        EnsurePhotoListsInitialized();

        //        // Obtener el ID de usuario
        //        var idUsuario = await SecureStorage.GetAsync("idUsuario");

        //        if (!string.IsNullOrEmpty(idUsuario))
        //        {
        //            if (int.TryParse(idUsuario, out int idEmpleado))
        //            {
        //                Vehiculo.Id_Empleado = idEmpleado;
        //            }
        //            else
        //            {
        //                await Application.Current.MainPage.DisplayAlert("Error", "El IdUsuario no es un número válido.", "OK");
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Error", "El IdUsuario no está disponible.", "OK");
        //            return;
        //        }

        //        // Realizar la actualización del vehículo en el servidor
                
        //        bool isUpdated  = await _apiService.UpdateVehiculoAsync(Vehiculo);

        //        if (isUpdated)
        //        {
        //            await Application.Current.MainPage.Navigation.PopAsync();
        //            await Application.Current.MainPage.DisplayAlert("Éxito", "Vehículo modificado correctamente.", "OK");
        //        }
        //        else
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error al modificar el vehículo.", "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al modificar el vehículo: {ex.Message}", "OK");
        //    }
        //}







        private const int MaxBase64Length = 2000; // Ajusta este valor según sea necesario
	


		private void EnsurePhotoListsInitialized()
		{
	
			var photoFields = new List<List<string>>()
				{
					Vehiculo.Espejo_retrovisor_foto,
					Vehiculo.Espejo_izquierdo_foto,
					Vehiculo.Espejo_derecho_foto,
					Vehiculo.Antena_foto,
					Vehiculo.Tapones_ruedas_foto,
					Vehiculo.Radio_foto,
					Vehiculo.Encendedor_foto,
					Vehiculo.Gato_foto,
					Vehiculo.Herramienta_foto,
					Vehiculo.Llanta_refaccion_foto,
					Vehiculo.Limpiadores_foto,
					Vehiculo.Pintura_rayada_foto,
					Vehiculo.Cristales_rotos_foto,
					Vehiculo.Golpes_foto,
					Vehiculo.Tapetes_foto,
					Vehiculo.Extintor_foto,
					Vehiculo.Tapones_gasolina_foto,
					Vehiculo.Calaveras_rotas_foto,
                    Vehiculo.Molduras_completas_foto,
                    Vehiculo.Panel_instrumentos_foto,

                    Vehiculo.Lado_izquierdo_foto,

                    Vehiculo.Lado_izquierdo_inf_foto,

                    Vehiculo.Lado_derecho_foto,

                    Vehiculo.Lado_derecho_inf_foto,

                    Vehiculo.Tablero_foto,

                    Vehiculo.Guantera_foto,
                    Vehiculo.Consola_foto,
                    Vehiculo.LadoFrontal_foto,
                    Vehiculo.LadoTrasero_foto,
                    Vehiculo.Cajuela_foto,
                    Vehiculo.NumeroEconomico_foto
                };
		}

		private async Task OnTomarVideo(string propiedad)
		{
			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
			{
				await Application.Current.MainPage.DisplayAlert("No Camera", ":( No video recording available.", "OK");
				return;
			}

			var file = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
			{
				Directory = "Vehículos",
				Name = $"{propiedad}.mp4",
				DesiredLength = TimeSpan.FromSeconds(60), 
				Quality = VideoQuality.Medium
			});

			if (file != null)
			{
				// Convertir el archivo de vídeo a Base64
				var base64Video = ConvertVideoToBase64(file.Path);

				// Cortar el Base64 a un tamaño específico para reducir el consumo de recursos
				int maxLength = 5000; // Tamaño máximo en caracteres
				if (base64Video.Length > maxLength)
				{
					base64Video = base64Video.Substring(0, maxLength);
				}

				// Almacenar el vídeo en la propiedad correspondiente del vehículo
				switch (propiedad)
				{

                    case "Acumulador_video":
                        Vehiculo.Acumulador_video.Add(base64Video);
                        break;
                    case "MotorVehiculo_video":
						Vehiculo.MotorVehiculo_video.Add(base64Video);
						break;
					case "Espejo_retrovisor_video":
						Vehiculo.Espejo_retrovisor_video.Add(base64Video);
						break;
					case "Espejo_izquierdo_video":
						Vehiculo.Espejo_izquierdo_video.Add(base64Video);
						break;

					case "Espejo_derecho_video":
						Vehiculo.Espejo_derecho_video.Add(base64Video);
						break;
					case "Antena_video":
						Vehiculo.Antena_video.Add(base64Video);
						break;
                    case "Tapones_ruedas_video":
                        Vehiculo.Tapones_ruedas_video.Add(base64Video);
                        break;
                    case "Radio_video":
                        Vehiculo.Radio_video.Add(base64Video);
                        break;
                    case "Encendedor_video":
						Vehiculo.Encendedor_video.Add(base64Video);
						break;
					case "Gato_video":
						Vehiculo.Gato_video.Add(base64Video);
						break;
					case "Herramienta_video":
						Vehiculo.Herramienta_video.Add(base64Video);
						break;
					case "Llanta_refaccion_video":
						Vehiculo.Llanta_refaccion_video.Add(base64Video);
						break;
					case "Limpiadores_video":
						Vehiculo.Limpiadores_video.Add(base64Video);
						break;
					case "Pintura_rayada_video":
						Vehiculo.Pintura_rayada_video.Add(base64Video);
						break;
					case "Cristales_rotos_video":
						Vehiculo.Cristales_rotos_video.Add(base64Video);
						break;
					case "Golpes_video":
						Vehiculo.Golpes_video.Add(base64Video);
						break;
					case "Tapetes_video":
						Vehiculo.Tapetes_video.Add(base64Video);
						break;
					case "Extintor_video":
						Vehiculo.Extintor_video.Add(base64Video);
						break;
					case "Tapones_gasolina_video":
						Vehiculo.Tapones_gasolina_video.Add(base64Video);
						break;
					case "Calaveras_rotas_video":
						Vehiculo.Calaveras_rotas_video.Add(base64Video);
						break;
					case "Molduras_completas_video":
						Vehiculo.Molduras_completas_video.Add(base64Video);
						break;
                    case "Panel_instrumentos_video":
                        Vehiculo.Panel_instrumentos_video.Add(base64Video);
                        break;
                    case "Lado_izquierdo_video":
                        Vehiculo.Lado_izquierdo_video.Add(base64Video);
                        break;
                    case "Lado_izquierdo_inf_video":
                        Vehiculo.Lado_izquierdo_inf_video.Add(base64Video);
                        break;
                    case "Lado_derecho_video":
                        Vehiculo.Lado_derecho_video.Add(base64Video);
                        break;
                    case "Lado_derecho_inf_video":
                        Vehiculo.Lado_derecho_inf_video.Add(base64Video);
                        break;
                    case "Tablero_video":
                        Vehiculo.Tablero_video.Add(base64Video);
                        break;
                    case "Guantera_video":
                        Vehiculo.Guantera_video.Add(base64Video);
                        break;
                    case "Consola_video":
                        Vehiculo.Consola_video.Add(base64Video);
                        break;
                    case "LadoFrontal_video":
                        Vehiculo.LadoFrontal_video.Add(base64Video);
                        break;
                    case "LadoTrasero_video":
                        Vehiculo.LadoTrasero_video.Add(base64Video);
                        break;
                    case "Cajuela_video":
                        Vehiculo.Cajuela_video.Add(base64Video);
                        break;

                    default:
						break;

				}
			}
		}



		private async Task OnTomarFoto(string propiedad)
		{
			await CrossMedia.Current.Initialize();



			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
				return;
			}

			var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
			{
				PhotoSize = PhotoSize.Small,
				Directory = "Vehículos",
				Name = $"{propiedad}.jpg"
			});

			if (file != null)
			{
				// Redimensionar y comprimir la imagen
				var base64Compressed = CompressImage(file, 500, 200); // Ajusta el tamaño y calidad
				switch (propiedad)
				{
                    case "MotorVehiculo_foto":
                        Vehiculo.MotorVehiculo_foto.Add(base64Compressed);
                        break;
                    case "Acumulador_foto":
                        Vehiculo.Acumulador_foto.Add(base64Compressed);
                        break;
                    case "Espejo_retrovisor_foto":
						Vehiculo.Espejo_retrovisor_foto.Add(base64Compressed);
						break;
					case "Espejo_izquierdo_foto":
					Vehiculo.Espejo_izquierdo_foto.Add(base64Compressed);
						break;

					case "Espejo_derecho_foto":
					Vehiculo.Espejo_derecho_foto.Add(base64Compressed);
						break;
					case "Antena_foto":
					Vehiculo.Antena_foto.Add(base64Compressed);
						break;
                    case "Tapones_ruedas_foto":
                        Vehiculo.Tapones_ruedas_foto.Add(base64Compressed);
                        break;
                    case "Radio_foto":
                        Vehiculo.Radio_foto.Add(base64Compressed);
                        break;
                    case "Encendedor_foto":
					Vehiculo.Encendedor_foto.Add(base64Compressed);
						break;
					case "Gato_foto":
					Vehiculo.Gato_foto.Add(base64Compressed);
						break;
					case "Herramienta_foto":
					Vehiculo.Herramienta_foto.Add(base64Compressed);
						break;
					case "Llanta_refaccion_foto":
					Vehiculo.Llanta_refaccion_foto.Add(base64Compressed);
						break;
					case "Limpiadores_foto":
					Vehiculo.Limpiadores_foto.Add(base64Compressed);	
						break;
					case "Pintura_rayada_foto":
					Vehiculo.Pintura_rayada_foto.Add(base64Compressed);
						break;
					case "Cristales_rotos_foto":
					Vehiculo.Cristales_rotos_foto.Add(base64Compressed);
						break;
					case "Golpes_foto":
					Vehiculo.Golpes_foto.Add(base64Compressed);
						break;
					case "Tapetes_foto":
					Vehiculo.Tapetes_foto.Add(base64Compressed);
					break;
					case "Extintor_foto":
					Vehiculo.Extintor_foto.Add(base64Compressed);
					break;
					case "Tapones_gasolina_foto":
					Vehiculo.Tapones_gasolina_foto.Add(base64Compressed);
					break;
					case "Calaveras_rotas_foto":
					Vehiculo.Calaveras_rotas_foto.Add(base64Compressed);
					break;
					case "Molduras_completas_foto":
					Vehiculo.Molduras_completas_foto.Add(base64Compressed);
					break;

                    case "Panel_instrumentos_foto":
                        Vehiculo.Panel_instrumentos_foto.Add(base64Compressed);
                        break;
                    case "Lado_izquierdo_foto":
                        Vehiculo.Lado_izquierdo_foto.Add(base64Compressed);
                        break;
                    case "Lado_izquierdo_inf_foto":
                        Vehiculo.Lado_izquierdo_inf_foto.Add(base64Compressed);
                        break;
                    case "Lado_derecho_foto":
                        Vehiculo.Lado_derecho_foto.Add(base64Compressed);
                        break;
                    case "Lado_derecho_inf_foto":
                        Vehiculo.Lado_derecho_inf_foto.Add(base64Compressed);
                        break;
                    case "Tablero_foto":
                        Vehiculo.Tablero_foto.Add(base64Compressed);
                        break;
                    case "Guantera_foto":
                        Vehiculo.Guantera_foto.Add(base64Compressed);
                        break;
                    case "Consola_foto":
                        Vehiculo.Consola_foto.Add(base64Compressed);
                        break;
                    case "LadoFrontal_foto":
                        Vehiculo.LadoFrontal_foto.Add(base64Compressed);
                        break;
                    case "LadoTrasero_foto":
                        Vehiculo.LadoTrasero_foto.Add(base64Compressed);
                        break;
                    case "Cajuela_foto":
                        Vehiculo.Cajuela_foto.Add(base64Compressed);

                        break;



                }
			}

			OnPropertyChanged(nameof(Vehiculo));


		}


		private string ConvertToBase64(MediaFile photo)
		{
			using (var memoryStream = new MemoryStream())
			{
				photo.GetStream().CopyTo(memoryStream);
				var imageBytes = memoryStream.ToArray();
				return Convert.ToBase64String(imageBytes);
			}
		}

		private string CompressImage(MediaFile photo, int maxWidth, int maxHeight)
		{
			using (var stream = photo.GetStream())
			{
				using (var originalBitmap = SKBitmap.Decode(stream))
				{
					// Calcula las dimensiones nuevas para la imagen
					var ratio = Math.Min((float)maxWidth / originalBitmap.Width, (float)maxHeight / originalBitmap.Height);
					var width = (int)(originalBitmap.Width * ratio);
					var height = (int)(originalBitmap.Height * ratio);

					var imageInfo = new SKImageInfo(width, height);
					using (var resizedBitmap = originalBitmap.Resize(imageInfo, SKFilterQuality.High))
					{
						using (var image = SKImage.FromBitmap(resizedBitmap))
						{
							using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 75)) // Ajusta la calidad según tus necesidades
							{
								return Convert.ToBase64String(data.ToArray());
							}
						}
					}
				}
			}
		}


		private string ConvertVideoToBase64(string videoPath)
		{
			try
			{
				var fileBytes = File.ReadAllBytes(videoPath);
				return Convert.ToBase64String(fileBytes);
			}
			catch (Exception ex)
			{
				Application.Current.MainPage.DisplayAlert("Error", $"Error al convertir el vídeo a Base64: {ex.Message}", "OK");
				return string.Empty;
			}
		}


       



    }
}
