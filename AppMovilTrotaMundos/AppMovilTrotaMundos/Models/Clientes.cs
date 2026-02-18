using System;
using System.Collections.Generic;
using System.Text;

namespace AppMovilTrotaMundos.Models
{
	public class Clientes
	{ 
		public int IdCliente { get; set; }
		public string Nombre { get; set; }
		public string Calle { get; set; }
		public string Colonia { get; set; }
		public string Ciudad { get; set; }
		public string Estado { get; set;}
		public string Tel { get; set; }
		public string Cel { get; set; }
		public string Email { get; set; }
		public string RFC { get; set; }

		public int No_int {get;set;}
		public string Facturar_a {get;set;}
		public int Id_empleado { get; set; }

        public List<Vehiculos> Vehiculos { get; set; }  // Aquí se definen los vehículos asociados al cliente

    }
}
