using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class LlamadaWebphoneDTO
    {
        public int Id { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Anexo { get; set; }
        public string TelefonoDestino { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdLlamadasWebphoneTipo { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public string WebPhoneId { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdActividadDetalle { get; set; }
        public string LlamadasWebphoneEstado { get; set; }
        public string Usuario { get; set; }
        public string NombreGrabacion { get; set; }
    }
}
