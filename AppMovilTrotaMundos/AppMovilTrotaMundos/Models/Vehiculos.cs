using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AppMovilTrotaMundos.Models
{
    public class Vehiculos
    {
        public int ID { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Empleado { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string No_serie { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string Motor { get; set; }
        public string Kms { get; set; }
        public int MotorVehiculo { get; set; }
        public int Acumulador { get; set; }
        public int Espejo_retrovisor { get; set; }
        public int Espejo_izquierdo { get; set; }
        public int Espejo_derecho { get; set; }
        public int Antena { get; set; }
        public int Tapones_ruedas { get; set; }
        public int Radio { get; set; }
        public int Encendedor { get; set; }
        public int Gato { get; set; }
        public int Herramienta { get; set; }
        public int Llanta_refaccion { get; set; }
        public int Limpiadores { get; set; }
        public int Pintura_rayada { get; set; }
        public int Cristales_rotos { get; set; }
        public int Golpes { get; set; }
        public int Tapetes { get; set; }
        public int Extintor { get; set; }
        public int Tapones_gasolina { get; set; }
        public int Calaveras_rotas { get; set; }
        public int Molduras_completas { get; set; }

        


        public int Panel_instrumentos { get; set; }
        public int Lado_izquierdo { get; set; }
        public int Lado_izquierdo_inf { get; set; }
        public int Lado_derecho { get; set; }
        public int Lado_derecho_inf { get; set; }
        public int Tablero { get; set; }
        public int Guantera { get; set; }
        public int Consola { get; set; }
        public int LadoFrontal { get; set; }
        public int LadoTrasero { get; set; }
        public int Cajuela { get; set; }
        public string NumeroEconomico { get; set; }

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> MotorVehiculo_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Acumulador_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Espejo_retrovisor_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Espejo_izquierdo_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Espejo_derecho_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Antena_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Tapones_ruedas_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Radio_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Encendedor_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Gato_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Herramienta_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Llanta_refaccion_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Limpiadores_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Pintura_rayada_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Cristales_rotos_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Golpes_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Tapetes_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Extintor_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Tapones_gasolina_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Calaveras_rotas_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Molduras_completas_foto { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Panel_instrumentos_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Lado_izquierdo_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Lado_izquierdo_inf_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Lado_derecho_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Lado_derecho_inf_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Tablero_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Guantera_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Consola_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> LadoFrontal_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> LadoTrasero_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Cajuela_foto { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]

        public List<string> NumeroEconomico_foto { get; set; } = new List<string>();
        




        [JsonConverter(typeof(StringToListConverter))]
        public List<string> MotorVehiculo_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Acumulador_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Espejo_retrovisor_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Espejo_izquierdo_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Espejo_derecho_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Antena_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Tapones_ruedas_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Radio_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Encendedor_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Gato_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Herramienta_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Llanta_refaccion_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Limpiadores_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Pintura_rayada_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Cristales_rotos_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Golpes_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Tapetes_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Extintor_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Tapones_gasolina_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Calaveras_rotas_video { get; set; } = new List<string>();

        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Molduras_completas_video { get; set; } = new List<string>();


        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Panel_instrumentos_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Lado_izquierdo_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Lado_izquierdo_inf_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Lado_derecho_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Lado_derecho_inf_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Tablero_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Guantera_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Consola_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> LadoFrontal_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> LadoTrasero_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> Cajuela_video { get; set; } = new List<string>();
        [JsonConverter(typeof(StringToListConverter))]
        public List<string> NumeroEconomico_video { get; set; } = new List<string>();
        


        public int IdFlotilla { get; set; }
        public int IdOrdenServicio { get; set; }
        public int Activo { get; set; }
        [JsonIgnore]
        public string DescripcionCompleta => $"Vehículo: {Modelo} - Placa: {Placa} - Orden {IdOrdenServicio} - Número Económico {NumeroEconomico}";
    }

}




