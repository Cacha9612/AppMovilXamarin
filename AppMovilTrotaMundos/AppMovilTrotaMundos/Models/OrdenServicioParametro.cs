using System;
using System.Collections.Generic;
using System.Text;

namespace AppMovilTrotaMundos.Models
{
    public class OrdenServicioParametro
    {
        public int IdCliente { get; set; }
        public string NoSerie { get; set; }

        public OrdenServicioParametro(int idCliente, string noSerie)
        {
            IdCliente = idCliente;
            NoSerie = noSerie;
        }
    }
}
