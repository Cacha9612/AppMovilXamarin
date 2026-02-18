using System;
using System.Collections.Generic;

namespace AppMovilTrotaMundos.Models
{
	public class Rol
	{
		public int Id_rol { get; set; }
		public string Descripcion { get; set; }
		public int Estatus { get; set; }
	}

	public class Permisos
	{
        public string Vista { get; set; }
        public List<string> RolesPermitidos { get; set; }
    }
}