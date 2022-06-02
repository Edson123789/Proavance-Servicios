using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GenerarRegistroEgresoInmediatoDTO
    {
        public CajaEgresoAprobadoDTO CajaEgresoAprobado { get; set; }
        public List<RegistroEgresoCajaDTO> ListaRegistroEgreso { get; set; }
    }
}
