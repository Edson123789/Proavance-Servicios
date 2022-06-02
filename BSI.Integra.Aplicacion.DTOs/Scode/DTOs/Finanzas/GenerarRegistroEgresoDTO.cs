using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GenerarRegistroEgresoDTO
    {
        public CajaEgresoAprobadoDTO CajaRECAprobado { get; set; }
        public List<IdCajaEgresoCanceladoDTO> ListaEgresoCancelado { get; set; }
    }
}
