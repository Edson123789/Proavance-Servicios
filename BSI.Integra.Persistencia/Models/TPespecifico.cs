using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecifico
    {
        public TPespecifico()
        {
            TConfiguracionCreacion = new HashSet<TConfiguracionCreacion>();
            TConfigurarWebinar = new HashSet<TConfigurarWebinar>();
            TCursoPespecifico = new HashSet<TCursoPespecifico>();
            TFeedbackGrupoPreguntaProgramaEspecifico = new HashSet<TFeedbackGrupoPreguntaProgramaEspecifico>();
            TMaterialAdicionalAulaVirtualPespecifico = new HashSet<TMaterialAdicionalAulaVirtualPespecifico>();
            TPreguntaProgramaCapacitacion = new HashSet<TPreguntaProgramaCapacitacion>();
            TProgramaGeneralMaterialEstudioAdicionalEspecificos = new HashSet<TProgramaGeneralMaterialEstudioAdicionalEspecificos>();
            TSolicitudOperacionesAccesoTemporalDetalle = new HashSet<TSolicitudOperacionesAccesoTemporalDetalle>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string Frecuencia { get; set; }
        public string EstadoP { get; set; }
        public string Tipo { get; set; }
        public string TipoAmbiente { get; set; }
        public string Categoria { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string Ciudad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string FechaInicioV { get; set; }
        public string FechaTerminoV { get; set; }
        public string CodigoBanco { get; set; }
        public string FechaInicioP { get; set; }
        public string FechaTerminoP { get; set; }
        public int? FrecuenciaId { get; set; }
        public int? EstadoPid { get; set; }
        public int? TipoId { get; set; }
        public int? CategoriaId { get; set; }
        public short? OrigenPrograma { get; set; }
        public int? IdCiudad { get; set; }
        public string CoordinadoraAcademica { get; set; }
        public string CoordinadoraCobranza { get; set; }
        public string Duracion { get; set; }
        public string ActualizacionAutomatica { get; set; }
        public int? IdCursoMoodle { get; set; }
        public bool? CursoIndividual { get; set; }
        public int? IdSesionInicio { get; set; }
        public int? IdExpositorReferencia { get; set; }
        public int? IdAmbiente { get; set; }
        public string UrlDocumentoCronograma { get; set; }
        public int? IdEstadoPespecifico { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string UrlDocumentoCronogramaGrupos { get; set; }
        public int? IdTroncalPartner { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public int? IdCursoRa { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdProveedorCalificaProyecto { get; set; }
        public string ObservacionCursoFinalizado { get; set; }

        public virtual TCentroCosto IdCentroCostoNavigation { get; set; }
        public virtual ICollection<TConfiguracionCreacion> TConfiguracionCreacion { get; set; }
        public virtual ICollection<TConfigurarWebinar> TConfigurarWebinar { get; set; }
        public virtual ICollection<TCursoPespecifico> TCursoPespecifico { get; set; }
        public virtual ICollection<TFeedbackGrupoPreguntaProgramaEspecifico> TFeedbackGrupoPreguntaProgramaEspecifico { get; set; }
        public virtual ICollection<TMaterialAdicionalAulaVirtualPespecifico> TMaterialAdicionalAulaVirtualPespecifico { get; set; }
        public virtual ICollection<TPreguntaProgramaCapacitacion> TPreguntaProgramaCapacitacion { get; set; }
        public virtual ICollection<TProgramaGeneralMaterialEstudioAdicionalEspecificos> TProgramaGeneralMaterialEstudioAdicionalEspecificos { get; set; }
        public virtual ICollection<TSolicitudOperacionesAccesoTemporalDetalle> TSolicitudOperacionesAccesoTemporalDetalle { get; set; }
    }
}
