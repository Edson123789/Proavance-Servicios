using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaEvaluacion_RegistrarAsignacionDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public List<int> IdModalidad { get; set; }
        public List<int> IdProveedor { get; set; }
        public int IdPGeneral { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool EsquemaPredeterminado { get; set; }
        public List<EsquemaEvaluacion_RegistrarDetalleAsignacionDTO> ListadoDetalleAsignacion { get; set; }

        public string NombreUsuario { get; set; }
    }
}
