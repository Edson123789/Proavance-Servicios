using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPersonal
    {
        public TPersonal()
        {
            TBandejaPendientePw = new HashSet<TBandejaPendientePw>();
            TCampaniaGeneralDetalle = new HashSet<TCampaniaGeneralDetalle>();
            TComisionMontoPago = new HashSet<TComisionMontoPago>();
            TComisionPersonal = new HashSet<TComisionPersonal>();
            TConvocatoriaPersonal = new HashSet<TConvocatoriaPersonal>();
            TCorreoGmail = new HashSet<TCorreoGmail>();
            TDocumentacionPersonal = new HashSet<TDocumentacionPersonal>();
            TExamenAsignadoEvaluador = new HashSet<TExamenAsignadoEvaluador>();
            TGrupoFiltroProgramaCriticoPorAsesor = new HashSet<TGrupoFiltroProgramaCriticoPorAsesor>();
            THorarioGrupoPersonal = new HashSet<THorarioGrupoPersonal>();
            TInstagramComentario = new HashSet<TInstagramComentario>();
            TLlamadaWebphoneReinicioAsesor = new HashSet<TLlamadaWebphoneReinicioAsesor>();
            TMessengerUsuarioLog = new HashSet<TMessengerUsuarioLog>();
            TPerfilPuestoTrabajoPersonalAprobacion = new HashSet<TPerfilPuestoTrabajoPersonalAprobacion>();
            TPersonalCertificacion = new HashSet<TPersonalCertificacion>();
            TPersonalLog = new HashSet<TPersonalLog>();
            TPersonalMotivoTiempoInactividad = new HashSet<TPersonalMotivoTiempoInactividad>();
            TPersonalPuestoSedeHistorico = new HashSet<TPersonalPuestoSedeHistorico>();
            TPlantillaRevisionPw = new HashSet<TPlantillaRevisionPw>();
            TPrioridadMailChimpLista = new HashSet<TPrioridadMailChimpLista>();
            TReportePendienteHistorico = new HashSet<TReportePendienteHistorico>();
            TSmsConfiguracionEnvio = new HashSet<TSmsConfiguracionEnvio>();
            TSolicitudCertificadoFisico = new HashSet<TSolicitudCertificadoFisico>();
            TUsuario = new HashSet<TUsuario>();
            TWhatsAppConfiguracionEnvio = new HashSet<TWhatsAppConfiguracionEnvio>();
            TWhatsAppMensajeEnviado = new HashSet<TWhatsAppMensajeEnviado>();
            TWhatsAppUsuario = new HashSet<TWhatsAppUsuario>();
        }

        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string TipoPersonal { get; set; }
        public string Email { get; set; }
        public string AreaAbrev { get; set; }
        public string Anexo { get; set; }
        public int? IdJefe { get; set; }
        public string Central { get; set; }
        public bool? Activo { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int? IdSexo { get; set; }
        public int? IdEstadocivil { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdRegion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string AutogeneradoEssalud { get; set; }
        public int? IdTipoSangre { get; set; }
        public string UrlFirmaCorreos { get; set; }
        public int? IdGrupoProgramasCriticos { get; set; }
        public int? IdCerrador { get; set; }
        public bool? EsCerrador { get; set; }
        public int? IdPaisDireccion { get; set; }
        public int? IdRegionDireccion { get; set; }
        public string CiudadDireccion { get; set; }
        public string NombreDireccion { get; set; }
        public string FijoReferencia { get; set; }
        public string MovilReferencia { get; set; }
        public string EmailReferencia { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string NombreCuspp { get; set; }
        public string DistritoDireccion { get; set; }
        public bool? ConEssalud { get; set; }
        public int? IdBusqueda { get; set; }
        public string AliasEmailAsesor { get; set; }
        public string Anexo3Cx { get; set; }
        public string Id3Cx { get; set; }
        public string Password3Cx { get; set; }
        public string Dominio { get; set; }
        public long? IdFacebookPersonal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EsPersonaValida { get; set; }
        public string UrlFoto { get; set; }
        public bool? AplicaFirmaHtml { get; set; }
        public string FirmaHtml { get; set; }
        public string CargoFirmaHtml { get; set; }
        public int? IdPostulante { get; set; }
        public int? UsuarioAsterisk { get; set; }
        public string ContrasenaAsterisk { get; set; }
        public int? IdTableroComercialCategoriaAsesor { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public int? IdRolUsuarioTicket { get; set; }
        public bool? DiscadorActivo { get; set; }
        public int? DiferenciaHoraria { get; set; }

        public virtual ICollection<TBandejaPendientePw> TBandejaPendientePw { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalle> TCampaniaGeneralDetalle { get; set; }
        public virtual ICollection<TComisionMontoPago> TComisionMontoPago { get; set; }
        public virtual ICollection<TComisionPersonal> TComisionPersonal { get; set; }
        public virtual ICollection<TConvocatoriaPersonal> TConvocatoriaPersonal { get; set; }
        public virtual ICollection<TCorreoGmail> TCorreoGmail { get; set; }
        public virtual ICollection<TDocumentacionPersonal> TDocumentacionPersonal { get; set; }
        public virtual ICollection<TExamenAsignadoEvaluador> TExamenAsignadoEvaluador { get; set; }
        public virtual ICollection<TGrupoFiltroProgramaCriticoPorAsesor> TGrupoFiltroProgramaCriticoPorAsesor { get; set; }
        public virtual ICollection<THorarioGrupoPersonal> THorarioGrupoPersonal { get; set; }
        public virtual ICollection<TInstagramComentario> TInstagramComentario { get; set; }
        public virtual ICollection<TLlamadaWebphoneReinicioAsesor> TLlamadaWebphoneReinicioAsesor { get; set; }
        public virtual ICollection<TMessengerUsuarioLog> TMessengerUsuarioLog { get; set; }
        public virtual ICollection<TPerfilPuestoTrabajoPersonalAprobacion> TPerfilPuestoTrabajoPersonalAprobacion { get; set; }
        public virtual ICollection<TPersonalCertificacion> TPersonalCertificacion { get; set; }
        public virtual ICollection<TPersonalLog> TPersonalLog { get; set; }
        public virtual ICollection<TPersonalMotivoTiempoInactividad> TPersonalMotivoTiempoInactividad { get; set; }
        public virtual ICollection<TPersonalPuestoSedeHistorico> TPersonalPuestoSedeHistorico { get; set; }
        public virtual ICollection<TPlantillaRevisionPw> TPlantillaRevisionPw { get; set; }
        public virtual ICollection<TPrioridadMailChimpLista> TPrioridadMailChimpLista { get; set; }
        public virtual ICollection<TReportePendienteHistorico> TReportePendienteHistorico { get; set; }
        public virtual ICollection<TSmsConfiguracionEnvio> TSmsConfiguracionEnvio { get; set; }
        public virtual ICollection<TSolicitudCertificadoFisico> TSolicitudCertificadoFisico { get; set; }
        public virtual ICollection<TUsuario> TUsuario { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvio> TWhatsAppConfiguracionEnvio { get; set; }
        public virtual ICollection<TWhatsAppMensajeEnviado> TWhatsAppMensajeEnviado { get; set; }
        public virtual ICollection<TWhatsAppUsuario> TWhatsAppUsuario { get; set; }
    }
}
