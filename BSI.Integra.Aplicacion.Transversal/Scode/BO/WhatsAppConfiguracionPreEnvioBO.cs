using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppConfiguracionPreEnvioBO : BaseBO
    {
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public string objetoplantilla { get; set; }
        public bool Procesado { get; set; }
        public string MensajeProceso { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
    }
}
