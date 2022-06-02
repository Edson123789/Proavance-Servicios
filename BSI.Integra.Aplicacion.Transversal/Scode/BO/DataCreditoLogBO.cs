using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoLogBO : BaseBO
    {
        /// BO: DataCreditoLogBO
        /// Autor: Ansoli Espinoza
        /// Fecha: 17-01-2022
        /// <summary>
        /// BO para registrar el Log de peticiones al servicio de datacredito
        /// </summary>
        public string NumeroDocumento { get; set; }
        public string PrimerApellido { get; set; }
        public int? TipoIdentificacion { get; set; }
        public string RespuestaXml { get; set; }

        public int? IdMigracion { get; set; }
    }
}
