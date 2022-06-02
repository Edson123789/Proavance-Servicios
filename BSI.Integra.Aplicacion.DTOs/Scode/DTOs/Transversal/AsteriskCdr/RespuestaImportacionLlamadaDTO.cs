using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Transversal.AsteriskCdr
{
    public class RespuestaImportacionLlamadaDTO
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public int IdLlamadaInicial { get; set; }
        public int IdLlamadaFinal { get; set; }
    }
}
