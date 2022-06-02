using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class FacebookPostLogTaskBO : BaseEntity
    {
        public string Message { get; set; }
        public string ResponseJson { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
