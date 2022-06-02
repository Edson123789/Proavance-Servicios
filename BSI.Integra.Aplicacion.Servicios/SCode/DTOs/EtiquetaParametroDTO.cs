using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.DTOs
{
    public class EtiquetaParametroDTO
    {
        public int oportunidadId { get; set; }
        public int centrocostoId { get; set; }
        public int actividadDetalleId { get; set; }
        public int programaGeneralId { get; set; }
        public int programaEspecificoId { get; set; }
        public int idPais { get; set; }
    }
}
