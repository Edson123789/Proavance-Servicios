using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadCabeceraMasivoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdActividadBase { get; set; }
        public int? IdConjuntoLista { get; set; }
        public int? IdFrecuencia { get; set; }
        public DateTime? FechaInicioActividad { get; set; }
        public DateTime? FechaFinActividad { get; set; }
        public byte? DiaFrecuenciaMensual { get; set; }
        public bool? EsRepetitivo { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public byte? CantidadIntevaloTiempo { get; set; }
        public int? IdTiempoIntervalo { get; set; }
        public bool? Activo { get; set; }
        public List<int> Semanal { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }
        public bool? EsEnvioMasivo { get; set; }
    }
}
