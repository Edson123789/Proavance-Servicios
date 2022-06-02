using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPgeneral
    {
        public TPgeneral()
        {
            TAdicionalProgramaGeneral = new HashSet<TAdicionalProgramaGeneral>();
            TAsesorProgramaGeneralDetalle = new HashSet<TAsesorProgramaGeneralDetalle>();
            TCampaniaGeneralDetallePrograma = new HashSet<TCampaniaGeneralDetallePrograma>();
            TCampaniaMailingDetallePrograma = new HashSet<TCampaniaMailingDetallePrograma>();
            TConfiguracionBeneficioProgramaGeneral = new HashSet<TConfiguracionBeneficioProgramaGeneral>();
            TConfigurarEvaluacionTrabajo = new HashSet<TConfigurarEvaluacionTrabajo>();
            TConfigurarExamenPrograma = new HashSet<TConfigurarExamenPrograma>();
            TConfigurarExamenesEncuestasEstructura = new HashSet<TConfigurarExamenesEncuestasEstructura>();
            TConfigurarVideoPrograma = new HashSet<TConfigurarVideoPrograma>();
            TEsquemaEvaluacionPgeneral = new HashSet<TEsquemaEvaluacionPgeneral>();
            TFeedbackGrupoPreguntaProgramaGeneral = new HashSet<TFeedbackGrupoPreguntaProgramaGeneral>();
            TMaterialAdicionalAulaVirtual = new HashSet<TMaterialAdicionalAulaVirtual>();
            TModeloGeneralPgeneral = new HashSet<TModeloGeneralPgeneral>();
            TModeloPredictivo = new HashSet<TModeloPredictivo>();
            TModeloPredictivoCargo = new HashSet<TModeloPredictivoCargo>();
            TModeloPredictivoCategoriaDato = new HashSet<TModeloPredictivoCategoriaDato>();
            TModeloPredictivoEscalaProbabilidad = new HashSet<TModeloPredictivoEscalaProbabilidad>();
            TModeloPredictivoFormacion = new HashSet<TModeloPredictivoFormacion>();
            TModeloPredictivoIndustria = new HashSet<TModeloPredictivoIndustria>();
            TModeloPredictivoTipoDato = new HashSet<TModeloPredictivoTipoDato>();
            TModeloPredictivoTrabajo = new HashSet<TModeloPredictivoTrabajo>();
            TPgeneralCodigoPartner = new HashSet<TPgeneralCodigoPartner>();
            TPgeneralConfiguracionPlantilla = new HashSet<TPgeneralConfiguracionPlantilla>();
            TPgeneralDescripcion = new HashSet<TPgeneralDescripcion>();
            TPgeneralExpositor = new HashSet<TPgeneralExpositor>();
            TPgeneralModalidad = new HashSet<TPgeneralModalidad>();
            TPgeneralParametroSeoPw = new HashSet<TPgeneralParametroSeoPw>();
            TPgeneralTagsPw = new HashSet<TPgeneralTagsPw>();
            TPgeneralVersionPrograma = new HashSet<TPgeneralVersionPrograma>();
            TPreguntaFrecuentePgeneral = new HashSet<TPreguntaFrecuentePgeneral>();
            TPreguntaProgramaCapacitacion = new HashSet<TPreguntaProgramaCapacitacion>();
            TProgramaAreaRelacionada = new HashSet<TProgramaAreaRelacionada>();
            TProgramaGeneralMaterialEstudioAdicional = new HashSet<TProgramaGeneralMaterialEstudioAdicional>();
            TProgramaGeneralPerfilAformacionCoeficiente = new HashSet<TProgramaGeneralPerfilAformacionCoeficiente>();
            TProgramaGeneralPerfilAtrabajoCoeficiente = new HashSet<TProgramaGeneralPerfilAtrabajoCoeficiente>();
            TProgramaGeneralPerfilCargoCoeficiente = new HashSet<TProgramaGeneralPerfilCargoCoeficiente>();
            TProgramaGeneralPerfilCategoriaCoeficiente = new HashSet<TProgramaGeneralPerfilCategoriaCoeficiente>();
            TProgramaGeneralPerfilCiudadCoeficiente = new HashSet<TProgramaGeneralPerfilCiudadCoeficiente>();
            TProgramaGeneralPerfilEscalaProbabilidad = new HashSet<TProgramaGeneralPerfilEscalaProbabilidad>();
            TProgramaGeneralPerfilIndustriaCoeficiente = new HashSet<TProgramaGeneralPerfilIndustriaCoeficiente>();
            TProgramaGeneralPerfilIntercepto = new HashSet<TProgramaGeneralPerfilIntercepto>();
            TProgramaGeneralPerfilModalidadCoeficiente = new HashSet<TProgramaGeneralPerfilModalidadCoeficiente>();
            TProgramaGeneralPerfilScoringAformacion = new HashSet<TProgramaGeneralPerfilScoringAformacion>();
            TProgramaGeneralPerfilScoringAtrabajo = new HashSet<TProgramaGeneralPerfilScoringAtrabajo>();
            TProgramaGeneralPerfilScoringCargo = new HashSet<TProgramaGeneralPerfilScoringCargo>();
            TProgramaGeneralPerfilScoringCategoria = new HashSet<TProgramaGeneralPerfilScoringCategoria>();
            TProgramaGeneralPerfilScoringCiudad = new HashSet<TProgramaGeneralPerfilScoringCiudad>();
            TProgramaGeneralPerfilScoringIndustria = new HashSet<TProgramaGeneralPerfilScoringIndustria>();
            TProgramaGeneralPerfilScoringModalidad = new HashSet<TProgramaGeneralPerfilScoringModalidad>();
            TProgramaGeneralPerfilTipoDato = new HashSet<TProgramaGeneralPerfilTipoDato>();
            TProgramaGeneralPuntoCorte = new HashSet<TProgramaGeneralPuntoCorte>();
            TSuscripcionProgramaGeneral = new HashSet<TSuscripcionProgramaGeneral>();
            TWhatsAppConfiguracionEnvioPorPrograma = new HashSet<TWhatsAppConfiguracionEnvioPorPrograma>();
        }

        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string PwImgPortada { get; set; }
        public string PwImgPortadaAlf { get; set; }
        public string PwImgSecundaria { get; set; }
        public string PwImgSecundariaAlf { get; set; }
        public int? IdPartner { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdCategoria { get; set; }
        public string PwEstado { get; set; }
        public string PwMostrarBsplay { get; set; }
        public string PwDuracion { get; set; }
        public int? IdBusqueda { get; set; }
        public int? IdChatZopim { get; set; }
        public string PgTitulo { get; set; }
        public string Codigo { get; set; }
        public string UrlImagenPortadaFr { get; set; }
        public string UrlBrochurePrograma { get; set; }
        public string UrlPartner { get; set; }
        public string UrlVersion { get; set; }
        public string PwTituloHtml { get; set; }
        public bool? EsModulo { get; set; }
        public string NombreCorto { get; set; }
        public int? IdPagina { get; set; }
        public int? ChatActivo { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string PwDescripcionGeneral { get; set; }
        public bool? TieneProyectoDeAplicacion { get; set; }
        public int? IdTipoPrograma { get; set; }
        public string CodigoPartner { get; set; }
        public string LogoPrograma { get; set; }
        public string UrlLogoPrograma { get; set; }

        public virtual TTipoPrograma IdTipoProgramaNavigation { get; set; }
        public virtual ICollection<TAdicionalProgramaGeneral> TAdicionalProgramaGeneral { get; set; }
        public virtual ICollection<TAsesorProgramaGeneralDetalle> TAsesorProgramaGeneralDetalle { get; set; }
        public virtual ICollection<TCampaniaGeneralDetallePrograma> TCampaniaGeneralDetallePrograma { get; set; }
        public virtual ICollection<TCampaniaMailingDetallePrograma> TCampaniaMailingDetallePrograma { get; set; }
        public virtual ICollection<TConfiguracionBeneficioProgramaGeneral> TConfiguracionBeneficioProgramaGeneral { get; set; }
        public virtual ICollection<TConfigurarEvaluacionTrabajo> TConfigurarEvaluacionTrabajo { get; set; }
        public virtual ICollection<TConfigurarExamenPrograma> TConfigurarExamenPrograma { get; set; }
        public virtual ICollection<TConfigurarExamenesEncuestasEstructura> TConfigurarExamenesEncuestasEstructura { get; set; }
        public virtual ICollection<TConfigurarVideoPrograma> TConfigurarVideoPrograma { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneral> TEsquemaEvaluacionPgeneral { get; set; }
        public virtual ICollection<TFeedbackGrupoPreguntaProgramaGeneral> TFeedbackGrupoPreguntaProgramaGeneral { get; set; }
        public virtual ICollection<TMaterialAdicionalAulaVirtual> TMaterialAdicionalAulaVirtual { get; set; }
        public virtual ICollection<TModeloGeneralPgeneral> TModeloGeneralPgeneral { get; set; }
        public virtual ICollection<TModeloPredictivo> TModeloPredictivo { get; set; }
        public virtual ICollection<TModeloPredictivoCargo> TModeloPredictivoCargo { get; set; }
        public virtual ICollection<TModeloPredictivoCategoriaDato> TModeloPredictivoCategoriaDato { get; set; }
        public virtual ICollection<TModeloPredictivoEscalaProbabilidad> TModeloPredictivoEscalaProbabilidad { get; set; }
        public virtual ICollection<TModeloPredictivoFormacion> TModeloPredictivoFormacion { get; set; }
        public virtual ICollection<TModeloPredictivoIndustria> TModeloPredictivoIndustria { get; set; }
        public virtual ICollection<TModeloPredictivoTipoDato> TModeloPredictivoTipoDato { get; set; }
        public virtual ICollection<TModeloPredictivoTrabajo> TModeloPredictivoTrabajo { get; set; }
        public virtual ICollection<TPgeneralCodigoPartner> TPgeneralCodigoPartner { get; set; }
        public virtual ICollection<TPgeneralConfiguracionPlantilla> TPgeneralConfiguracionPlantilla { get; set; }
        public virtual ICollection<TPgeneralDescripcion> TPgeneralDescripcion { get; set; }
        public virtual ICollection<TPgeneralExpositor> TPgeneralExpositor { get; set; }
        public virtual ICollection<TPgeneralModalidad> TPgeneralModalidad { get; set; }
        public virtual ICollection<TPgeneralParametroSeoPw> TPgeneralParametroSeoPw { get; set; }
        public virtual ICollection<TPgeneralTagsPw> TPgeneralTagsPw { get; set; }
        public virtual ICollection<TPgeneralVersionPrograma> TPgeneralVersionPrograma { get; set; }
        public virtual ICollection<TPreguntaFrecuentePgeneral> TPreguntaFrecuentePgeneral { get; set; }
        public virtual ICollection<TPreguntaProgramaCapacitacion> TPreguntaProgramaCapacitacion { get; set; }
        public virtual ICollection<TProgramaAreaRelacionada> TProgramaAreaRelacionada { get; set; }
        public virtual ICollection<TProgramaGeneralMaterialEstudioAdicional> TProgramaGeneralMaterialEstudioAdicional { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilAformacionCoeficiente> TProgramaGeneralPerfilAformacionCoeficiente { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilAtrabajoCoeficiente> TProgramaGeneralPerfilAtrabajoCoeficiente { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilCargoCoeficiente> TProgramaGeneralPerfilCargoCoeficiente { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilCategoriaCoeficiente> TProgramaGeneralPerfilCategoriaCoeficiente { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilCiudadCoeficiente> TProgramaGeneralPerfilCiudadCoeficiente { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilEscalaProbabilidad> TProgramaGeneralPerfilEscalaProbabilidad { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilIndustriaCoeficiente> TProgramaGeneralPerfilIndustriaCoeficiente { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilIntercepto> TProgramaGeneralPerfilIntercepto { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilModalidadCoeficiente> TProgramaGeneralPerfilModalidadCoeficiente { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringAformacion> TProgramaGeneralPerfilScoringAformacion { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringAtrabajo> TProgramaGeneralPerfilScoringAtrabajo { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringCargo> TProgramaGeneralPerfilScoringCargo { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringCategoria> TProgramaGeneralPerfilScoringCategoria { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringCiudad> TProgramaGeneralPerfilScoringCiudad { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringIndustria> TProgramaGeneralPerfilScoringIndustria { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilScoringModalidad> TProgramaGeneralPerfilScoringModalidad { get; set; }
        public virtual ICollection<TProgramaGeneralPerfilTipoDato> TProgramaGeneralPerfilTipoDato { get; set; }
        public virtual ICollection<TProgramaGeneralPuntoCorte> TProgramaGeneralPuntoCorte { get; set; }
        public virtual ICollection<TSuscripcionProgramaGeneral> TSuscripcionProgramaGeneral { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvioPorPrograma> TWhatsAppConfiguracionEnvioPorPrograma { get; set; }
    }
}
