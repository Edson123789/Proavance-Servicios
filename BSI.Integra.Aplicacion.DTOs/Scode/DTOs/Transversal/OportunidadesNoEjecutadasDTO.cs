using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadesNoEjecutadasDTO
    {
        public String Id { get; set; }
        public DateTime FechaProgramada { get; set; }
        public int? IdCodigoPais { get; set; }
    }
}
