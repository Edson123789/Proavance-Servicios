using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Persistencia.Models.Vistas
{
    public partial class VAgendaActividadProgramada : BaseVistaEntity
    {
        public int Id { get; set; }
        public string TipoActividad { get; set; }
        public string EstadoHoja { get; set; }
        public string CentroCosto { get; set; }
        public string Contacto { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string CodigoFase { get; set; }
        public string NombreTipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdOportunidad { get; set; }
        public string UltimoComentario { get; set; }
        public int? IdActividadCabecera { get; set; }
        public int? ActividadesVencidas { get; set; }
        public bool? ReprogramacionManual { get; set; }
        public bool? ReprogramacionAutomatica { get; set; }
        public string ActividadCabecera { get; set; }
        public string Asesor { get; set; }
        public int? IdPersonal_Asignado { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdTipoDato { get; set; }
        public string ProbabilidadActualDesc { get; set; }
        public string CategoriaNombre { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdEstadoOportunidad { get; set; }
        public bool? ValidaLlamada { get; set; }
        public string EstadoOportunidad { get; set; }
    }
}
