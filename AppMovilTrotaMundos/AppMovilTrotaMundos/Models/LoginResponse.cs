using System;
using System.Collections.Generic;
using System.Text;

namespace AppMovilTrotaMundos.Models
{
	public class LoginResponse
	{
	
		public string access_token { get; set; }

		public string token_type { get; set; }

		public string sub { get; set; }
        public int idUsuario { get; set; }
        public int idRol { get; set; }
		
	}
}
