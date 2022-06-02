using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SolucionClienteByActividadDTO
    {
        public int IdOportunidad { get; set; }
        public int IdActividadDetalle { get; set; }
        public int IdCausa { get; set; }
        public int IdPersonal { get; set; }
        public bool Solucionado { get; set; }
        public int IdProblemaCliente { get; set; }
        public string OtroProblema { get; set; }
     
    }
    public class SolucionClienteByActividadAlternoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdProblema { get; set; }
        public bool Seleccionado { get; set; }
        public bool Solucionado { get; set; }

    }
}
