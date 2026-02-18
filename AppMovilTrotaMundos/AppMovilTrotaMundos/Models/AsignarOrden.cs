using System;
using System.Collections.Generic;
using System.Text;

namespace AppMovilTrotaMundos.Models
{
    public class AsignarOrden
    {
        public int IdOrden { get; set; }
        public int IdVehiculo { get; set; } // Relación con el vehículo
        public int? IdTecnico { get; set; } // Técnico asignado, puede ser nulo
    
        public DateTime FechaCreacion { get; set; }
  

    }
}
