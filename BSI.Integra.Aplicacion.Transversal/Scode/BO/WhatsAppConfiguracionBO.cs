using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
     public class WhatsAppConfiguracionBO : BaseBO
    {
        public string UrlWhatsApp { get; set; }
        public string IpHost { get; set; }
        public string Numero { get; set; }
        public string Vname { get; set; }
        public string Certificado { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }

    }
}
