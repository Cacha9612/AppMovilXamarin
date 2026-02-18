using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AppMovilTrotaMundos.Models;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Xamarin.Forms;
using AppMovilTrotaMundos.Services;
using System.Diagnostics;
using System.Windows.Input;
using AppMovilTrotaMundos.Views;
using System.Collections.ObjectModel;
using SkiaSharp;
using Xamarin.Essentials;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AppMovilTrotaMundos.ViewModels
{
	public class FormularioChecklistViewModel : BaseViewModel
	{

		private readonly ApiService _apiService;
		private Models.CheckList _checklist;

		private Stopwatch _stopwatch;  // Cronómetro
		private string _tiempoTranscurrido;

		public Command GuardarCommand { get; }
        public Command ModificarCheckCommand { get; }
        public Command CameraCommand { get; }
		public Command LoadDataCommand { get; }


		public Command IniciarCronometroCommand { get; }  // Comando para iniciar el cronómetro
		public Command DetenerCronometroCommand { get; }  // Comando para detener el cronómetro

		private bool _isEditing;

        public Models.CheckList CheckList
        {
            get => _checklist;
            set
            {
                _checklist = value;
                //CargarItemsDesdeCheckList();
                OnPropertyChanged();
               
            }

           
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

		public string TiempoTranscurrido
		{
			get => _tiempoTranscurrido;
			set
			{
				_tiempoTranscurrido = value;
				OnPropertyChanged();
			}
		}


		private ObservableCollection<Models.Vehiculos> _vehiculos;
		public ObservableCollection<Models.Vehiculos> Vehiculos
		{
			get => _vehiculos;
			set
			{
				_vehiculos = value;
				OnPropertyChanged();
                ActualizarNumeroSerie(); // Llama el método al actualizar los vehículos
            }
		}

        private string _numeroSerie;
        public string NumeroSerie
        {
            get => _numeroSerie;
            set
            {
                _numeroSerie = value;
                OnPropertyChanged();
            }
        }

        public int IdVehiculo
        {
            get => CheckList.IdVehiculo;
            set
            {
                CheckList.IdVehiculo = value;
                OnPropertyChanged(nameof(IdVehiculo));
                ActualizarNumeroSerie(); // Actualiza el número de serie cada vez que cambie el IdVehiculo
            }
        }



        private Models.Vehiculos _selectedvehiculo;
		public Models.Vehiculos SelectedVehiculo
		{
			get => _selectedvehiculo;
			set
			{
				_selectedvehiculo = value;
				OnPropertyChanged();
				// Opcional: Actualizar alguna propiedad adicional o realizar alguna acción
				CheckList.IdVehiculo = _selectedvehiculo?.ID ?? 0;
			}
		}

	

		private int _id_ordendeservicio;
		public int Id_ordendeservicio
		{
			get => _id_ordendeservicio;
			set
			{
				if (_id_ordendeservicio != value)
				{
					_id_ordendeservicio = value;
					OnPropertyChanged(nameof(Id_ordendeservicio));
				}
			}
		}

        private ObservableCollection<CheckListItem> _items;
        public ObservableCollection<CheckListItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        //public void CargarItemsDesdeCheckList()
        //{
        //    if (CheckList == null) return;

        //    var items = new ObservableCollection<CheckListItem>();

        //    var props = typeof(Models.CheckList).GetProperties()
        //        .Where(p => p.PropertyType == typeof(int)
        //            && !p.Name.EndsWith("_observacion")
        //            && !p.Name.EndsWith("_foto")
        //            && p.Name != "IdEmpleado"
        //            && p.Name != "IdVehiculo"
        //            && p.Name != "Id_ordendeservicio"
        //            && p.Name != "Id"
        //            && p.Name != "Activo");

        //    bool tieneRojo = false;
        //    bool tieneAmarillo = false;

        //    foreach (var prop in props)
        //    {
        //        var valor = (int)prop.GetValue(CheckList);

        //        items.Add(new CheckListItem
        //        {
        //            NombreCampo = prop.Name,
        //            Estado = valor
        //        });

        //        if (valor == 0) tieneRojo = true;
        //        else if (valor == 1) tieneAmarillo = true;
        //    }

        //    Items = items;

        //    // Actualizar semáforo general según estados
        //    if (tieneRojo)
        //        EstadoSemaforo = 0;
        //    else if (tieneAmarillo)
        //        EstadoSemaforo = 1;
        //    else
        //        EstadoSemaforo = 2;
        //}



        //public string SemaforoIcon
        //{
        //    get
        //    {
        //        switch (EstadoSemaforo)
        //        {
        //            case 0:
        //                return "semaforo_rojo.png";
        //            case 1:
        //                return "semaforo_amarillo.png";
        //            case 2:
        //                return "semaforo_verde.png";
        //            default:
        //                return "semaforo_gris.png";
        //        }
        //    }
        //}


        //private int _estadoSemaforo;
        //public int EstadoSemaforo
        //{
        //    get => _estadoSemaforo;
        //    set
        //    {
        //        if (_estadoSemaforo != value)
        //        {
        //            _estadoSemaforo = value;
        //            OnPropertyChanged();
        //            OnPropertyChanged(nameof(SemaforoIcon));  // Notificar cambio
        //        }
        //    }
        //}

        // Propiedad para devolver la ruta o fuente de imagen del ícono semáforo

   

        public ICommand TomarLecturaCodigosFotoCommand { get; }
		public ICommand TomarServoFrenoFotoCommand { get; }
		public ICommand TomarPedalFrenoFotoCommand { get; }
		public ICommand TomarPedalEstacionamientoFotoCommand { get; }
		public ICommand TomarCinturonSeguridadFotoCommand { get; }
		public ICommand TomarCuadroInstrumentosFotoCommand { get; }
		public ICommand TomarAireAcondicionadoFotoCommand { get; }
		public ICommand TomarBocinaClaxonFotoCommand { get; }
		public ICommand TomarIluminacionInteriorFotoCommand { get; }
		public ICommand TomarIluminacionExternaFotoCommand { get; }
		public ICommand TomarLimpiaparabrisasFotoCommand { get; }
		public ICommand TomarLimpiaMedallonFotoCommand { get; }
		public ICommand TomarNeumaticosFriccionFotoCommand { get; }
		public ICommand TomarEstadoFugasAceiteFotoCommand { get; }
		public ICommand TomarEstadoNivelCalidaTransmisionFotoCommand { get; }
		public ICommand TomarNivelCalidadDiferencialFotoCommand { get; }
		public ICommand TomarCubrepolvosFlechasFotoCommand { get; }
		public ICommand TomarComponentesDireccionFotoCommand { get; }
		public ICommand TomarComponentesSuspensionFotoCommand { get; }
		public ICommand TomarSistemaEscapeCompletoFotoCommand { get; }
		public ICommand TomarSistemaAlimentacionCombustibleFotoCommand { get; }
		public ICommand TomarFiltroCombustibleFotoCommand { get; }
		public ICommand TomarControlFugasHidraulicaFotoCommand { get; }
		public ICommand TomarRodamientoMazasRuedasFotoCommand { get; }
		public ICommand TomarHolguraPartesSuspensionFotoCommand { get; }
		public ICommand TomarControlNeumaticosDesgasteFotoCommand { get; }
		public ICommand TomarProfundidadFotoCommand { get; }
		public ICommand TomarPresionFotoCommand { get; }
		public ICommand TomarNivelCalidadAceiteMotorFotoCommand { get; }
		public ICommand TomarFiltroAireFotoCommand { get; }
		public ICommand TomarFiltroPolenFotoCommand { get; }
		public ICommand TomarFiltroPCVFotoCommand { get; }
		public ICommand TomarValvulaPCVFotoCommand { get; }
		public ICommand TomarBujiasEncendidoFotoCommand { get; }
		public ICommand TomarCablesBujiasBobinasFotoCommand { get; }
		public ICommand TomarNivelAnticongelanteFotoCommand { get; }
		public ICommand TomarTaponRadiadorFotoCommand { get; }
		public ICommand TomarManguerasSistemaFotoCommand { get; }
		public ICommand TomarDesempeñoVentiladorFotoCommand { get; }
		public ICommand TomarCalidadLiquidoParabrisasFotoCommand { get; }
		public ICommand TomarCaldiadAceiteHidraulicaFotoCommand { get; }
		public ICommand TomarCalidadAceiteTransmisionFotoCommand { get; }
		public ICommand TomarLiquidoBateriaConcidionesFotoCommand { get; }
		public ICommand TomarBandasPolyVFotoCommand { get; }
		public ICommand TomarPoleasBandaFotoCommand { get; }
		public ICommand TomarBandaTiempoFotoCommand { get; }
		public ICommand TomarResetIntervaloServicioFotoCommand { get; }
		public ICommand TomarAjusteTornillosNeumaticosFotoCommand { get; }
		public ICommand TomarLimpiarLubricarPuertasFotoCommand { get; }
		public ICommand TomarCompletarPlanMantenimientoFotoCommand { get; }


		public ICommand TomarOtrosVehiculoPisoFotoCommand { get; }
		public ICommand TomarOtrosAlturaMediaFotoCommand { get; }
		public ICommand TomarOtrosAlturaTotalFotoCommand { get; }
		public ICommand TomarOtrosHabitaculoMotorFotoCommand { get; }

		public ICommand ExpandImageCommand { get; }






		public FormularioChecklistViewModel()
		{
			_apiService = new ApiService();
			CheckList = new Models.CheckList();
			_vehiculos = new ObservableCollection<Models.Vehiculos>();
			_stopwatch = new Stopwatch();  // Inicializa el cronómetro
			TiempoTranscurrido = "00:00"; // Tiempo inicial
			IniciarCronometroCommand = new Command(IniciarCronometro);
			DetenerCronometroCommand = new Command(DetenerCronometro);
			ModificarCheckCommand = new Command(async () => await OnModificarCheck());
			GuardarCommand = new Command(async () => await GuardarChecklistAsync());
			CargarUltimoIdAsync();


			ExpandImageCommand = new Command<string>(OnExpandImage);


			TomarLecturaCodigosFotoCommand = new Command(async () => await OnTomarFoto("lectura_codigos_foto"));
			TomarServoFrenoFotoCommand = new Command(async () => await OnTomarFoto("servofreno_foto"));
			TomarPedalFrenoFotoCommand = new Command(async () => await OnTomarFoto("pedal_freno_foto"));
			TomarPedalEstacionamientoFotoCommand = new Command(async () => await OnTomarFoto("pedal_estacionamiento_foto"));
			TomarCinturonSeguridadFotoCommand = new Command(async () => await OnTomarFoto("cinturon_seguridad_foto"));
			TomarCuadroInstrumentosFotoCommand = new Command(async () => await OnTomarFoto("cuadro_instrumentos_foto"));
			TomarAireAcondicionadoFotoCommand = new Command(async () => await OnTomarFoto("aire_acondicionado_foto"));
			TomarBocinaClaxonFotoCommand = new Command(async () => await OnTomarFoto("bocina_claxon_foto"));
			TomarIluminacionInteriorFotoCommand = new Command(async () => await OnTomarFoto("iluminacion_interior_foto"));
			TomarIluminacionExternaFotoCommand = new Command(async () => await OnTomarFoto("iluminacion_externa_foto"));
			TomarLimpiaparabrisasFotoCommand = new Command(async () => await OnTomarFoto("limpiaparabrisas_foto"));
			TomarLimpiaMedallonFotoCommand = new Command(async () => await OnTomarFoto("limpia_medallon_foto"));
			TomarNeumaticosFriccionFotoCommand = new Command(async () => await OnTomarFoto("neumaticos_friccion_foto"));
			TomarEstadoFugasAceiteFotoCommand = new Command(async () => await OnTomarFoto("estado_fugas_aceite_foto"));
			TomarEstadoNivelCalidaTransmisionFotoCommand = new Command(async () => await OnTomarFoto("estado_nivel_calidad_lubricante_transmision_foto"));
			TomarNivelCalidadDiferencialFotoCommand = new Command(async () => await OnTomarFoto("estado_nivel_calidad_lubricante_diferencial_foto"));
			TomarCubrepolvosFlechasFotoCommand = new Command(async () => await OnTomarFoto("cubrepolvos_flechas_foto"));
			TomarComponentesDireccionFotoCommand = new Command(async () => await OnTomarFoto("componentes_direccion_foto"));
			TomarComponentesSuspensionFotoCommand = new Command(async () => await OnTomarFoto("componentes_suspesion_foto"));
			TomarSistemaEscapeCompletoFotoCommand = new Command(async () => await OnTomarFoto("sistema_escape_completo_foto"));
			TomarSistemaAlimentacionCombustibleFotoCommand = new Command(async () => await OnTomarFoto("sistema_alimentacion_combustible_foto"));
			TomarFiltroCombustibleFotoCommand = new Command(async () => await OnTomarFoto("filtro_combustible_foto"));
			TomarControlFugasHidraulicaFotoCommand = new Command(async () => await OnTomarFoto("control_fugas_direccion_hidraulica_foto"));
			TomarRodamientoMazasRuedasFotoCommand = new Command(async () => await OnTomarFoto("rodamiento_mazas_rueda_foto"));
			TomarHolguraPartesSuspensionFotoCommand = new Command(async () => await OnTomarFoto("holgura_partes_suspension_rueda_foto"));
			TomarControlNeumaticosDesgasteFotoCommand = new Command(async () => await OnTomarFoto("control_neumaticos_desgaste_presion_foto"));
			TomarProfundidadFotoCommand = new Command(async () => await OnTomarFoto("profundidad_foto"));
			TomarPresionFotoCommand = new Command(async () => await OnTomarFoto("presion_foto"));
			TomarNivelCalidadAceiteMotorFotoCommand = new Command(async () => await OnTomarFoto("nivel_calidad_aceite_motor_foto"));
			TomarFiltroAireFotoCommand = new Command(async () => await OnTomarFoto("filtro_aire_foto"));
			TomarFiltroPolenFotoCommand = new Command(async () => await OnTomarFoto("filtro_polen_foto"));
			TomarFiltroPCVFotoCommand = new Command(async () => await OnTomarFoto("filtro_pcv_foto"));
			TomarValvulaPCVFotoCommand = new Command(async () => await OnTomarFoto("valvula_pcv_foto"));
			TomarBujiasEncendidoFotoCommand = new Command(async () => await OnTomarFoto("bujias_encendido_foto"));
			TomarCablesBujiasBobinasFotoCommand = new Command(async () => await OnTomarFoto("cables_bujias_bobinas_ignicion_foto"));
			TomarNivelAnticongelanteFotoCommand = new Command(async () => await OnTomarFoto("nivel_anticongenlante_foto"));
			TomarTaponRadiadorFotoCommand = new Command(async () => await OnTomarFoto("tapon_radiador_foto"));
			TomarManguerasSistemaFotoCommand = new Command(async () => await OnTomarFoto("mangueras_sistema_foto"));
			TomarDesempeñoVentiladorFotoCommand = new Command(async () => await OnTomarFoto("desempeño_ventilador_foto"));
			TomarCalidadLiquidoParabrisasFotoCommand = new Command(async () => await OnTomarFoto("calidad_liquido_limpiaparabrisas_foto"));
			TomarCaldiadAceiteHidraulicaFotoCommand = new Command(async () => await OnTomarFoto("calidad_aceite_direccion_hidraulica_foto"));
			TomarCalidadAceiteTransmisionFotoCommand = new Command(async () => await OnTomarFoto("calidad_aceite_transmision_bayoneta_foto"));
			TomarLiquidoBateriaConcidionesFotoCommand = new Command(async () => await OnTomarFoto("liquido_bateria_condiciones_foto"));
			TomarBandasPolyVFotoCommand = new Command(async () => await OnTomarFoto("bandas_poly_v_foto"));
			TomarPoleasBandaFotoCommand = new Command(async () => await OnTomarFoto("poleas_banda_foto"));
			TomarBandaTiempoFotoCommand = new Command(async () => await OnTomarFoto("banda_tiempo_foto"));
			TomarResetIntervaloServicioFotoCommand = new Command(async () => await OnTomarFoto("reset_intervalo_servicio_foto"));
			TomarAjusteTornillosNeumaticosFotoCommand = new Command(async () => await OnTomarFoto("ajuste_tornillos_neumaticos_torquimetro_foto"));
			TomarLimpiarLubricarPuertasFotoCommand = new Command(async () => await OnTomarFoto("limpiar_libricar_puertas_cerraduras_foto"));
			TomarCompletarPlanMantenimientoFotoCommand = new Command(async () => await OnTomarFoto("completar_plan_mantenimiento_foto"));
			TomarOtrosVehiculoPisoFotoCommand = new Command(async () => await OnTomarFoto("otros_vehiculo_en_piso_foto"));
			TomarOtrosAlturaMediaFotoCommand = new Command(async () => await OnTomarFoto("otros_altura_media_foto"));
			TomarOtrosAlturaTotalFotoCommand = new Command(async () => await OnTomarFoto("otros_altura_total_foto"));
			TomarOtrosHabitaculoMotorFotoCommand = new Command(async () => await OnTomarFoto("otros_habitaculo_motor_foto"));


            Task.Run(async () =>
            {
                await LoadVehiculosAsync();
                ActualizarNumeroSerie(); // Asegúrate de que Vehiculos ya esté cargado
            });
        }

		private void IniciarCronometro()
		{
			_stopwatch.Start();
			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{
				// Actualiza el tiempo transcurrido cada segundo
				TiempoTranscurrido = _stopwatch.Elapsed.ToString(@"mm\:ss");
				return true;  // Continuar actualizando cada segundo
			});
		}


		// Detener el cronómetro
		private void DetenerCronometro()
		{
			_stopwatch.Stop();
		}

		private async Task CargarUltimoIdAsync()
		{
			try
			{
				var ultimoId = await _apiService.ObtenerNuevoIdOrdenAsync();
				Debug.WriteLine($"Último ID obtenido: {ultimoId}");

				if (ultimoId != null)
				{
					CheckList.Id_ordendeservicio = ultimoId;
					OnPropertyChanged(nameof(CheckList)); // Notifica el cambio completo del objeto
				}
				else
				{
					CheckList.Id_ordendeservicio = 1;
					OnPropertyChanged(nameof(CheckList));
				}
			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo obtener el último ID: {ex.Message}", "OK");
			}
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



		private async Task LoadVehiculosAsync()
		{
			try
			{
				var vehiculo = await _apiService.GetAsync<List<Models.Vehiculos>>("api/vehiculos");
				Vehiculos = new ObservableCollection<Models.Vehiculos>(vehiculo);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error loading vehicles: {ex.Message}");
				// Manejar el error de manera apropiada
			}
		}


		private async Task GuardarChecklistAsync()
		{
			try
			{
				// Detener el cronómetro después de guardar el checklist
				_stopwatch.Stop();

				// Obtener el tiempo transcurrido en segundos
				int tiempoTranscurridoSegundos = (int)_stopwatch.Elapsed.TotalSeconds;



				CheckList.lectura_codigos_foto = string.IsNullOrWhiteSpace(CheckList.lectura_codigos_foto) ? string.Empty : CheckList.lectura_codigos_foto;
				CheckList.servofreno_foto = string.IsNullOrWhiteSpace(CheckList.servofreno_foto) ? string.Empty : CheckList.servofreno_foto;
				CheckList.pedal_freno_foto = string.IsNullOrWhiteSpace(CheckList.pedal_freno_foto) ? string.Empty : CheckList.pedal_freno_foto;
				CheckList.pedal_estacionamiento_foto = string.IsNullOrWhiteSpace(CheckList.pedal_estacionamiento_foto) ? string.Empty : CheckList.pedal_estacionamiento_foto;
				CheckList.cinturon_seguridad_foto = string.IsNullOrWhiteSpace(CheckList.cinturon_seguridad_foto) ? string.Empty : CheckList.cinturon_seguridad_foto;
				CheckList.cuadro_instrumentos_foto = string.IsNullOrWhiteSpace(CheckList.cuadro_instrumentos_foto) ? string.Empty : CheckList.cuadro_instrumentos_foto;
				CheckList.aire_acondicionado_foto = string.IsNullOrWhiteSpace(CheckList.aire_acondicionado_foto) ? string.Empty : CheckList.aire_acondicionado_foto;
				CheckList.bocina_claxon_foto = string.IsNullOrWhiteSpace(CheckList.bocina_claxon_foto) ? string.Empty : CheckList.bocina_claxon_foto;
				CheckList.iluminacion_interior_foto = string.IsNullOrWhiteSpace(CheckList.iluminacion_interior_foto) ? string.Empty : CheckList.iluminacion_interior_foto;
				CheckList.iluminacion_externa_foto = string.IsNullOrWhiteSpace(CheckList.iluminacion_externa_foto) ? string.Empty : CheckList.iluminacion_externa_foto;
				CheckList.limpiaparabrisas_foto = string.IsNullOrWhiteSpace(CheckList.limpiaparabrisas_foto) ? string.Empty : CheckList.limpiaparabrisas_foto;
				CheckList.limpia_medallon_foto = string.IsNullOrWhiteSpace(CheckList.limpia_medallon_foto) ? string.Empty : CheckList.limpia_medallon_foto;
				CheckList.neumaticos_friccion_foto = string.IsNullOrWhiteSpace(CheckList.neumaticos_friccion_foto) ? string.Empty : CheckList.neumaticos_friccion_foto;
				CheckList.estado_fugas_aceite_foto = string.IsNullOrWhiteSpace(CheckList.estado_fugas_aceite_foto) ? string.Empty : CheckList.estado_fugas_aceite_foto;
				CheckList.estado_nivel_calidad_lubricante_transmision_foto = string.IsNullOrWhiteSpace(CheckList.estado_nivel_calidad_lubricante_transmision_foto) ? string.Empty : CheckList.estado_nivel_calidad_lubricante_transmision_foto;
				CheckList.estado_nivel_calidad_lubricante_diferencial_foto = string.IsNullOrWhiteSpace(CheckList.estado_nivel_calidad_lubricante_diferencial_foto) ? string.Empty : CheckList.estado_nivel_calidad_lubricante_diferencial_foto;
				CheckList.cubrepolvos_flechas_foto = string.IsNullOrWhiteSpace(CheckList.cubrepolvos_flechas_foto) ? string.Empty : CheckList.cubrepolvos_flechas_foto;
				CheckList.componentes_direccion_foto = string.IsNullOrWhiteSpace(CheckList.componentes_direccion_foto) ? string.Empty : CheckList.componentes_direccion_foto;
				CheckList.componentes_suspesion_foto = string.IsNullOrWhiteSpace(CheckList.componentes_suspesion_foto) ? string.Empty : CheckList.componentes_suspesion_foto;
				CheckList.sistema_escape_completo_foto = string.IsNullOrWhiteSpace(CheckList.sistema_escape_completo_foto) ? string.Empty : CheckList.sistema_escape_completo_foto;
				CheckList.sistema_alimentacion_combustible_foto = string.IsNullOrWhiteSpace(CheckList.sistema_alimentacion_combustible_foto) ? string.Empty : CheckList.sistema_alimentacion_combustible_foto;
				CheckList.filtro_combustible_foto = string.IsNullOrWhiteSpace(CheckList.filtro_combustible_foto) ? string.Empty : CheckList.filtro_combustible_foto;
				CheckList.control_fugas_direccion_hidraulica_foto = string.IsNullOrWhiteSpace(CheckList.control_fugas_direccion_hidraulica_foto) ? string.Empty : CheckList.control_fugas_direccion_hidraulica_foto;
				CheckList.holgura_partes_suspension_rueda_foto = string.IsNullOrWhiteSpace(CheckList.holgura_partes_suspension_rueda_foto) ? string.Empty : CheckList.holgura_partes_suspension_rueda_foto;
				CheckList.control_neumaticos_desgaste_presion_foto = string.IsNullOrWhiteSpace(CheckList.control_neumaticos_desgaste_presion_foto) ? string.Empty : CheckList.control_neumaticos_desgaste_presion_foto;
				CheckList.profundidad_foto = string.IsNullOrWhiteSpace(CheckList.profundidad_foto) ? string.Empty : CheckList.profundidad_foto;
				CheckList.presion_foto = string.IsNullOrWhiteSpace(CheckList.presion_foto) ? string.Empty : CheckList.presion_foto;
				CheckList.nivel_calidad_aceite_motor_foto = string.IsNullOrWhiteSpace(CheckList.nivel_calidad_aceite_motor_foto) ? string.Empty : CheckList.nivel_calidad_aceite_motor_foto;
				CheckList.filtro_aire_foto = string.IsNullOrWhiteSpace(CheckList.filtro_aire_foto) ? string.Empty : CheckList.filtro_aire_foto;
				CheckList.filtro_polen_foto = string.IsNullOrWhiteSpace(CheckList.filtro_polen_foto) ? string.Empty : CheckList.filtro_polen_foto;
				CheckList.filtro_pcv_foto = string.IsNullOrWhiteSpace(CheckList.filtro_pcv_foto) ? string.Empty : CheckList.filtro_pcv_foto;
				CheckList.valvula_pcv_foto = string.IsNullOrWhiteSpace(CheckList.valvula_pcv_foto) ? string.Empty : CheckList.valvula_pcv_foto;
				CheckList.bujias_encendido_foto = string.IsNullOrWhiteSpace(CheckList.bujias_encendido_foto) ? string.Empty : CheckList.bujias_encendido_foto;
				CheckList.cables_bujias_bobinas_ignicion_foto = string.IsNullOrWhiteSpace(CheckList.cables_bujias_bobinas_ignicion_foto) ? string.Empty : CheckList.cables_bujias_bobinas_ignicion_foto;
				CheckList.nivel_anticongenlante_foto = string.IsNullOrWhiteSpace(CheckList.nivel_anticongenlante_foto) ? string.Empty : CheckList.nivel_anticongenlante_foto;
				CheckList.tapon_radiador_foto = string.IsNullOrWhiteSpace(CheckList.tapon_radiador_foto) ? string.Empty : CheckList.tapon_radiador_foto;
				CheckList.mangueras_sistema_foto = string.IsNullOrWhiteSpace(CheckList.mangueras_sistema_foto) ? string.Empty : CheckList.mangueras_sistema_foto;
				CheckList.desempeño_ventilador_foto = string.IsNullOrWhiteSpace(CheckList.desempeño_ventilador_foto) ? string.Empty : CheckList.desempeño_ventilador_foto;
				CheckList.calidad_liquido_limpiaparabrisas_foto = string.IsNullOrWhiteSpace(CheckList.calidad_liquido_limpiaparabrisas_foto) ? string.Empty : CheckList.calidad_liquido_limpiaparabrisas_foto;
				CheckList.calidad_aceite_direccion_hidraulica_foto = string.IsNullOrWhiteSpace(CheckList.calidad_aceite_direccion_hidraulica_foto) ? string.Empty : CheckList.calidad_aceite_direccion_hidraulica_foto;
				CheckList.calidad_aceite_transmision_bayoneta_foto = string.IsNullOrWhiteSpace(CheckList.calidad_aceite_transmision_bayoneta_foto) ? string.Empty : CheckList.calidad_aceite_transmision_bayoneta_foto;
				CheckList.liquido_bateria_condiciones_foto = string.IsNullOrWhiteSpace(CheckList.liquido_bateria_condiciones_foto) ? string.Empty : CheckList.liquido_bateria_condiciones_foto;
				CheckList.bandas_poly_v_foto = string.IsNullOrWhiteSpace(CheckList.bandas_poly_v_foto) ? string.Empty : CheckList.bandas_poly_v_foto;
				CheckList.poleas_banda_foto = string.IsNullOrWhiteSpace(CheckList.poleas_banda_foto) ? string.Empty : CheckList.poleas_banda_foto;
				CheckList.banda_tiempo_foto = string.IsNullOrWhiteSpace(CheckList.banda_tiempo_foto) ? string.Empty : CheckList.banda_tiempo_foto;
				CheckList.reset_intervalo_servicio_foto = string.IsNullOrWhiteSpace(CheckList.reset_intervalo_servicio_foto) ? string.Empty : CheckList.reset_intervalo_servicio_foto;
				CheckList.ajuste_tornillos_neumaticos_torquimetro_foto = string.IsNullOrWhiteSpace(CheckList.ajuste_tornillos_neumaticos_torquimetro_foto) ? string.Empty : CheckList.ajuste_tornillos_neumaticos_torquimetro_foto;
				CheckList.limpiar_libricar_puertas_cerraduras_foto = string.IsNullOrWhiteSpace(CheckList.limpiar_libricar_puertas_cerraduras_foto) ? string.Empty : CheckList.limpiar_libricar_puertas_cerraduras_foto;
				CheckList.completar_plan_mantenimiento_foto = string.IsNullOrWhiteSpace(CheckList.completar_plan_mantenimiento_foto) ? string.Empty : CheckList.completar_plan_mantenimiento_foto;
				CheckList.otros_vehiculo_en_piso_foto = string.IsNullOrWhiteSpace(CheckList.otros_vehiculo_en_piso_foto) ? string.Empty : CheckList.otros_vehiculo_en_piso_foto;
				CheckList.otros_altura_media_foto = string.IsNullOrWhiteSpace(CheckList.otros_altura_media_foto) ? string.Empty : CheckList.otros_altura_media_foto;
				CheckList.otros_altura_total_foto = string.IsNullOrWhiteSpace(CheckList.otros_altura_total_foto) ? string.Empty : CheckList.otros_altura_total_foto;
				CheckList.otros_habitaculo_motor_foto = string.IsNullOrWhiteSpace(CheckList.otros_habitaculo_motor_foto) ? string.Empty : CheckList.otros_habitaculo_motor_foto;
				CheckList.lectura_codigos_observacion = string.IsNullOrWhiteSpace(CheckList.lectura_codigos_observacion) ? string.Empty : CheckList.lectura_codigos_observacion;
				CheckList.servofreno_observacion = string.IsNullOrWhiteSpace(CheckList.servofreno_observacion) ? string.Empty : CheckList.servofreno_observacion;
				CheckList.pedal_freno_observacion = string.IsNullOrWhiteSpace(CheckList.pedal_freno_observacion) ? string.Empty : CheckList.pedal_freno_observacion;
				CheckList.pedal_estacionamiento_observacion = string.IsNullOrWhiteSpace(CheckList.pedal_estacionamiento_observacion) ? string.Empty : CheckList.pedal_estacionamiento_observacion;
				CheckList.cinturon_seguridad_observacion = string.IsNullOrWhiteSpace(CheckList.cinturon_seguridad_observacion) ? string.Empty : CheckList.cinturon_seguridad_observacion;
				CheckList.cuadro_instrumentos_observacion = string.IsNullOrWhiteSpace(CheckList.cuadro_instrumentos_observacion) ? string.Empty : CheckList.cuadro_instrumentos_observacion;
				CheckList.aire_acondicionado_observacion = string.IsNullOrWhiteSpace(CheckList.aire_acondicionado_observacion) ? string.Empty : CheckList.aire_acondicionado_observacion;
				CheckList.bocina_claxon_observacion = string.IsNullOrWhiteSpace(CheckList.bocina_claxon_observacion) ? string.Empty : CheckList.bocina_claxon_observacion;
				CheckList.iluminacion_interior_observacion = string.IsNullOrWhiteSpace(CheckList.iluminacion_interior_observacion) ? string.Empty : CheckList.iluminacion_interior_observacion;
				CheckList.iluminacion_externa_observacion = string.IsNullOrWhiteSpace(CheckList.iluminacion_externa_observacion) ? string.Empty : CheckList.iluminacion_externa_observacion;
				CheckList.limpiaparabrisas_observacion = string.IsNullOrWhiteSpace(CheckList.limpiaparabrisas_observacion) ? string.Empty : CheckList.limpiaparabrisas_observacion;
				CheckList.limpia_medallon_observacion = string.IsNullOrWhiteSpace(CheckList.limpia_medallon_observacion) ? string.Empty : CheckList.limpia_medallon_observacion;
				CheckList.neumaticos_friccion_observacion = string.IsNullOrWhiteSpace(CheckList.neumaticos_friccion_observacion) ? string.Empty : CheckList.neumaticos_friccion_observacion;
				CheckList.estado_fugas_aceite_observacion = string.IsNullOrWhiteSpace(CheckList.estado_fugas_aceite_observacion) ? string.Empty : CheckList.estado_fugas_aceite_observacion;
				CheckList.estado_nivel_calidad_lubricante_transmision_observacion = string.IsNullOrWhiteSpace(CheckList.estado_nivel_calidad_lubricante_transmision_observacion) ? string.Empty : CheckList.estado_nivel_calidad_lubricante_transmision_observacion;
				CheckList.estado_nivel_calidad_lubricante_diferencial_observacion = string.IsNullOrWhiteSpace(CheckList.estado_nivel_calidad_lubricante_diferencial_observacion) ? string.Empty : CheckList.estado_nivel_calidad_lubricante_diferencial_observacion;
				CheckList.cubrepolvos_flechas_observacion = string.IsNullOrWhiteSpace(CheckList.cubrepolvos_flechas_observacion) ? string.Empty : CheckList.cubrepolvos_flechas_observacion;
				CheckList.componentes_direccion_observacion = string.IsNullOrWhiteSpace(CheckList.componentes_direccion_observacion) ? string.Empty : CheckList.componentes_direccion_observacion;
				CheckList.componentes_suspesion_observacion = string.IsNullOrWhiteSpace(CheckList.componentes_suspesion_observacion) ? string.Empty : CheckList.componentes_suspesion_observacion;
				CheckList.sistema_escape_completo_observacion = string.IsNullOrWhiteSpace(CheckList.sistema_escape_completo_observacion) ? string.Empty : CheckList.sistema_escape_completo_observacion;
				CheckList.sistema_alimentacion_combustible_observacion = string.IsNullOrWhiteSpace(CheckList.sistema_alimentacion_combustible_observacion) ? string.Empty : CheckList.sistema_alimentacion_combustible_observacion;
				CheckList.filtro_combustible_observacion = string.IsNullOrWhiteSpace(CheckList.filtro_combustible_observacion) ? string.Empty : CheckList.filtro_combustible_observacion;
				CheckList.control_fugas_direccion_hidraulica_observacion = string.IsNullOrWhiteSpace(CheckList.control_fugas_direccion_hidraulica_observacion) ? string.Empty : CheckList.control_fugas_direccion_hidraulica_observacion;
				CheckList.holgura_partes_suspension_rueda_observacion = string.IsNullOrWhiteSpace(CheckList.holgura_partes_suspension_rueda_observacion) ? string.Empty : CheckList.holgura_partes_suspension_rueda_observacion;
				CheckList.control_neumaticos_desgaste_presion_observacion = string.IsNullOrWhiteSpace(CheckList.control_neumaticos_desgaste_presion_observacion) ? string.Empty : CheckList.control_neumaticos_desgaste_presion_observacion;
				CheckList.profundidad_observacion = string.IsNullOrWhiteSpace(CheckList.profundidad_observacion) ? string.Empty : CheckList.profundidad_observacion;
				CheckList.presion_observacion = string.IsNullOrWhiteSpace(CheckList.presion_observacion) ? string.Empty : CheckList.presion_observacion;
				CheckList.nivel_calidad_aceite_motor_observacion = string.IsNullOrWhiteSpace(CheckList.nivel_calidad_aceite_motor_observacion) ? string.Empty : CheckList.nivel_calidad_aceite_motor_observacion;
				CheckList.filtro_aire_observacion = string.IsNullOrWhiteSpace(CheckList.filtro_aire_observacion) ? string.Empty : CheckList.filtro_aire_observacion;
				CheckList.filtro_polen_observacion = string.IsNullOrWhiteSpace(CheckList.filtro_polen_observacion) ? string.Empty : CheckList.filtro_polen_observacion;
				CheckList.filtro_pcv_observacion = string.IsNullOrWhiteSpace(CheckList.filtro_pcv_observacion) ? string.Empty : CheckList.filtro_pcv_observacion;
				CheckList.valvula_pcv_observacion = string.IsNullOrWhiteSpace(CheckList.valvula_pcv_observacion) ? string.Empty : CheckList.valvula_pcv_observacion;
				CheckList.bujias_encendido_observacion = string.IsNullOrWhiteSpace(CheckList.bujias_encendido_observacion) ? string.Empty : CheckList.bujias_encendido_observacion;
				CheckList.cables_bujias_bobinas_ignicion_observacion = string.IsNullOrWhiteSpace(CheckList.cables_bujias_bobinas_ignicion_observacion) ? string.Empty : CheckList.cables_bujias_bobinas_ignicion_observacion;
				CheckList.nivel_anticongenlante_observacion = string.IsNullOrWhiteSpace(CheckList.nivel_anticongenlante_observacion) ? string.Empty : CheckList.nivel_anticongenlante_observacion;
				CheckList.tapon_radiador_observacion = string.IsNullOrWhiteSpace(CheckList.tapon_radiador_observacion) ? string.Empty : CheckList.tapon_radiador_observacion;
				CheckList.mangueras_sistema_observacion = string.IsNullOrWhiteSpace(CheckList.mangueras_sistema_observacion) ? string.Empty : CheckList.mangueras_sistema_observacion;
				CheckList.desempeño_ventilador_observacion = string.IsNullOrWhiteSpace(CheckList.desempeño_ventilador_observacion) ? string.Empty : CheckList.desempeño_ventilador_observacion;
				CheckList.calidad_liquido_limpiaparabrisas_observacion = string.IsNullOrWhiteSpace(CheckList.calidad_liquido_limpiaparabrisas_observacion) ? string.Empty : CheckList.calidad_liquido_limpiaparabrisas_observacion;
				CheckList.calidad_aceite_direccion_hidraulica_observacion = string.IsNullOrWhiteSpace(CheckList.calidad_aceite_direccion_hidraulica_observacion) ? string.Empty : CheckList.calidad_aceite_direccion_hidraulica_observacion;
				CheckList.calidad_aceite_transmision_bayoneta_observacion = string.IsNullOrWhiteSpace(CheckList.calidad_aceite_transmision_bayoneta_observacion) ? string.Empty : CheckList.calidad_aceite_transmision_bayoneta_observacion;
				CheckList.liquido_bateria_condiciones_observacion = string.IsNullOrWhiteSpace(CheckList.liquido_bateria_condiciones_observacion) ? string.Empty : CheckList.liquido_bateria_condiciones_observacion;
				CheckList.bandas_poly_v_observacion = string.IsNullOrWhiteSpace(CheckList.bandas_poly_v_observacion) ? string.Empty : CheckList.bandas_poly_v_observacion;
				CheckList.poleas_banda_observacion = string.IsNullOrWhiteSpace(CheckList.poleas_banda_observacion) ? string.Empty : CheckList.poleas_banda_observacion;
				CheckList.banda_tiempo_observacion = string.IsNullOrWhiteSpace(CheckList.banda_tiempo_observacion) ? string.Empty : CheckList.banda_tiempo_observacion;
				CheckList.reset_intervalo_servicio_observacion = string.IsNullOrWhiteSpace(CheckList.reset_intervalo_servicio_observacion) ? string.Empty : CheckList.reset_intervalo_servicio_observacion;
				CheckList.ajuste_tornillos_neumaticos_torquimetro_observacion = string.IsNullOrWhiteSpace(CheckList.ajuste_tornillos_neumaticos_torquimetro_observacion) ? string.Empty : CheckList.ajuste_tornillos_neumaticos_torquimetro_observacion;
				CheckList.limpiar_libricar_puertas_cerraduras_observacion = string.IsNullOrWhiteSpace(CheckList.limpiar_libricar_puertas_cerraduras_observacion) ? string.Empty : CheckList.limpiar_libricar_puertas_cerraduras_observacion;
				CheckList.completar_plan_mantenimiento_observacion = string.IsNullOrWhiteSpace(CheckList.completar_plan_mantenimiento_observacion) ? string.Empty : CheckList.completar_plan_mantenimiento_observacion;


				CheckList.otros_vehiculo_en_piso_observacion = string.IsNullOrWhiteSpace(CheckList.otros_vehiculo_en_piso_observacion) ? string.Empty : CheckList.otros_vehiculo_en_piso_observacion;


				CheckList.otros_altura_media_observacion = string.IsNullOrWhiteSpace(CheckList.otros_altura_media_observacion) ? string.Empty : CheckList.otros_altura_media_observacion;


				CheckList.otros_altura_total_observacion = string.IsNullOrWhiteSpace(CheckList.otros_altura_total_observacion) ? string.Empty : CheckList.otros_altura_total_observacion;


				CheckList.otros_habitaculo_motor_observacion = string.IsNullOrWhiteSpace(CheckList.otros_habitaculo_motor_observacion) ? string.Empty : CheckList.otros_habitaculo_motor_observacion;


				CheckList.rodamiento_mazas_rueda_foto = string.IsNullOrWhiteSpace(CheckList.rodamiento_mazas_rueda_foto) ? string.Empty : CheckList.rodamiento_mazas_rueda_foto;
				CheckList.rodamiento_mazas_rueda_observacion = string.IsNullOrWhiteSpace(CheckList.rodamiento_mazas_rueda_observacion) ? string.Empty : CheckList.rodamiento_mazas_rueda_observacion;




				CheckList.Fecha = DateTime.Today.ToString("yyyy-MM-dd");

				if (DateTime.TryParse(CheckList.Fecha, out var parsedDate))
				{
					CheckList.Fecha = parsedDate.ToString("yyyy-MM-dd");
				}
				CheckList.NumeroSerie = SelectedVehiculo.No_serie;

				var idUsuario = await SecureStorage.GetAsync("idUsuario");

				if (!string.IsNullOrEmpty(idUsuario))
				{
					if (int.TryParse(idUsuario, out int idEmpleado))
					{
						CheckList.IdEmpleado = idEmpleado;



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
				CheckList.TiempoTranscurrido = TiempoTranscurrido;
				CheckList.IdVehiculo = SelectedVehiculo.ID;

				await _apiService.AddChecklistAsync(CheckList);

				if (tiempoTranscurridoSegundos > 2) // 40 minutos en segundos
				{
					// Crear el objeto Historico con los valores correspondientes
					var registroHistorico = new Historico
					{
						IdChecklist = CheckList.Id,
						IdVehiculo = CheckList.IdVehiculo,
						IdEmpleado = CheckList.IdEmpleado,
						Fecha = DateTime.Now,
						TiempoTranscurrido = tiempoTranscurridoSegundos,
						Estado = "Pendiente"
					};

					// Llamar al servicio para agregar el histórico
					await _apiService.AddHistoricoAsync(registroHistorico);
				}

				await Application.Current.MainPage.Navigation.PopAsync();
				await Application.Current.MainPage.DisplayAlert("Éxito", "Checklist guardado exitosamente", "OK");




			}
			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
			}
		}


        private async Task OnModificarCheck()
        {
            try
            {
                // Validación de campos obligatorios
             

                // Inicializar listas de fotos si son nulas
                CheckList.lectura_codigos_foto = string.IsNullOrWhiteSpace(CheckList.lectura_codigos_foto) ? string.Empty : CheckList.lectura_codigos_foto;
                CheckList.servofreno_foto = string.IsNullOrWhiteSpace(CheckList.servofreno_foto) ? string.Empty : CheckList.servofreno_foto;
                CheckList.pedal_freno_foto = string.IsNullOrWhiteSpace(CheckList.pedal_freno_foto) ? string.Empty : CheckList.pedal_freno_foto;
                CheckList.pedal_estacionamiento_foto = string.IsNullOrWhiteSpace(CheckList.pedal_estacionamiento_foto) ? string.Empty : CheckList.pedal_estacionamiento_foto;
                CheckList.cinturon_seguridad_foto = string.IsNullOrWhiteSpace(CheckList.cinturon_seguridad_foto) ? string.Empty : CheckList.cinturon_seguridad_foto;
                CheckList.cuadro_instrumentos_foto = string.IsNullOrWhiteSpace(CheckList.cuadro_instrumentos_foto) ? string.Empty : CheckList.cuadro_instrumentos_foto;
                CheckList.aire_acondicionado_foto = string.IsNullOrWhiteSpace(CheckList.aire_acondicionado_foto) ? string.Empty : CheckList.aire_acondicionado_foto;
                CheckList.bocina_claxon_foto = string.IsNullOrWhiteSpace(CheckList.bocina_claxon_foto) ? string.Empty : CheckList.bocina_claxon_foto;
                CheckList.iluminacion_interior_foto = string.IsNullOrWhiteSpace(CheckList.iluminacion_interior_foto) ? string.Empty : CheckList.iluminacion_interior_foto;
                CheckList.iluminacion_externa_foto = string.IsNullOrWhiteSpace(CheckList.iluminacion_externa_foto) ? string.Empty : CheckList.iluminacion_externa_foto;
                CheckList.limpiaparabrisas_foto = string.IsNullOrWhiteSpace(CheckList.limpiaparabrisas_foto) ? string.Empty : CheckList.limpiaparabrisas_foto;
                CheckList.limpia_medallon_foto = string.IsNullOrWhiteSpace(CheckList.limpia_medallon_foto) ? string.Empty : CheckList.limpia_medallon_foto;
                CheckList.neumaticos_friccion_foto = string.IsNullOrWhiteSpace(CheckList.neumaticos_friccion_foto) ? string.Empty : CheckList.neumaticos_friccion_foto;
                CheckList.estado_fugas_aceite_foto = string.IsNullOrWhiteSpace(CheckList.estado_fugas_aceite_foto) ? string.Empty : CheckList.estado_fugas_aceite_foto;
                CheckList.estado_nivel_calidad_lubricante_transmision_foto = string.IsNullOrWhiteSpace(CheckList.estado_nivel_calidad_lubricante_transmision_foto) ? string.Empty : CheckList.estado_nivel_calidad_lubricante_transmision_foto;
                CheckList.estado_nivel_calidad_lubricante_diferencial_foto = string.IsNullOrWhiteSpace(CheckList.estado_nivel_calidad_lubricante_diferencial_foto) ? string.Empty : CheckList.estado_nivel_calidad_lubricante_diferencial_foto;
                CheckList.cubrepolvos_flechas_foto = string.IsNullOrWhiteSpace(CheckList.cubrepolvos_flechas_foto) ? string.Empty : CheckList.cubrepolvos_flechas_foto;
                CheckList.componentes_direccion_foto = string.IsNullOrWhiteSpace(CheckList.componentes_direccion_foto) ? string.Empty : CheckList.componentes_direccion_foto;
                CheckList.componentes_suspesion_foto = string.IsNullOrWhiteSpace(CheckList.componentes_suspesion_foto) ? string.Empty : CheckList.componentes_suspesion_foto;
                CheckList.sistema_escape_completo_foto = string.IsNullOrWhiteSpace(CheckList.sistema_escape_completo_foto) ? string.Empty : CheckList.sistema_escape_completo_foto;
                CheckList.sistema_alimentacion_combustible_foto = string.IsNullOrWhiteSpace(CheckList.sistema_alimentacion_combustible_foto) ? string.Empty : CheckList.sistema_alimentacion_combustible_foto;
                CheckList.filtro_combustible_foto = string.IsNullOrWhiteSpace(CheckList.filtro_combustible_foto) ? string.Empty : CheckList.filtro_combustible_foto;
                CheckList.control_fugas_direccion_hidraulica_foto = string.IsNullOrWhiteSpace(CheckList.control_fugas_direccion_hidraulica_foto) ? string.Empty : CheckList.control_fugas_direccion_hidraulica_foto;
                CheckList.holgura_partes_suspension_rueda_foto = string.IsNullOrWhiteSpace(CheckList.holgura_partes_suspension_rueda_foto) ? string.Empty : CheckList.holgura_partes_suspension_rueda_foto;
                CheckList.control_neumaticos_desgaste_presion_foto = string.IsNullOrWhiteSpace(CheckList.control_neumaticos_desgaste_presion_foto) ? string.Empty : CheckList.control_neumaticos_desgaste_presion_foto;
                CheckList.profundidad_foto = string.IsNullOrWhiteSpace(CheckList.profundidad_foto) ? string.Empty : CheckList.profundidad_foto;
                CheckList.presion_foto = string.IsNullOrWhiteSpace(CheckList.presion_foto) ? string.Empty : CheckList.presion_foto;
                CheckList.nivel_calidad_aceite_motor_foto = string.IsNullOrWhiteSpace(CheckList.nivel_calidad_aceite_motor_foto) ? string.Empty : CheckList.nivel_calidad_aceite_motor_foto;
                CheckList.filtro_aire_foto = string.IsNullOrWhiteSpace(CheckList.filtro_aire_foto) ? string.Empty : CheckList.filtro_aire_foto;
                CheckList.filtro_polen_foto = string.IsNullOrWhiteSpace(CheckList.filtro_polen_foto) ? string.Empty : CheckList.filtro_polen_foto;
                CheckList.filtro_pcv_foto = string.IsNullOrWhiteSpace(CheckList.filtro_pcv_foto) ? string.Empty : CheckList.filtro_pcv_foto;
                CheckList.valvula_pcv_foto = string.IsNullOrWhiteSpace(CheckList.valvula_pcv_foto) ? string.Empty : CheckList.valvula_pcv_foto;
                CheckList.bujias_encendido_foto = string.IsNullOrWhiteSpace(CheckList.bujias_encendido_foto) ? string.Empty : CheckList.bujias_encendido_foto;
                CheckList.cables_bujias_bobinas_ignicion_foto = string.IsNullOrWhiteSpace(CheckList.cables_bujias_bobinas_ignicion_foto) ? string.Empty : CheckList.cables_bujias_bobinas_ignicion_foto;
                CheckList.nivel_anticongenlante_foto = string.IsNullOrWhiteSpace(CheckList.nivel_anticongenlante_foto) ? string.Empty : CheckList.nivel_anticongenlante_foto;
                CheckList.tapon_radiador_foto = string.IsNullOrWhiteSpace(CheckList.tapon_radiador_foto) ? string.Empty : CheckList.tapon_radiador_foto;
                CheckList.mangueras_sistema_foto = string.IsNullOrWhiteSpace(CheckList.mangueras_sistema_foto) ? string.Empty : CheckList.mangueras_sistema_foto;
                CheckList.desempeño_ventilador_foto = string.IsNullOrWhiteSpace(CheckList.desempeño_ventilador_foto) ? string.Empty : CheckList.desempeño_ventilador_foto;
                CheckList.calidad_liquido_limpiaparabrisas_foto = string.IsNullOrWhiteSpace(CheckList.calidad_liquido_limpiaparabrisas_foto) ? string.Empty : CheckList.calidad_liquido_limpiaparabrisas_foto;
                CheckList.calidad_aceite_direccion_hidraulica_foto = string.IsNullOrWhiteSpace(CheckList.calidad_aceite_direccion_hidraulica_foto) ? string.Empty : CheckList.calidad_aceite_direccion_hidraulica_foto;
                CheckList.calidad_aceite_transmision_bayoneta_foto = string.IsNullOrWhiteSpace(CheckList.calidad_aceite_transmision_bayoneta_foto) ? string.Empty : CheckList.calidad_aceite_transmision_bayoneta_foto;
                CheckList.liquido_bateria_condiciones_foto = string.IsNullOrWhiteSpace(CheckList.liquido_bateria_condiciones_foto) ? string.Empty : CheckList.liquido_bateria_condiciones_foto;
                CheckList.bandas_poly_v_foto = string.IsNullOrWhiteSpace(CheckList.bandas_poly_v_foto) ? string.Empty : CheckList.bandas_poly_v_foto;
                CheckList.poleas_banda_foto = string.IsNullOrWhiteSpace(CheckList.poleas_banda_foto) ? string.Empty : CheckList.poleas_banda_foto;
                CheckList.banda_tiempo_foto = string.IsNullOrWhiteSpace(CheckList.banda_tiempo_foto) ? string.Empty : CheckList.banda_tiempo_foto;
                CheckList.reset_intervalo_servicio_foto = string.IsNullOrWhiteSpace(CheckList.reset_intervalo_servicio_foto) ? string.Empty : CheckList.reset_intervalo_servicio_foto;
                CheckList.ajuste_tornillos_neumaticos_torquimetro_foto = string.IsNullOrWhiteSpace(CheckList.ajuste_tornillos_neumaticos_torquimetro_foto) ? string.Empty : CheckList.ajuste_tornillos_neumaticos_torquimetro_foto;
                CheckList.limpiar_libricar_puertas_cerraduras_foto = string.IsNullOrWhiteSpace(CheckList.limpiar_libricar_puertas_cerraduras_foto) ? string.Empty : CheckList.limpiar_libricar_puertas_cerraduras_foto;
                CheckList.completar_plan_mantenimiento_foto = string.IsNullOrWhiteSpace(CheckList.completar_plan_mantenimiento_foto) ? string.Empty : CheckList.completar_plan_mantenimiento_foto;
                CheckList.otros_vehiculo_en_piso_foto = string.IsNullOrWhiteSpace(CheckList.otros_vehiculo_en_piso_foto) ? string.Empty : CheckList.otros_vehiculo_en_piso_foto;
                CheckList.otros_altura_media_foto = string.IsNullOrWhiteSpace(CheckList.otros_altura_media_foto) ? string.Empty : CheckList.otros_altura_media_foto;
                CheckList.otros_altura_total_foto = string.IsNullOrWhiteSpace(CheckList.otros_altura_total_foto) ? string.Empty : CheckList.otros_altura_total_foto;
                CheckList.otros_habitaculo_motor_foto = string.IsNullOrWhiteSpace(CheckList.otros_habitaculo_motor_foto) ? string.Empty : CheckList.otros_habitaculo_motor_foto;
                CheckList.lectura_codigos_observacion = string.IsNullOrWhiteSpace(CheckList.lectura_codigos_observacion) ? string.Empty : CheckList.lectura_codigos_observacion;
                CheckList.servofreno_observacion = string.IsNullOrWhiteSpace(CheckList.servofreno_observacion) ? string.Empty : CheckList.servofreno_observacion;
                CheckList.pedal_freno_observacion = string.IsNullOrWhiteSpace(CheckList.pedal_freno_observacion) ? string.Empty : CheckList.pedal_freno_observacion;
                CheckList.pedal_estacionamiento_observacion = string.IsNullOrWhiteSpace(CheckList.pedal_estacionamiento_observacion) ? string.Empty : CheckList.pedal_estacionamiento_observacion;
                CheckList.cinturon_seguridad_observacion = string.IsNullOrWhiteSpace(CheckList.cinturon_seguridad_observacion) ? string.Empty : CheckList.cinturon_seguridad_observacion;
                CheckList.cuadro_instrumentos_observacion = string.IsNullOrWhiteSpace(CheckList.cuadro_instrumentos_observacion) ? string.Empty : CheckList.cuadro_instrumentos_observacion;
                CheckList.aire_acondicionado_observacion = string.IsNullOrWhiteSpace(CheckList.aire_acondicionado_observacion) ? string.Empty : CheckList.aire_acondicionado_observacion;
                CheckList.bocina_claxon_observacion = string.IsNullOrWhiteSpace(CheckList.bocina_claxon_observacion) ? string.Empty : CheckList.bocina_claxon_observacion;
                CheckList.iluminacion_interior_observacion = string.IsNullOrWhiteSpace(CheckList.iluminacion_interior_observacion) ? string.Empty : CheckList.iluminacion_interior_observacion;
                CheckList.iluminacion_externa_observacion = string.IsNullOrWhiteSpace(CheckList.iluminacion_externa_observacion) ? string.Empty : CheckList.iluminacion_externa_observacion;
                CheckList.limpiaparabrisas_observacion = string.IsNullOrWhiteSpace(CheckList.limpiaparabrisas_observacion) ? string.Empty : CheckList.limpiaparabrisas_observacion;
                CheckList.limpia_medallon_observacion = string.IsNullOrWhiteSpace(CheckList.limpia_medallon_observacion) ? string.Empty : CheckList.limpia_medallon_observacion;
                CheckList.neumaticos_friccion_observacion = string.IsNullOrWhiteSpace(CheckList.neumaticos_friccion_observacion) ? string.Empty : CheckList.neumaticos_friccion_observacion;
                CheckList.estado_fugas_aceite_observacion = string.IsNullOrWhiteSpace(CheckList.estado_fugas_aceite_observacion) ? string.Empty : CheckList.estado_fugas_aceite_observacion;
                CheckList.estado_nivel_calidad_lubricante_transmision_observacion = string.IsNullOrWhiteSpace(CheckList.estado_nivel_calidad_lubricante_transmision_observacion) ? string.Empty : CheckList.estado_nivel_calidad_lubricante_transmision_observacion;
                CheckList.estado_nivel_calidad_lubricante_diferencial_observacion = string.IsNullOrWhiteSpace(CheckList.estado_nivel_calidad_lubricante_diferencial_observacion) ? string.Empty : CheckList.estado_nivel_calidad_lubricante_diferencial_observacion;
                CheckList.cubrepolvos_flechas_observacion = string.IsNullOrWhiteSpace(CheckList.cubrepolvos_flechas_observacion) ? string.Empty : CheckList.cubrepolvos_flechas_observacion;
                CheckList.componentes_direccion_observacion = string.IsNullOrWhiteSpace(CheckList.componentes_direccion_observacion) ? string.Empty : CheckList.componentes_direccion_observacion;
                CheckList.componentes_suspesion_observacion = string.IsNullOrWhiteSpace(CheckList.componentes_suspesion_observacion) ? string.Empty : CheckList.componentes_suspesion_observacion;
                CheckList.sistema_escape_completo_observacion = string.IsNullOrWhiteSpace(CheckList.sistema_escape_completo_observacion) ? string.Empty : CheckList.sistema_escape_completo_observacion;
                CheckList.sistema_alimentacion_combustible_observacion = string.IsNullOrWhiteSpace(CheckList.sistema_alimentacion_combustible_observacion) ? string.Empty : CheckList.sistema_alimentacion_combustible_observacion;
                CheckList.filtro_combustible_observacion = string.IsNullOrWhiteSpace(CheckList.filtro_combustible_observacion) ? string.Empty : CheckList.filtro_combustible_observacion;
                CheckList.control_fugas_direccion_hidraulica_observacion = string.IsNullOrWhiteSpace(CheckList.control_fugas_direccion_hidraulica_observacion) ? string.Empty : CheckList.control_fugas_direccion_hidraulica_observacion;
                CheckList.holgura_partes_suspension_rueda_observacion = string.IsNullOrWhiteSpace(CheckList.holgura_partes_suspension_rueda_observacion) ? string.Empty : CheckList.holgura_partes_suspension_rueda_observacion;
                CheckList.control_neumaticos_desgaste_presion_observacion = string.IsNullOrWhiteSpace(CheckList.control_neumaticos_desgaste_presion_observacion) ? string.Empty : CheckList.control_neumaticos_desgaste_presion_observacion;
                CheckList.profundidad_observacion = string.IsNullOrWhiteSpace(CheckList.profundidad_observacion) ? string.Empty : CheckList.profundidad_observacion;
                CheckList.presion_observacion = string.IsNullOrWhiteSpace(CheckList.presion_observacion) ? string.Empty : CheckList.presion_observacion;
                CheckList.nivel_calidad_aceite_motor_observacion = string.IsNullOrWhiteSpace(CheckList.nivel_calidad_aceite_motor_observacion) ? string.Empty : CheckList.nivel_calidad_aceite_motor_observacion;
                CheckList.filtro_aire_observacion = string.IsNullOrWhiteSpace(CheckList.filtro_aire_observacion) ? string.Empty : CheckList.filtro_aire_observacion;
                CheckList.filtro_polen_observacion = string.IsNullOrWhiteSpace(CheckList.filtro_polen_observacion) ? string.Empty : CheckList.filtro_polen_observacion;
                CheckList.filtro_pcv_observacion = string.IsNullOrWhiteSpace(CheckList.filtro_pcv_observacion) ? string.Empty : CheckList.filtro_pcv_observacion;
                CheckList.valvula_pcv_observacion = string.IsNullOrWhiteSpace(CheckList.valvula_pcv_observacion) ? string.Empty : CheckList.valvula_pcv_observacion;
                CheckList.bujias_encendido_observacion = string.IsNullOrWhiteSpace(CheckList.bujias_encendido_observacion) ? string.Empty : CheckList.bujias_encendido_observacion;
                CheckList.cables_bujias_bobinas_ignicion_observacion = string.IsNullOrWhiteSpace(CheckList.cables_bujias_bobinas_ignicion_observacion) ? string.Empty : CheckList.cables_bujias_bobinas_ignicion_observacion;
                CheckList.nivel_anticongenlante_observacion = string.IsNullOrWhiteSpace(CheckList.nivel_anticongenlante_observacion) ? string.Empty : CheckList.nivel_anticongenlante_observacion;
                CheckList.tapon_radiador_observacion = string.IsNullOrWhiteSpace(CheckList.tapon_radiador_observacion) ? string.Empty : CheckList.tapon_radiador_observacion;
                CheckList.mangueras_sistema_observacion = string.IsNullOrWhiteSpace(CheckList.mangueras_sistema_observacion) ? string.Empty : CheckList.mangueras_sistema_observacion;
                CheckList.desempeño_ventilador_observacion = string.IsNullOrWhiteSpace(CheckList.desempeño_ventilador_observacion) ? string.Empty : CheckList.desempeño_ventilador_observacion;
                CheckList.calidad_liquido_limpiaparabrisas_observacion = string.IsNullOrWhiteSpace(CheckList.calidad_liquido_limpiaparabrisas_observacion) ? string.Empty : CheckList.calidad_liquido_limpiaparabrisas_observacion;
                CheckList.calidad_aceite_direccion_hidraulica_observacion = string.IsNullOrWhiteSpace(CheckList.calidad_aceite_direccion_hidraulica_observacion) ? string.Empty : CheckList.calidad_aceite_direccion_hidraulica_observacion;
                CheckList.calidad_aceite_transmision_bayoneta_observacion = string.IsNullOrWhiteSpace(CheckList.calidad_aceite_transmision_bayoneta_observacion) ? string.Empty : CheckList.calidad_aceite_transmision_bayoneta_observacion;
                CheckList.liquido_bateria_condiciones_observacion = string.IsNullOrWhiteSpace(CheckList.liquido_bateria_condiciones_observacion) ? string.Empty : CheckList.liquido_bateria_condiciones_observacion;
                CheckList.bandas_poly_v_observacion = string.IsNullOrWhiteSpace(CheckList.bandas_poly_v_observacion) ? string.Empty : CheckList.bandas_poly_v_observacion;
                CheckList.poleas_banda_observacion = string.IsNullOrWhiteSpace(CheckList.poleas_banda_observacion) ? string.Empty : CheckList.poleas_banda_observacion;
                CheckList.banda_tiempo_observacion = string.IsNullOrWhiteSpace(CheckList.banda_tiempo_observacion) ? string.Empty : CheckList.banda_tiempo_observacion;
                CheckList.reset_intervalo_servicio_observacion = string.IsNullOrWhiteSpace(CheckList.reset_intervalo_servicio_observacion) ? string.Empty : CheckList.reset_intervalo_servicio_observacion;
                CheckList.ajuste_tornillos_neumaticos_torquimetro_observacion = string.IsNullOrWhiteSpace(CheckList.ajuste_tornillos_neumaticos_torquimetro_observacion) ? string.Empty : CheckList.ajuste_tornillos_neumaticos_torquimetro_observacion;
                CheckList.limpiar_libricar_puertas_cerraduras_observacion = string.IsNullOrWhiteSpace(CheckList.limpiar_libricar_puertas_cerraduras_observacion) ? string.Empty : CheckList.limpiar_libricar_puertas_cerraduras_observacion;
                CheckList.completar_plan_mantenimiento_observacion = string.IsNullOrWhiteSpace(CheckList.completar_plan_mantenimiento_observacion) ? string.Empty : CheckList.completar_plan_mantenimiento_observacion;


                CheckList.otros_vehiculo_en_piso_observacion = string.IsNullOrWhiteSpace(CheckList.otros_vehiculo_en_piso_observacion) ? string.Empty : CheckList.otros_vehiculo_en_piso_observacion;


                CheckList.otros_altura_media_observacion = string.IsNullOrWhiteSpace(CheckList.otros_altura_media_observacion) ? string.Empty : CheckList.otros_altura_media_observacion;


                CheckList.otros_altura_total_observacion = string.IsNullOrWhiteSpace(CheckList.otros_altura_total_observacion) ? string.Empty : CheckList.otros_altura_total_observacion;


                CheckList.otros_habitaculo_motor_observacion = string.IsNullOrWhiteSpace(CheckList.otros_habitaculo_motor_observacion) ? string.Empty : CheckList.otros_habitaculo_motor_observacion;


                CheckList.rodamiento_mazas_rueda_foto = string.IsNullOrWhiteSpace(CheckList.rodamiento_mazas_rueda_foto) ? string.Empty : CheckList.rodamiento_mazas_rueda_foto;
                CheckList.rodamiento_mazas_rueda_observacion = string.IsNullOrWhiteSpace(CheckList.rodamiento_mazas_rueda_observacion) ? string.Empty : CheckList.rodamiento_mazas_rueda_observacion;




                CheckList.Fecha = DateTime.Today.ToString("yyyy-MM-dd");

                if (DateTime.TryParse(CheckList.Fecha, out var parsedDate))
                {
                    CheckList.Fecha = parsedDate.ToString("yyyy-MM-dd");
                }
                CheckList.NumeroSerie = NumeroSerie;

                var idUsuario = await SecureStorage.GetAsync("idUsuario");

                if (!string.IsNullOrEmpty(idUsuario))
                {
                    if (int.TryParse(idUsuario, out int idEmpleado))
                    {
                        CheckList.IdEmpleado = idEmpleado;



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
                CheckList.TiempoTranscurrido = TiempoTranscurrido;
                CheckList.IdVehiculo = IdVehiculo;

                // Realizar la actualización del vehículo en el servidor

                bool isUpdated = await _apiService.UpdateCheckAsync(CheckList);

                if (isUpdated)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Check modificado correctamente.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error al modificar el check.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al modificar el check: {ex.Message}", "OK");
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
				Directory = "CheckList",
				Name = $"{propiedad}.jpg"
			});

			if (file != null)
			{
				// Redimensionar y comprimir la imagen
				var base64Compressed = CompressImage(file, 500, 200); // Ajusta el tamaño y calidad
				switch (propiedad)
				{
					case "lectura_codigos_foto":
						CheckList.lectura_codigos_foto = base64Compressed;
						break;
					case "servofreno_foto":
						CheckList.servofreno_foto = base64Compressed;
						break;

					case "pedal_freno_foto":
						CheckList.pedal_freno_foto = base64Compressed;
						break;
					case "pedal_estacionamiento_foto":
						CheckList.pedal_estacionamiento_foto = base64Compressed;
						break;
					case "cinturon_seguridad_foto":
						CheckList.cinturon_seguridad_foto = base64Compressed;
						break;
					case "cuadro_instrumentos_foto":
						CheckList.cuadro_instrumentos_foto = base64Compressed;
						break;
					case "aire_acondicionado_foto":
						CheckList.aire_acondicionado_foto = base64Compressed;
						break;
					case "bocina_claxon_foto":
						CheckList.bocina_claxon_foto = base64Compressed;
						break;
					case "iluminacion_interior_foto":
						CheckList.iluminacion_interior_foto = base64Compressed;
						break;
					case "iluminacion_externa_foto":
						CheckList.iluminacion_externa_foto = base64Compressed;
						break;
					case "limpiaparabrisas_foto":
						CheckList.limpiaparabrisas_foto = base64Compressed;
						break;
					case "limpia_medallon_foto":
						CheckList.limpia_medallon_foto = base64Compressed;
						break;
					case "neumaticos_friccion_foto":
						CheckList.neumaticos_friccion_foto = base64Compressed;
						break;
					case "otros_vehiculo_en_piso_foto":
						CheckList.otros_vehiculo_en_piso_foto = base64Compressed;
						break;
					case "estado_fugas_aceite_foto":
						CheckList.estado_fugas_aceite_foto = base64Compressed;
						break;
					case "estado_nivel_calidad_lubricante_transmision_foto":
						CheckList.estado_nivel_calidad_lubricante_transmision_foto = base64Compressed;
						break;
					case "estado_nivel_calidad_lubricante_diferencial_foto":
						CheckList.estado_nivel_calidad_lubricante_diferencial_foto = base64Compressed;
						break;
					case "cubrepolvos_flechas_foto":
						CheckList.cubrepolvos_flechas_foto = base64Compressed;
						break;
					case "componentes_direccion_foto":
						CheckList.componentes_direccion_foto = base64Compressed;
						break;
					case "componentes_suspesion_foto":
						CheckList.componentes_suspesion_foto = base64Compressed;
						break;
					case "sistema_escape_completo_foto":
						CheckList.sistema_escape_completo_foto = base64Compressed;
						break;
					case "sistema_alimentacion_combustible_foto":
						CheckList.sistema_alimentacion_combustible_foto = base64Compressed;
						break;
					case "filtro_combustible_foto":
						CheckList.filtro_combustible_foto = base64Compressed;
						break;
					case "control_fugas_direccion_hidraulica_foto":
						CheckList.control_fugas_direccion_hidraulica_foto = base64Compressed;
						break;
					case "otros_altura_media_foto":
						CheckList.otros_altura_media_foto = base64Compressed;
						break;
					case "holgura_partes_suspension_rueda_foto":
						CheckList.holgura_partes_suspension_rueda_foto = base64Compressed;
						break;
					case "control_neumaticos_desgaste_presion_foto":
						CheckList.control_neumaticos_desgaste_presion_foto = base64Compressed;
						break;
					case "profundidad_foto":
						CheckList.profundidad_foto = base64Compressed;
						break;
					case "presion_foto":
						CheckList.presion_foto = base64Compressed;
						break;
					case "otros_altura_total_foto":
						CheckList.otros_altura_total_foto = base64Compressed;
						break;
					case "nivel_calidad_aceite_motor_foto":
						CheckList.nivel_calidad_aceite_motor_foto = base64Compressed;
						break;
					case "filtro_aire_foto":
						CheckList.filtro_aire_foto = base64Compressed;
						break;
					case "filtro_polen_foto":
						CheckList.filtro_polen_foto = base64Compressed;
						break;
					case "filtro_pcv_foto":
						CheckList.filtro_pcv_foto = base64Compressed;
						break;
					case "valvula_pcv_foto":
						CheckList.valvula_pcv_foto = base64Compressed;
						break;
					case "bujias_encendido_foto":
						CheckList.bujias_encendido_foto = base64Compressed;
						break;
					case "cables_bujias_bobinas_ignicion_foto":
						CheckList.cables_bujias_bobinas_ignicion_foto = base64Compressed;
						break;
					case "nivel_anticongenlante_foto":
						CheckList.nivel_anticongenlante_foto = base64Compressed;
						break;
					case "tapon_radiador_foto":
						CheckList.tapon_radiador_foto = base64Compressed;
						break;
					case "mangueras_sistema_foto":
						CheckList.mangueras_sistema_foto = base64Compressed;
						break;
					case "desempeño_ventilador_foto":
						CheckList.desempeño_ventilador_foto = base64Compressed;
						break;
					case "calidad_liquido_limpiaparabrisas_foto":
						CheckList.calidad_liquido_limpiaparabrisas_foto = base64Compressed;
						break;
					case "calidad_aceite_direccion_hidraulica_foto":
						CheckList.calidad_aceite_direccion_hidraulica_foto = base64Compressed;
						break;
					case "calidad_aceite_transmision_bayoneta_foto":
						CheckList.calidad_aceite_transmision_bayoneta_foto = base64Compressed;
						break;
					case "liquido_bateria_condiciones_foto":
						CheckList.liquido_bateria_condiciones_foto = base64Compressed;
						break;
					case "bandas_poly_v_foto":
						CheckList.bandas_poly_v_foto = base64Compressed;
						break;
					case "poleas_banda_foto":
						CheckList.poleas_banda_foto = base64Compressed;
						break;
					case "banda_tiempo_foto":
						CheckList.banda_tiempo_foto = base64Compressed;
						break;
					case "otros_habitaculo_motor_foto":
						CheckList.otros_habitaculo_motor_foto = base64Compressed;
						break;
					case "reset_intervalo_servicio_foto":
						CheckList.reset_intervalo_servicio_foto = base64Compressed;
						break;
					case "ajuste_tornillos_neumaticos_torquimetro_foto":
						CheckList.ajuste_tornillos_neumaticos_torquimetro_foto = base64Compressed;
						break;
					case "limpiar_libricar_puertas_cerraduras_foto":
						CheckList.limpiar_libricar_puertas_cerraduras_foto = base64Compressed;
						break;
					case "completar_plan_mantenimiento_foto":
						CheckList.completar_plan_mantenimiento_foto = base64Compressed;
						break;
					case "rodamiento_mazas_rueda_foto":
						CheckList.rodamiento_mazas_rueda_foto = base64Compressed;
						break;
				}
			}

			OnPropertyChanged(nameof(CheckList));


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
        private void ActualizarNumeroSerie()
        {
            if (Vehiculos == null || Vehiculos.Count == 0)
            {
                Debug.WriteLine("La lista de vehículos está vacía.");
                NumeroSerie = "No se encontraron vehículos";
                return;
            }

            var vehiculo = Vehiculos.FirstOrDefault(v => v.ID == CheckList.IdVehiculo);

            if (vehiculo != null)
            {
                NumeroSerie = vehiculo.No_serie;
            }
            else
            {
                Debug.WriteLine($"No se encontró un vehículo con IdVehiculo: {CheckList.IdVehiculo}");
                NumeroSerie = "Vehículo no encontrado";
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

	}
}
