using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppMensajePublicidadBO : BaseBO
    {
        public int IdPersonal { get; set; }
        public int? IdConjuntoListaResultado { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPais { get; set; }
        public bool EsValido { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
    }
}
