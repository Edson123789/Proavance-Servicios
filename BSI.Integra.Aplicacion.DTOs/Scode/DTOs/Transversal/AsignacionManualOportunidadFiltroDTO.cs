using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignacionManualOportunidadFiltroDTO
    {
        public string CentrosCosto { get; set; }
        public string Asesores { get; set; }
        public string TiposDato { get; set; }
        public string Origenes { get; set; }
        public string Categorias { get; set; }
        public string FasesOportunidad { get; set; }
        public string Programa { get; set; }
        public string Area { get; set; }
        public string subArea { get; set; }
        public string Pais { get; set; }
        public string TipoCategoriaOrigen { get; set; }
        public string contacto { get; set; }
        public string email { get; set; }
        public Nullable<DateTime> FechaInicio { get; set; }
        public Nullable<DateTime> FechaFin { get; set; }
        public string Probabilidad { get; set; }
        public string ventaCruzada { get; set; }
        public string UsuarioModificacion { get; set; }
        public Nullable<DateTime> FechaProgramacionInicio { get; set; }
        public Nullable<DateTime> FechaProgramacionFin { get; set; }
        public int? NroOportunidades { get; set; }
        public int? IdOperadorComparacionNroOportunidades { get; set; }
        public int? NroSolicitud { get; set; }
        public int? IdOperadorComparacionNroSolicitud { get; set; }
        public int? NroSolicitudPorArea { get; set; }
        public int? IdOperadorComparacionNroSolicitudPorArea { get; set; }
        public int? NroSolicitudPorSubArea { get; set; }
        public int? IdOperadorComparacionNroSolicitudPorSubArea { get; set; }
        public int? NroSolicitudPorProgramaGeneral { get; set; }
        public int? IdOperadorComparacionNroSolicitudPorProgramaGeneral { get; set; }
        public int? NroSolicitudPorProgramaEspecifico { get; set; }
        public int? IdOperadorComparacionNroSolicitudPorProgramaEspecifico { get; set; }

    }
}
