using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Comercial
{
    public class CentralLlamadaDTO
    {
        public int Estado { get; set; }
        public string Mensaje { get; set; }
        public int IdErroneo { get; set; }
    }
}
