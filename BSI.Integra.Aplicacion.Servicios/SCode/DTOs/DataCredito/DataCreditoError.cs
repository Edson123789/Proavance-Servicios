using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.DTOs.DataCredito
{
    public class DataCreditoError : Exception
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
    }
}
