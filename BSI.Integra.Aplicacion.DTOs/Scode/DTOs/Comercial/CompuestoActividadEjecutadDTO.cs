using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion
{
    public class CompuestoActividadEjecutadDTO
    {
        public int TotalOportunidades { get; set; }
        public int Id { get; set; }
        public string CentroCosto { get; set; }
        public string Contacto { get; set; }
        public string CodigoFase { get; set; }
        public string NombreTipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public Nullable<int> Duracion { get; set; }
        public string Actividad { get; set; }
        public string Ocurrencia { get; set; }
        public string Comentario { get; set; }
        public string Asesor { get; set; }
        public int IdContacto { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CategoriaNombre { get; set; }
        public int IdCategoria { get; set; }
        public string TiempoLlamadas { get; set; }
        public string FaseMaxima { get; set; }
        public string FaseInicial { get; set; }
        public int NumeroLlamadas { get; set; }
        public string DuracionTimbrado { get; set; }
        public string DuracionContesto { get; set; }
        public string EstadoLlamada { get; set; }
        public string UnicoTimbrado { get; set; }
        public string UnicoContesto { get; set; }
        public string UnicoEstadoLlamada { get; set; }
        public string Estado { get; set; }
        public string EstadoClasificacion { get; set; }
        public string UnicoClasificacion { get; set; }
        public DateTime? UnicoFechaLlamada { get; set; }
        public List<CompuestoActividadEjecutadaDetalleDTO> Lista { get; set; }
        public double MinutosIntervale { get; set; }
        public double MinutosTotalTimbrado { get; set; }
        public double MinutosTotalContesto { get; set; }
        public double MinutosTotalPerdido { get; set; }
        public double MayorTiempo { get; set; }
        public string TiemposTresCX { get; set; }

    }
}
