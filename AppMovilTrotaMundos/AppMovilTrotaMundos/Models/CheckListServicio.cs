using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Models
{
    public class CheckListServicio
    {
        // Propiedades generales del checklist
        public string Fecha { get; set; }
        public int IdEmpleado { get; set; }

        public int IdVehiculo { get; set; }
        public int Id_ordendeservicio { get; set; }
        public string NumeroSerie { get; set; }
        public int Id { get; set; }

        public int id_checklist { get; set; }



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


        
        //public string lectura_codigos_semaforo { get; set; }
       

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


    }
}













