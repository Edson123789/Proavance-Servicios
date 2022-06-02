using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: WhatsAppConfiguracionEnvioDetalle
    ///Autor: Fischer Valdez - Joao Benavente
    ///Fecha: 07/05/2021
    ///<summary>
    ///Columnas y funciones de la tabla mkt.T_WhatsAppConfiguracionEnvioDetalle
    ///</summary>
    public class WhatsAppConfiguracionEnvioDetalleBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///EnviadoCorrectamente                     Flag para determinar si se envio correctamente el WhatsApp
        ///MensajeError                             Cadena con el mensaje de error si en caso hubiera occurido en el envio
        ///IdConjuntoListaResultado                 Id del conjuntolistaresultado (PK de la tabla mkt.T_ConjuntoListaResultado)
        ///ConjuntoListaNroEjecucion                Entero con la cantidad de ejecuciones que se realizo el conjuntolista
        ///IdWhatsAppConfiguracionLogEjecucion      Id del log de ejecucion de whatsapp (PK de la tabla mkt.T_WhatsAppConfiguracionLogEjecucion)
        ///Mensaje                                  Id de alumno
        ///WhatsAppId                               Id del Whatsapp, formato cadena
        ///IdMigracion                              Id de la migracion (campo nullable)
        ///DescartarCrearOportunidad                Flag para descartar el crear una oportunidad
        ///IdPrioridadMailChimpListaCorreo          Id del resultado de calculo de Mailchimp (PK de la tabla mkt.T_PrioridadMailChimpListaCorreo)
        public bool EnviadoCorrectamente { get; set; }
        public string MensajeError { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int ConjuntoListaNroEjecucion { get; set; }
        public int IdWhatsAppConfiguracionLogEjecucion { get; set; }
        public string Mensaje { get; set; }
        public string WhatsAppId { get; set; }
        public int? IdMigracion { get; set; }
        public bool? DescartarCrearOportunidad { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }

    }
}
