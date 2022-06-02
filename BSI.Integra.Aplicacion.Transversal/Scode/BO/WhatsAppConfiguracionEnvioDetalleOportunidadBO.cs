using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: WhatsAppConfiguracionEnvioDetalleOportunidad
    ///Autor: Joao Benavente - Gian Miranda
    ///Fecha: 23/03/2021
    ///<summary>
    ///BO para la interaccion con la tabla T_WhatsAppConfiguracionEnvioDetalleOportunidad
    ///</summary>
    public class WhatsAppConfiguracionEnvioDetalleOportunidadBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///IdWhatsAppConfiguracionEnvioDetalle      Id de WhatsAppConfiguracionEnvioDetalle (PK de la tabla mkt.T_WhatsAppConfiguracionEnvioDetalle)
        ///IdOportunidad                            Id de la oportunidad (PK de la tabla com.T_Oportunidad)
        ///IdCentroCosto                            Id del Centro de Costo (PK de la tabla pla.T_CentroCosto)
        ///IdPersonal                               Id del personal (PK de la tabla gp.T_Personal)
        ///IdCodigoPais                             Id del Codigo del Pais (PK de la tabla conf.T_Pais)
        ///IdMigracion                              Id de migracion (nullable)

        public int IdWhatsAppConfiguracionEnvioDetalle { get; set; }
        public int? IdOportunidad { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPersonal { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? IdMigracion { get; set; }

    }
}
