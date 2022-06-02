using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadDetalleDTO
    {
        public int Id { get; set; }
        public int? IdActividadCabecera { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? DuracionReal { get; set; }
        public int? IdOcurrencia { get; set; }
        public int IdEstadoActividadDetalle { get; set; }
        public string Comentario { get; set; }
        public int? IdAlumno { get; set; }
        public string Actor { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentralLlamada { get; set; }
        public string RefLlamada { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
    }
}
