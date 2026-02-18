using System;
using System.Collections.Generic;
using System.Text;

namespace AppMovilTrotaMundos.Models
{
	public class Empleados
	{

		public int IdUsuario { get; set; }
		public string Nombre { get; set; }

		public string Password { get; set; }


		public int Rol { get; set; }
		public int Estatus { get; set; }

        public string EstatusTexto => Estatus == 1 ? "Activo" : "Inactivo";

        // Propiedad calculada para el texto del rol
        public string RolTexto
        {
            get
            {
                if (Rol == 1)
                    return "Administrador";
                else if (Rol == 2)
                    return "Almacén";
                else if (Rol == 3)
                    return "Técnicos";
                else if (Rol == 4)
                    return "Jefe de Taller";
                else
                    return "Desconocido";
            }
        }


    }



}
