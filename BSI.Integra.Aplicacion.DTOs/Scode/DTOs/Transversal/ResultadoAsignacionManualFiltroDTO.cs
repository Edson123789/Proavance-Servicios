using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ResultadoAsignacionManualFiltroDTO
    {
        public int Id { get; set; }
        public int? Total { get; set; }
        public int? IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public string Asesor { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdAlumno { get; set; }
        public string Contacto { get; set; }
		public string Email { get; set; }
		public DateTime? FechaRegistroCampania { get; set; }
		public string Categoria { get; set; }
		public string AreaCapacitacion { get; set; }
		public string SubAreaCapacitacion { get; set; }
		public string NombreGrupo { get; set; }
		public string NombreCampania { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int? IdEstadoOportunidad { get; set; }
        public string EstadoOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }

        public int? NroOportunidades { get; set; }
        public int? NroSolicitud { get; set; }
        public int? NroSolicitudPorArea { get; set; }
        public int? NroSolicitudPorSubArea { get; set; }
        public int? NroSolicitudPorProgramaGeneral { get; set; }
        public int? NroSolicitudPorProgramaEspecifico { get; set; }
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }
        public string NombrePais { get; set; }
    }

    public class ResultadoAsignacionManualFiltroTotalDTO
    {
        public List<ResultadoAsignacionManualFiltroDTO> data { get; set; }
        public int Total { get; set; }
    }
}
