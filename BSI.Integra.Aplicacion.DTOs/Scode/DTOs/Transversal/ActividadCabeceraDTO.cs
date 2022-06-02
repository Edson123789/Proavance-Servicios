using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadCabeceraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion2 { get; set; }
        public int DuracionEstimada { get; set; }
        public bool ReproManual { get; set; }
        public bool ReproAutomatica { get; set; }
        public int Idplantilla { get; set; }
        public int IdActividadBase { get; set; }
        public DateTime? FechaModificacion2 { get; set; }
        public bool ValidaLlamada { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int NumeroMaximoLlamadas { get; set; }
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
		public int IdPersonalAreaTrabajo { get; set; }
		public string PersonalAreaTrabajo { get; set; }
		public int? IdFacebookCuentaPublicitaria { get; set; }

	}
}
