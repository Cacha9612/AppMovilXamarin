using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppMovilTrotaMundos.Models
{
    public class Historico
    {
        public int IdChecklist { get; set; }
        public int IdVehiculo { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public int TiempoTranscurrido { get; set; }  // En segundos
        public string Estado { get; set; }
    }

}













