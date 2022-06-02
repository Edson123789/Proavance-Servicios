using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WhatsAppConfiguracionPreEnvioDTO
    {
        public int Id { get; set; }
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
        public string objetoplantilla { get; set; }
        public bool Procesado { get; set; }
    }

    public class RegistroWhatsAppConfiguracionPreEnvioDTO
    {
        public int NumeroValidos { get; set; }
        public int NumerosNoValidos { get; set; }
        public List<VistaWhatsAppConfiguracionPreEnvioDTO> ListaPreConfigurados { get; set; }
    }

    public class VistaWhatsAppConfiguracionPreEnvioDTO
    {
        public int Id { get; set; }
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int IdAlumno { get; set; }
        public string Alumno { get; set; }
        public string Celular { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public string Personal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public string WhatsAppEstadoValidacion { get; set; }
        public string objetoplantilla { get; set; }
    }

    public class DiaFallidoEvaluadoDTO
    {
        public int Id { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdPersonal { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPlantilla { get; set; }
        public int Dia { get; set; }
        public DateTime FechaEvaluada { get; set; }
        public List<int> ListaDiaConfigurado { get; set; }
        public int Cantidad { get; set; }
    }
}
