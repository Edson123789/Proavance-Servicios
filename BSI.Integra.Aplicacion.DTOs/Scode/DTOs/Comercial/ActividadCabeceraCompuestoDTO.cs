using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadCabeceraCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion2 { get; set; }
        public int DuracionEstimada { get; set; }
        public bool ReproManual { get; set; }
        public bool ReproAutomatica { get; set; }
        public int Idplantilla { get; set; }
        public int IdActividadBase { get; set; }
        public DateTime FechaModificacion2 { get; set; }
        public bool ValidaLlamada { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int NumeroMaximoLlamadas { get; set; }
        public List<ReprogramacionCabeceraDTO> Reprogramaciones { get; set; }
        public List<int> TipoDato { get; set; }
        public string Usuario { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }
        public bool? EsEnvioMasivo { get; set; }
    }
}
