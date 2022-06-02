using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadProgramadaAgendaDTO
    {
        public int RowIndex { get; set; }
        public int Id { get; set; }
        public string TipoActividad { get; set; }
        public string EstadoHoja { get; set; }
        public int CentroCosto { get; set; }
        public string Contacto { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? ContactoIdATrabajo { get; set; }
        public int? ContactoIdIndustria { get; set; }
        public string CodigoFase { get; set; }
        public string TipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? FechaActividad { get; set; }
        public DateTime? HoraProgramada { get; set; }
        public int IdContacto { get; set; }
        public int IdOportunidad { get; set; }
        public string UltimoComentario { get; set; }
        public int IdActividadCabecera { get; set; }
        public int ActividadesVencidas { get; set; }
        public bool ReprogramacionManual { get; set; }
        public bool ReprogramacionAutomatica { get; set; }
        public string ActividadCabecera { get; set; }
        public string Asesor { get; set; }
        public int IdAsesor { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdTipoDato { get; set; }
        public bool ValidaLlamada { get; set; }
        public string ProbInicial { get; set; }
        public int ProbActual { get; set; }
        public string ProbabilidadActualDesc { get; set; }
        public string CategoriaNombre { get; set; }
        public int IdCategoriaOrigen { get; set; }

        public bool TieneMultipleSolicitud { get; set; }
    }
}
