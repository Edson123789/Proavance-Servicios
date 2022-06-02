using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosOportunidadReprogramacionAutomaticaDTO
    {
        public int IdPersonalAsignado { get; set; }
        public int IdActividadCabeceraUltima { get; set; }
        public int IdTipoDato { get; set; }
        public int IdCategoriaOrigen { get; set; }
    }
    public class DatosOportunidadReprogramacionManualOperacioneDTO
    {
        public int Caso { get; set; }
        public DateTime? FechaProximaCuota { get; set; }
        public string FechaProximaCuotaTexto { get; set; }
        public List<List<TimeSpan?>> personalHorario { get; set; }
    }
}
