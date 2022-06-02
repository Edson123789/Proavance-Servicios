using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Comercial
{
    public class LlamadaWebphoneModificadoDTO
    {
        public int IdLlamada { get; set; }
        public string NombreArchivo { get; set; }
        public string NombreUsuario { get; set; }
        public int DuracionContesto { get; set; }
        public int NroBytes { get; set; }
    }
    public class RespuestaLlamadaWebphoneModificadoDTO
    {
        public int Valor { get; set; }
    }
}
