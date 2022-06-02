using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PreWhatsAppResultadoConjuntoListaDTO
    {
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public int? IdWhatsAppConfiguracionEnvio { get; set; }
        public bool? Prevalidado { get; set; }
        public List<datoPlantillaWhatsApp> objetoplantilla { get; set; }
    }

    public class WhatsAppPrimeraListaCampaniaGeneralDTO
    {
        public int IdPrioridadMailChimpListaCorreo { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdPgeneral { get; set; }
        public bool Prevalidado { get; set; }
    }

    public class WhatsAppResultadoCampaniaGeneralDTO
    {
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdPersonal { get; set; }
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdPrioridadMailChimpListaCorreo { get; set; }
        public List<datoPlantillaWhatsApp> ListaObjetoPlantilla { get; set; }
    }

    public class WhatsAppResultadoConjuntoListaDTO
    {
        public int IdPre { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public List<datoPlantillaWhatsApp> objetoplantilla { get; set; }
    }
    public class datoPlantillaWhatsApp
    {
        public string codigo { get; set; }
        public string texto { get; set; }
    }

    public class WhatsAppResultadoPostulanteDTO
    {
        public int IdPostulante { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public string Plantilla { get; set; }
        public int IdPersonal { get; set; }
    }
}
