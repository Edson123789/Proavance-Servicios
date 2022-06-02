using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampaniaMailingDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int PrincipalValor { get; set; }
        public string PrincipalValorTiempo { get; set; }
        public int SecundarioValor { get; set; }
        public string SecundarioValorTiempo { get; set; }
        public int ActivaValor { get; set; }
        public string ActivaValorTiempo { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal { get; set; }
        public DateTime? FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal { get; set; }
        public List<CampaniaMailingValorTipoDTO> ListaExcluirPorCampaniaMailing { get; set; }
        public List<PrioridadesDTO> ListaPrioridades { get; set; }
    }
    public class CampaniaGeneralDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatoria")]
        [StringLength(100, ErrorMessage = "La longitud maxima del nombre es de 100 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La categoria origen es obligatoria")]
        public int? IdCategoriaOrigen { get; set; }
        [Required(ErrorMessage = "La fecha de envio es obligatoria")]
        public DateTime? FechaEnvio { get; set; }
        [Required(ErrorMessage = "El nivel de segmentacion es obligatorio")]
        [Range(1, 3)]
        public int? IdNivelSegmentacion { get; set; }
        public string NivelSegmentacion { get; set; }
        [Required(ErrorMessage = "La hora de envio de Mailing es obligatoria")]
        public int? IdHoraEnvio_Mailing { get; set; }
        [Required(ErrorMessage = "El tipo de asociacion es obligatorio")]
        [Range(1, 2, ErrorMessage = "El valor del tipo de asociacion debe estar entre 1 y 2")]
        public int? IdTipoAsociacion { get; set; }
        [Required(ErrorMessage = "La probabilidad de registro es obligatoria")]
        [Range(1, 4, ErrorMessage = "La valor de la probabilidad de registro debe estar entre 1 y 4")]
        public int? IdProbabilidadRegistro_Nivel { get; set; }
        [Required(ErrorMessage = "El numero maximo de segmentos es obligatorio")]
        [Range(1, 10, ErrorMessage = "El valor de numero maximo de segmentos debe estar entre 1 y 10")]
        public int? NroMaximoSegmentos { get; set; }
        [Required(ErrorMessage = "La cantidad de periodo sin correo es obligatoria")]
        public int? CantidadPeriodoSinCorreo { get; set; }
        [Required(ErrorMessage = "El tipo de frecuencia es obligatorio")]
        public int? IdTiempoFrecuencia { get; set; }
        [Required(ErrorMessage = "El filtro segmento es obligatorio")]
        public int? IdFiltroSegmento { get; set; }
        [Required(ErrorMessage = "La plantilla Mailing es obligatoria")]
        public int? IdPlantilla_Mailing { get; set; }
        [Required(ErrorMessage = "El dominio del remitente es obligatoria")]
        public int? IdRemitenteMailing { get; set; }
        public bool? IncluyeWhatsapp { get; set; }
        public bool? EnEjecucion { get; set; }
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }
        public DateTime? FechaFinEnvioWhatsapp { get; set; }
        public int? NumeroMinutosPrimerEnvio { get; set; }
        public int? IdHoraEnvio_Whatsapp { get; set; }
        public int? DiasSinWhatsapp { get; set; }
        public int? IdPlantilla_Whatsapp { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public List<PrioridadesCampaniaGeneralDetalleDTO> ListaPrioridades { get; set; }

    }
    public class GestionCampaniaGeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? CantidadContactosMailing { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string HoraEnvio_Mailing { get; set; }
        public bool? IncluirRebotes { get; set; }
        public int IdEstadoEnvio_Mailing { get; set; }
        public string EstadoEnvio_Mailing { get; set; }
        public int? CantidadContactosWhatsapp { get; set; }
        public int? CorreosEnviadosMailchimp { get; set; }
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }
        public DateTime? FechaFinEnvioWhatsapp { get; set; }
        public string HoraEnvio_Whatsapp { get; set; }
        public string EstadoEnvio_Whatsapp { get; set; }
        public int? Enviados_Whatsapp { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class GestionCampaniaGeneralValorDTO
    {
        public int Id { get; set; }
        public bool IncluirRebotes { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class FiltroReporteMailingMetricaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class FiltroReporteMailingMetricaDetalleDTO
    {
        public int IdCampaniaMailing { get; set; }
        public bool VersionMailing { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class ReporteCampaniaMailchimpMetricaDTO
    {
        public int IdCampaniaMailing { get; set; }
        public string NombreCampaniaMailing { get; set; }
        public int IdCampaniaMailingDetalle { get; set; }
        public string NombreCampaniaMailingDetalle { get; set; }
        public bool VersionMailing { get; set; }
        public DateTime FechaEnvioCampaniaMailing { get; set; }
        public int? CantidadEnviadosMailChimp { get; set; }
        public int? CantidadEntregaExitosaMailChimp { get; set; }
        public int? CantidadAperturaUnica { get; set; }
        public double? TasaAperturaUnica { get; set; }
        public double? TasaRebote { get; set; }
        public double? TasaClic { get; set; }
        public int? CantidadReboteDuro { get; set; }
        public int? CantidadReboteSuave { get; set; }
        public int? CantidadReboteSintaxis { get; set; }
        public int? CantidadTotalRebotes { get; set; }
        public int? CantidadClicUnico { get; set; }
        public int? CantidadTotalClic { get; set; }
        public int? CantidadCorreoAbiertoMismoDia { get; set; }
        public int? CantidadCorreoAbiertoDosTresDias { get; set; }
        public int? CantidadCorreoAbiertoCuatroSieteDias { get; set; }
        public int? CantidadCorreoAbiertoOchoCatorceDias { get; set; }
        public int? CantidadCorreoAbiertoQuinceTreintaDias { get; set; }
        public int? CantidadCorreoAbiertoTreintaUnoNoventaDias { get; set; }
        public int? CantidadCorreoAbiertoNoventaMedioAnioDias { get; set; }
        public int? CantidadCorreoAbierto { get; set; }
        public DateTime? FechaConsulta { get; set; }
        public int CantidadRegistros { get; set; }
        public int CantidadOportunidades { get; set; }
        public int CantidadMailingAsesores { get; set; }
        public int CantidadTotalOportunidades { get; set; }
    }

    public class ReporteCampaniaMailchimpMetricaRegistrosDTO
    {
        public int Id { get; set; }
        public int IdCampaniaMailing { get; set; }
        public string NombreCampaniaMailing { get; set; }
        public int IdCampaniaMailingDetalle { get; set; }
        public string NombreCampaniaMailingDetalle { get; set; }
        public bool VersionMailing { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public DateTime FechaEnvioCampaniaMailing { get; set; }
        public int CantidadCategoriaOrigenChat { get; set; }
        public int CantidadCategoriaOrigenChatOffline { get; set; }
        public int CantidadCategoriaAccesoPrueba { get; set; }
        public int CantidadCategoriaFormularioCarrera { get; set; }
        public int CantidadCategoriaFormularioContactenos { get; set; }
        public int CantidadCategoriaFormularioPago { get; set; }
        public int CantidadCategoriaFormularioPrograma { get; set; }
        public int CantidadCategoriaFormularioPropio { get; set; }
        public int CantidadCategoriaFormularioRegistrarse { get; set; }
        public int CantidadCategoriaOrigenInteligenteChat { get; set; }
        public int CantidadCategoriaOrigenInteligenteChatOffline { get; set; }
        public int CantidadCategoriaInteligenteAccesoPrueba { get; set; }
        public int CantidadCategoriaFormularioInteligenteCarrera { get; set; }
        public int CantidadCategoriaFormularioInteligenteContactenos { get; set; }
        public int CantidadCategoriaFormularioInteligentePago { get; set; }
        public int CantidadCategoriaFormularioInteligentePrograma { get; set; }
        public int CantidadCategoriaFormularioInteligentePropio { get; set; }
        public int CantidadCategoriaFormularioInteligenteRegistrarse { get; set; }
        public int CantidadRegistrosMailing { get; set; }
    }

    public class CorreoAbiertoPorFechaConsultaDTO
    {
        public DateTime? FechaConsulta { get; set; }
        public int CantidadCorreoAbierto { get; set; }
    }

    public class ReporteCampaniaMailchimpMetricaOportunidadesDTO
    {
        public int Id { get; set; }
        public int IdCampaniaMailing { get; set; }
        public string NombreCampaniaMailing { get; set; }
        public int IdCampaniaMailingDetalle { get; set; }
        public string NombreCampaniaMailingDetalle { get; set; }
        public bool VersionMailing { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public DateTime FechaEnvioCampaniaMailing { get; set; }
        public int CantidadCategoriaOrigenChat { get; set; }
        public int CantidadCategoriaOrigenChatOffline { get; set; }
        public int CantidadCategoriaAccesoPrueba { get; set; }
        public int CantidadCategoriaFormularioCarrera { get; set; }
        public int CantidadCategoriaFormularioContactenos { get; set; }
        public int CantidadCategoriaFormularioPago { get; set; }
        public int CantidadCategoriaFormularioPrograma { get; set; }
        public int CantidadCategoriaFormularioPropio { get; set; }
        public int CantidadCategoriaFormularioRegistrarse { get; set; }
        public int CantidadCategoriaOrigenInteligenteChat { get; set; }
        public int CantidadCategoriaOrigenInteligenteChatOffline { get; set; }
        public int CantidadCategoriaInteligenteAccesoPrueba { get; set; }
        public int CantidadCategoriaFormularioInteligenteCarrera { get; set; }
        public int CantidadCategoriaFormularioInteligenteContactenos { get; set; }
        public int CantidadCategoriaFormularioInteligentePago { get; set; }
        public int CantidadCategoriaFormularioInteligentePrograma { get; set; }
        public int CantidadCategoriaFormularioInteligentePropio { get; set; }
        public int CantidadCategoriaFormularioInteligenteRegistrarse { get; set; }
        public int CantidadOportunidadesMailing { get; set; }
    }
}
