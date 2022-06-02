using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadCerrarDTO
    {
        public int Valor { get; set; }
        public int IdOportunidad { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdPersonalAsignado { get; set; }
    }
}
