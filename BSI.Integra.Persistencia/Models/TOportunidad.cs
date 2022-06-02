using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOportunidad
    {
        public TOportunidad()
        {
            TActividadDetalle = new HashSet<TActividadDetalle>();
            TAsignacionOportunidad = new HashSet<TAsignacionOportunidad>();
            TCalidadProcesamiento = new HashSet<TCalidadProcesamiento>();
            TComprobantePagoOportunidad = new HashSet<TComprobantePagoOportunidad>();
            TDatoOportunidadAreaVenta = new HashSet<TDatoOportunidadAreaVenta>();
            TModeloDataMining = new HashSet<TModeloDataMining>();
            TModeloPredictivoProbabilidad = new HashSet<TModeloPredictivoProbabilidad>();
            TOportunidadClasificacionOperaciones = new HashSet<TOportunidadClasificacionOperaciones>();
            TOportunidadCompetidor = new HashSet<TOportunidadCompetidor>();
            TOportunidadLog = new HashSet<TOportunidadLog>();
            TSolucionClienteByActividad = new HashSet<TSolucionClienteByActividad>();
        }

        public int Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdAlumno { get; set; }
        public string UltimoComentario { get; set; }
        public int? IdActividadDetalleUltima { get; set; }
        public int? IdActividadCabeceraUltima { get; set; }
        public int? IdEstadoActividadDetalleUltimoEstado { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int IdEstadoOportunidad { get; set; }
        public int? IdEstadoOcurrenciaUltimo { get; set; }
        public int IdFaseOportunidadMaxima { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdCampaniaScoring { get; set; }
        public int? IdFaseOportunidadIp { get; set; }
        public int? IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public DateTime FechaRegistroCampania { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public int? IdFaseOportunidadPf { get; set; }
        public string CodigoPagoIc { get; set; }
        public int? FlagVentaCruzada { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdTiempoCapacitacionValidacion { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }
        public DateTime? FechaPaso2 { get; set; }
        public bool? Paso2 { get; set; }
        public string CodMailing { get; set; }
        public int? IdPagina { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? NroSolicitud { get; set; }
        public int? NroSolicitudPorArea { get; set; }
        public int? NroSolicitudPorSubArea { get; set; }
        public int? NroSolicitudPorProgramaGeneral { get; set; }
        public int? NroSolicitudPorProgramaEspecifico { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdPadre { get; set; }
        public int? IdAnuncioFacebook { get; set; }

        public virtual TAnuncioFacebook IdAnuncioFacebookNavigation { get; set; }
        public virtual ICollection<TActividadDetalle> TActividadDetalle { get; set; }
        public virtual ICollection<TAsignacionOportunidad> TAsignacionOportunidad { get; set; }
        public virtual ICollection<TCalidadProcesamiento> TCalidadProcesamiento { get; set; }
        public virtual ICollection<TComprobantePagoOportunidad> TComprobantePagoOportunidad { get; set; }
        public virtual ICollection<TDatoOportunidadAreaVenta> TDatoOportunidadAreaVenta { get; set; }
        public virtual ICollection<TModeloDataMining> TModeloDataMining { get; set; }
        public virtual ICollection<TModeloPredictivoProbabilidad> TModeloPredictivoProbabilidad { get; set; }
        public virtual ICollection<TOportunidadClasificacionOperaciones> TOportunidadClasificacionOperaciones { get; set; }
        public virtual ICollection<TOportunidadCompetidor> TOportunidadCompetidor { get; set; }
        public virtual ICollection<TOportunidadLog> TOportunidadLog { get; set; }
        public virtual ICollection<TSolucionClienteByActividad> TSolucionClienteByActividad { get; set; }
    }
}
