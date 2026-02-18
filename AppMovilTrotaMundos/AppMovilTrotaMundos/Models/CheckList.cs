using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Models
{
	public class CheckList
	{
        // Propiedades generales del checklist
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public string Fecha { get; set; }
		public int IdEmpleado { get; set; }

		public int IdVehiculo { get; set; }
		public int Id_ordendeservicio { get; set; }
		public string NumeroSerie { get; set; }
		public int Id { get; set; }


    
        public int lectura_codigos { get; set; }
        
        public int servofreno { get; set; }
		public int pedal_freno { get; set; }
		public int pedal_estacionamiento { get; set; }
		public int cinturon_seguridad { get; set; }
		public int cuadro_instrumentos { get; set; }
		public int aire_acondicionado { get; set; }
		public int bocina_claxon { get; set; }
		public int iluminacion_interior { get; set; }
		public int iluminacion_externa { get; set; }
		public int limpiaparabrisas { get; set; }
		public int limpia_medallon { get; set; }
		public int neumaticos_friccion { get; set; }
		public int otros_vehiculo_en_piso { get; set; }
		public int estado_fugas_aceite { get; set; }
		public int estado_nivel_calidad_lubricante_transmision { get; set; }
		public int estado_nivel_calidad_lubricante_diferencial { get; set; }
		public int cubrepolvos_flechas { get; set; }
		public int componentes_direccion { get; set; }
		public int componentes_suspesion { get; set; }
		public int sistema_escape_completo { get; set; }
		public int sistema_alimentacion_combustible { get; set; }
		public int filtro_combustible { get; set; }
		public int control_fugas_direccion_hidraulica { get; set; }
		public int otros_altura_total { get; set; }
		public int rodamiento_mazas_rueda { get; set; }
		public int holgura_partes_suspension_rueda { get; set; }
		public int control_neumaticos_desgaste_presion { get; set; }
		public int profundidad { get; set; }
		public int presion { get; set; }
		public int otros_altura_media { get; set; }
		public int nivel_calidad_aceite_motor { get; set; }
		public int filtro_aire { get; set; }
		public int filtro_polen { get; set; }
		public int filtro_pcv { get; set; }
		public int valvula_pcv { get; set; }
		public int bujias_encendido { get; set; }
		public int cables_bujias_bobinas_ignicion { get; set; }
		public int nivel_anticongenlante { get; set; }
		public int tapon_radiador { get; set; }
		public int mangueras_sistema { get; set; }
		public int desempeño_ventilador { get; set; }
		public int calidad_liquido_limpiaparabrisas { get; set; }
		public int calidad_aceite_direccion_hidraulica { get; set; }
		public int calidad_aceite_transmision_bayoneta { get; set; }
		public int liquido_bateria_condiciones { get; set; }
		public int bandas_poly_v { get; set; }
		public int poleas_banda { get; set; }
		public int banda_tiempo { get; set; }
		public int otros_habitaculo_motor { get; set; }
		public int reset_intervalo_servicio { get; set; }
		public int ajuste_tornillos_neumaticos_torquimetro { get; set; }
		public int limpiar_libricar_puertas_cerraduras { get; set; }
		public int completar_plan_mantenimiento { get; set; }

		public string lectura_codigos_observacion { get; set; }
		public string servofreno_observacion { get; set; }
		public string pedal_freno_observacion { get; set; }
		public string pedal_estacionamiento_observacion { get; set; }
		public string cinturon_seguridad_observacion { get; set; }
		public string cuadro_instrumentos_observacion { get; set; }
		public string aire_acondicionado_observacion { get; set; }
		public string bocina_claxon_observacion { get; set; }
		public string iluminacion_interior_observacion { get; set; }
		public string iluminacion_externa_observacion { get; set; }
		public string limpiaparabrisas_observacion { get; set; }
		public string limpia_medallon_observacion { get; set; }
		public string neumaticos_friccion_observacion { get; set; }
		public string otros_vehiculo_en_piso_observacion { get; set; }
		public string estado_fugas_aceite_observacion { get; set; }
		public string estado_nivel_calidad_lubricante_transmision_observacion { get; set; }
		public string estado_nivel_calidad_lubricante_diferencial_observacion { get; set; }
		public string cubrepolvos_flechas_observacion { get; set; }
		public string componentes_direccion_observacion { get; set; }
		public string componentes_suspesion_observacion { get; set; }
		public string sistema_escape_completo_observacion { get; set; }
		public string sistema_alimentacion_combustible_observacion { get; set; }
		public string filtro_combustible_observacion { get; set; }
		public string control_fugas_direccion_hidraulica_observacion { get; set; }
		public string otros_altura_media_observacion { get; set; }
		public string rodamiento_mazas_rueda_observacion { get; set; }
		public string holgura_partes_suspension_rueda_observacion { get; set; }
		public string control_neumaticos_desgaste_presion_observacion { get; set; }
		public string profundidad_observacion { get; set; }
		public string presion_observacion { get; set; }
		public string otros_altura_total_observacion { get; set; }
		public string nivel_calidad_aceite_motor_observacion { get; set; }
		public string filtro_aire_observacion { get; set; }
		public string filtro_polen_observacion { get; set; }
		public string filtro_pcv_observacion { get; set; }
		public string valvula_pcv_observacion { get; set; }
		public string bujias_encendido_observacion { get; set; }
		public string cables_bujias_bobinas_ignicion_observacion { get; set; }
		public string nivel_anticongenlante_observacion { get; set; }
		public string tapon_radiador_observacion { get; set; }
		public string mangueras_sistema_observacion { get; set; }
		public string desempeño_ventilador_observacion { get; set; }
		public string calidad_liquido_limpiaparabrisas_observacion { get; set; }
		public string calidad_aceite_direccion_hidraulica_observacion { get; set; }
		public string calidad_aceite_transmision_bayoneta_observacion { get; set; }
		public string liquido_bateria_condiciones_observacion { get; set; }
		public string bandas_poly_v_observacion { get; set; }
		public string poleas_banda_observacion { get; set; }
		public string banda_tiempo_observacion { get; set; }
		public string otros_habitaculo_motor_observacion { get; set; }
		public string reset_intervalo_servicio_observacion { get; set; }
		public string ajuste_tornillos_neumaticos_torquimetro_observacion { get; set; }
		public string limpiar_libricar_puertas_cerraduras_observacion { get; set; }
		public string completar_plan_mantenimiento_observacion { get; set; }



		public string lectura_codigos_foto { get; set; }
		public string servofreno_foto { get; set; }
		public string pedal_freno_foto { get; set; }
		public string pedal_estacionamiento_foto { get; set; }
		public string cinturon_seguridad_foto { get; set; }
		public string cuadro_instrumentos_foto { get; set; }
		public string aire_acondicionado_foto { get; set; }
		public string bocina_claxon_foto { get; set; }
		public string iluminacion_interior_foto { get; set; }
		public string iluminacion_externa_foto { get; set; }
		public string limpiaparabrisas_foto { get; set; }
		public string limpia_medallon_foto { get; set; }
		public string neumaticos_friccion_foto { get; set; }
		public string otros_vehiculo_en_piso_foto { get; set; }
		public string estado_fugas_aceite_foto { get; set; }
		public string estado_nivel_calidad_lubricante_transmision_foto { get; set; }
		public string estado_nivel_calidad_lubricante_diferencial_foto { get; set; }
		public string cubrepolvos_flechas_foto { get; set; }
		public string componentes_direccion_foto { get; set; }
		public string componentes_suspesion_foto { get; set; }
		public string sistema_escape_completo_foto { get; set; }
		public string sistema_alimentacion_combustible_foto { get; set; }
		public string filtro_combustible_foto { get; set; }
		public string control_fugas_direccion_hidraulica_foto { get; set; }
		public string otros_altura_media_foto { get; set; }
		public string holgura_partes_suspension_rueda_foto { get; set; }
		public string control_neumaticos_desgaste_presion_foto { get; set; }
		public string profundidad_foto { get; set; }
		public string presion_foto { get; set; }
		public string otros_altura_total_foto { get; set; }
		public string nivel_calidad_aceite_motor_foto { get; set; }
		public string filtro_aire_foto { get; set; }
		public string filtro_polen_foto { get; set; }
		public string filtro_pcv_foto { get; set; }
		public string valvula_pcv_foto { get; set; }
		public string bujias_encendido_foto { get; set; }
		public string cables_bujias_bobinas_ignicion_foto { get; set; }
		public string nivel_anticongenlante_foto { get; set; }
		public string tapon_radiador_foto { get; set; }
		public string mangueras_sistema_foto { get; set; }
		public string desempeño_ventilador_foto { get; set; }
		public string calidad_liquido_limpiaparabrisas_foto { get; set; }
		public string calidad_aceite_direccion_hidraulica_foto { get; set; }
		public string calidad_aceite_transmision_bayoneta_foto { get; set; }
		public string liquido_bateria_condiciones_foto { get; set; }
		public string bandas_poly_v_foto { get; set; }
		public string poleas_banda_foto { get; set; }
		public string banda_tiempo_foto { get; set; }
		public string otros_habitaculo_motor_foto { get; set; }
		public string reset_intervalo_servicio_foto { get; set; }
		public string ajuste_tornillos_neumaticos_torquimetro_foto { get; set; }
		public string limpiar_libricar_puertas_cerraduras_foto { get; set; }
		public string completar_plan_mantenimiento_foto { get; set; }

		public string rodamiento_mazas_rueda_foto { get; set; }

		public string TiempoTranscurrido { get; set; }
		public int Activo { get; set; }


        public string lectura_codigos_SemaforoIcon
        {
            get
            {
                switch (lectura_codigos)
                {
                    case 2: return char.ConvertFromUtf32(0xF111);

                    case 1: return "\uF111"; // círculo amarillo (podrías cambiar el color desde el XAML)
                    case 0: return "\uF111"; // círculo rojo
                    default: return "\uF111"; // gris u otro
                }
            }
        }

        public string servofreno_SemaforoIcon
        {
            get
            {
                switch (servofreno)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string pedal_freno_SemaforoIcon
        {
            get
            {
                switch (pedal_freno)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string pedal_estacionamiento_SemaforoIcon
        {
            get
            {
                switch (pedal_estacionamiento)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string cinturon_seguridad_SemaforoIcon
        {
            get
            {
                switch (cinturon_seguridad)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string cuadro_instrumentos_SemaforoIcon
        {
            get
            {
                switch (cuadro_instrumentos)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string aire_acondicionado_SemaforoIcon
        {
            get
            {
                switch (aire_acondicionado)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string bocina_claxon_SemaforoIcon
        {
            get
            {
                switch (bocina_claxon)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string iluminacion_interior_SemaforoIcon
        {
            get
            {
                switch (iluminacion_interior)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string iluminacion_externa_SemaforoIcon
        {
            get
            {
                switch (iluminacion_externa)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string limpiaparabrisas_SemaforoIcon
        {
            get
            {
                switch (limpiaparabrisas)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string limpia_medallon_SemaforoIcon
        {
            get
            {
                switch (limpia_medallon)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string neumaticos_friccion_SemaforoIcon
        {
            get
            {
                switch (neumaticos_friccion)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string otros_vehiculo_en_piso_SemaforoIcon
        {
            get
            {
                switch (otros_vehiculo_en_piso)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string estado_fugas_aceite_SemaforoIcon
        {
            get
            {
                switch (estado_fugas_aceite)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string estado_nivel_calidad_lubricante_transmision_SemaforoIcon
        {
            get
            {
                switch (estado_nivel_calidad_lubricante_transmision)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string estado_nivel_calidad_lubricante_diferencial_SemaforoIcon
        {
            get
            {
                switch (estado_nivel_calidad_lubricante_diferencial)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string cubrepolvos_flechas_SemaforoIcon
        {
            get
            {
                switch (cubrepolvos_flechas)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string componentes_direccion_SemaforoIcon
        {
            get
            {
                switch (componentes_direccion)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string componentes_suspesion_SemaforoIcon
        {
            get
            {
                switch (componentes_suspesion)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string sistema_escape_completo_SemaforoIcon
        {
            get
            {
                switch (sistema_escape_completo)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string sistema_alimentacion_combustible_SemaforoIcon
        {
            get
            {
                switch (sistema_alimentacion_combustible)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string filtro_combustible_SemaforoIcon
        {
            get
            {
                switch (filtro_combustible)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string control_fugas_direccion_hidraulica_SemaforoIcon
        {
            get
            {
                switch (control_fugas_direccion_hidraulica)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string otros_altura_total_SemaforoIcon
        {
            get
            {
                switch (otros_altura_total)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string rodamiento_mazas_rueda_SemaforoIcon
        {
            get
            {
                switch (rodamiento_mazas_rueda)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string holgura_partes_suspension_rueda_SemaforoIcon
        {
            get
            {
                switch (holgura_partes_suspension_rueda)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string control_neumaticos_desgaste_presion_SemaforoIcon
        {
            get
            {
                switch (control_neumaticos_desgaste_presion)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string profundidad_SemaforoIcon
        {
            get
            {
                switch (profundidad)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string presion_SemaforoIcon
        {
            get
            {
                switch (presion)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string otros_altura_media_SemaforoIcon
        {
            get
            {
                switch (otros_altura_media)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string nivel_calidad_aceite_motor_SemaforoIcon
        {
            get
            {
                switch (nivel_calidad_aceite_motor)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string filtro_aire_SemaforoIcon
        {
            get
            {
                switch (filtro_aire)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string filtro_polen_SemaforoIcon
        {
            get
            {
                switch (filtro_polen)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string filtro_pcv_SemaforoIcon
        {
            get
            {
                switch (filtro_pcv)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string valvula_pcv_SemaforoIcon
        {
            get
            {
                switch (valvula_pcv)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string bujias_encendido_SemaforoIcon
        {
            get
            {
                switch (bujias_encendido)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string cables_bujias_bobinas_ignicion_SemaforoIcon
        {
            get
            {
                switch (cables_bujias_bobinas_ignicion)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string nivel_anticongenlante_SemaforoIcon
        {
            get
            {
                switch (nivel_anticongenlante)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string tapon_radiador_SemaforoIcon
        {
            get
            {
                switch (tapon_radiador)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string mangueras_sistema_SemaforoIcon
        {
            get
            {
                switch (mangueras_sistema)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string desempeño_ventilador_SemaforoIcon
        {
            get
            {
                switch (desempeño_ventilador)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string calidad_liquido_limpiaparabrisas_SemaforoIcon
        {
            get
            {
                switch (calidad_liquido_limpiaparabrisas)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string calidad_aceite_direccion_hidraulica_SemaforoIcon
        {
            get
            {
                switch (calidad_aceite_direccion_hidraulica)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string calidad_aceite_transmision_bayoneta_SemaforoIcon
        {
            get
            {
                switch (calidad_aceite_transmision_bayoneta)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string liquido_bateria_condiciones_SemaforoIcon
        {
            get
            {
                switch (liquido_bateria_condiciones)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string bandas_poly_v_SemaforoIcon
        {
            get
            {
                switch (bandas_poly_v)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string poleas_banda_SemaforoIcon
        {
            get
            {
                switch (poleas_banda)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string banda_tiempo_SemaforoIcon
        {
            get
            {
                switch (banda_tiempo)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string otros_habitaculo_motor_SemaforoIcon
        {
            get
            {
                switch (otros_habitaculo_motor)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string reset_intervalo_servicio_SemaforoIcon
        {
            get
            {
                switch (reset_intervalo_servicio)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string ajuste_tornillos_neumaticos_torquimetro_SemaforoIcon
        {
            get
            {
                switch (ajuste_tornillos_neumaticos_torquimetro)
                {
                    case 2: return "semaforo_verde.png";
                    case 1: return "semaforo_amarillo.png";
                    case 0: return "semaforo_rojo.png";
                    default: return "semaforo_gris.png";
                }
            }
        }

        public string limpiar_libricar_puertas_cerraduras_SemaforoIcon
        {
            get
            {
                switch (limpiar_libricar_puertas_cerraduras)
                {
                    case 2: return "\uf111"; // círculo verde - FontAwesome unicode
                    case 1: return "\uf111"; // círculo amarillo (podrías cambiar el color desde el XAML)
                    case 0: return "\uf111"; // círculo rojo
                    default: return "\uf111"; // gris u otro
                }
            }
        }

        public string completar_plan_mantenimiento_SemaforoIcon
        {
            get
            {
                switch (completar_plan_mantenimiento)
                {
                    case 2: return "\uf111"; // círculo verde - FontAwesome unicode
                    case 1: return "\uf111"; // círculo amarillo (podrías cambiar el color desde el XAML)
                    case 0: return "\uf111"; // círculo rojo
                    default: return "\uf111"; // gris u otro
                }
            }
        }


    }



}













